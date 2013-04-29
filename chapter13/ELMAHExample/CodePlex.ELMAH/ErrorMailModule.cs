#region Byline & Disclaimer
//
//  Author(s):
//
//      Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)
//      Andrew Cain (mistercain@gmail.com, http://dotnettricks.com/blogs/andrewcainblog/default.aspx)
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
#endregion

namespace CodePlex.Elmah
{
    #region Imports
    
    using System;
    using System.Web;
    using System.Net.Mail;
    using System.IO;

    using System.Configuration;
    using System.Collections;
    using System.Threading;
    
    #endregion

    /// <summary>
    /// HTTP module that sends an e-mail whenever an unhandled exception
    /// occurs in an ASP.NET web application.
    /// </summary>

    public class ErrorMailModule : IHttpModule
    {
        private string _mailSender;
        private string _mailRecipient;
        private string _mailSubjectFormat;
        private bool _reportAsynchronously;

        /// <summary>
        /// Initializes the module and prepares it to handle requests.
        /// </summary>

        public virtual void Init(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            //
            // Get the configuration section of this module.
            // If it's not there then there is nothing to initialize or do.
            // In this case, the module is as good as mute.
            //

            IDictionary config = (IDictionary) GetConfig();

            if (config == null)
            {
                return;
            }

            //
            // Extract the settings.
            //

            string mailRecipient = GetSetting(config, "to");
            string mailSender = GetSetting(config, "from", mailRecipient);
            string mailSubjectFormat = GetSetting(config, "subject", string.Empty);
            bool reportAsynchronously = Convert.ToBoolean(GetSetting(config, "async", bool.TrueString));

            //
            // Hook into the Error event of the application.
            //

            application.Error += new EventHandler(OnError);

            //
            // Finally, commit the state of the module if we got this far.
            // Anything beyond this point should not cause an exception.
            //

            _mailRecipient = mailRecipient;
            _mailSender = mailSender;
            _mailSubjectFormat = mailSubjectFormat;
            _reportAsynchronously = reportAsynchronously;
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module.
        /// </summary>

        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Gets the e-mail address of the sender.
        /// </summary>
        
        protected virtual string MailSender
        {
            get { return _mailSender; }
        }
        
        /// <summary>
        /// Gets the e-mail address of the recipient, or a semicolon-delimited 
        /// list of e-mail addresses in case of multiple recipients.
        /// </summary>

        protected virtual string MailRecipient
        {
            get { return _mailRecipient; }
        }

        /// <summary>
        /// Gets the text used to format the e-mail subject.
        /// </summary>
        /// <remarks>
        /// The subject text specification may include {0} where the
        /// error message (<see cref="Error.Message"/>) should be inserted 
        /// and {1} <see cref="Error.Type"/> where the error type should 
        /// be insert.
        /// </remarks>

        protected virtual string MailSubjectFormat
        {
            get { return _mailSubjectFormat; }
        }
        
        /// <summary>
        /// The handler called when an unhandled exception bubbles up to 
        /// the module.
        /// </summary>

        protected virtual void OnError(object sender, EventArgs e)
        {
            //
            // Get the last error and then report it synchronously or 
            // asynchronously based on the configuration.
            //

            HttpApplication application = (HttpApplication) sender;
            Error error = GetLastError(application);

            if (_reportAsynchronously)
            {
                ReportErrorAsync(error);
            }
            else
            {
                ReportError(error);
            }
        }

        /// <summary>
        /// Schedules the error to be e-mailed asynchronously.
        /// </summary>
        /// <remarks>
        /// The default implementation uses the <see cref="ThreadPool"/>
        /// to queue the reporting.
        /// </remarks>

        protected virtual void ReportErrorAsync(Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            //
            // Schedule the reporting at a later time using a worker from 
            // the system thread pool. This makes the implementation
            // simpler, but it might have an impact on reducing the
            // number of workers available for processing ASP.NET
            // requests in the case where lots of errors being generated.
            //

            ThreadPool.QueueUserWorkItem(new WaitCallback(ReportError), error);
        }

        private void ReportError(object state)
        {
            ReportError((Error) state);
        }

        /// <summary>
        /// Schedules the error to be e-mailed synchronously.
        /// </summary>

        protected virtual void ReportError(Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            //
            // Start by checking if we have a sender and a recipient.
            // These values may be null if someone overrides the
            // implementation of Init but does not override the
            // MailSender and MailRecipient properties.
            //

            string sender = StringEtc.MaskNull(this.MailSender);
            string recipient = StringEtc.MaskNull(this.MailRecipient);

            if (sender.Length == 0 || recipient.Length == 0)
            {
                return;
            }

            //
            // Create the mail, setting up the sender and recipient.
            //

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(sender);
            mail.To.Add(new MailAddress(recipient));

            //
            // Format the mail subject.
            // 

            string subjectFormat = StringEtc.MaskNull(this.MailSubjectFormat);
        
            if (subjectFormat.Length == 0)
            {
                subjectFormat = "Error ({1}): {0}";
            }

            mail.Subject = string.Format(subjectFormat, error.Message, error.Type);

            //
            // Format the mail body.
            //

            ErrorTextFormatter formatter = CreateErrorFormatter();

            StringWriter bodyWriter = new StringWriter();
            formatter.Format(bodyWriter, error);
            mail.Body = bodyWriter.ToString();

            switch (formatter.MimeType)
            {
                case "text/html": mail.IsBodyHtml = true; break;
                case "text/plain": mail.IsBodyHtml = false;  break;

                default :
                {
                    throw new ApplicationException(string.Format(
                        "The error mail module does not know how to handle the {1} media type that is created by the {0} formatter.",
                        formatter.GetType().FullName, formatter.MimeType));
                }
            }

            //
            // Provide one last hook to pre-process the mail and then send 
            // it off.
            //

            try
            {
                PreSendMail(mail, error);
                SendMail(mail);
            }
            finally
            {
                DisposeMail(mail);
            }
        }

        /// <summary>
        /// Provides a final point (mainly for inheritors) to customize the 
        /// e-mail message before it is sent.
        /// </summary>

        protected virtual void PreSendMail(MailMessage mail, Error error)
        {
            if (mail == null)
                throw new ArgumentNullException("mail");

            if (error == null)
                throw new ArgumentNullException("error");

            //
            // If a HTML message supplied by the web host then attach it to
            // the mail.
            //

            if (error.WebHostHtmlMessage.Length != 0)
            {
                //
                // Create a temporary file to hold the attachment. Note that 
                // the temporary file is created in the location returned by
                // System.Web.HttpRuntime.CodegenDir. It is assumed that
                // this code will have sufficient rights to create the
                // temporary file in that area.
                //

                string fileName = "WebHostHtmlMessage-" + Guid.NewGuid().ToString() + ".htm";
                string path = Path.Combine(HttpRuntime.CodegenDir, fileName);

                try
                {
                    using (StreamWriter attachementWriter = File.CreateText(path))
                    {
                        attachementWriter.Write(error.WebHostHtmlMessage);
                    }

                    mail.Attachments.Add(new Attachment(path));
                }
                catch (IOException)
                {
                    //
                    // Ignore I/O errors as non-critical. It's not the
                    // end of the world if the attachment could not be
                    // created (though it would be nice). It is more
                    // important to get to deliver the error message!
                    //
                }
            }
        }

        /// <summary>
        /// Disposes the e-mail message after sending it, like deleting
        /// any temporary files created for attachements.
        /// </summary>

        protected virtual void DisposeMail(MailMessage mail)
        {
            foreach (Attachment attachment in mail.Attachments)
            {
                File.Delete(attachment.Name);
            }
        }

        /// <summary>
        /// Creates the <see cref="ErrorTextFormatter"/> implementation to 
        /// be used to format the body of the e-mail.
        /// </summary>

        protected virtual ErrorTextFormatter CreateErrorFormatter()
        {
            return new ErrorMailHtmlFormatter();
        }

        /// <summary>
        /// Sends the e-mail using <see cref="SmtpClient"/>.
        /// </summary>

        protected virtual void SendMail(MailMessage mail)
        {
            if (mail == null)
                throw new ArgumentNullException("mail");

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mail);
        }

        /// <summary>
        /// Gets the configuration object used by <see cref="Init"/> to read
        /// the settings for module.
        /// </summary>

        protected virtual object GetConfig()
        {
            return ConfigurationManager.GetSection("CodePlex.elmah/errorMail");
        }

        /// <summary>
        /// Builds an <see cref="Error"/> object from the last application
        /// exception generated.
        /// </summary>

        protected virtual Error GetLastError(HttpApplication application)
        {
            if (application == null)
                throw new ArgumentNullException("application");

            return new Error(application.Server.GetLastError(), application.Context);
        }

        private static string GetSetting(IDictionary config, string name)
        {
            return GetSetting(config, name, null);
        }

        private static string GetSetting(IDictionary config, string name, string defaultValue)
        {
            Debug.Assert(config != null);
            Debug.AssertStringNotEmpty(name);

            string value = StringEtc.MaskNull((string) config[name]);

            if (value.Length == 0)
            {
                if (defaultValue == null)
                {
                    throw new ApplicationException(string.Format(
                        "The required configuration setting '{0}' is missing for the error mailing module.", name));
                }

                value = defaultValue;
            }

            return value;
        }
    }
}
