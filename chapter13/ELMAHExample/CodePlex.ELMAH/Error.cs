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
    using System.Collections.Specialized;
    using System.Threading;
    using System.Xml;

	#endregion

    /// <summary>
    /// Represents a logical application error (as opposed to the actual 
    /// exception it may be representing).
    /// </summary>

    [ Serializable ]
	public class Error : IXmlExportable, ICloneable
	{
        private Exception _exception;
        private string _applicationName;
        private string _hostName;
        private string _typeName;
        private string _source;
        private string _message;
        private string _detail;
        private string _user;
        private DateTime _time;
        private int _statusCode;
        private string _webHostHtmlMessage;
        private NameValueCollection _serverVariables;
        private NameValueCollection _queryString;
        private NameValueCollection _form;
        private NameValueCollection _cookies;

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class.
        /// </summary>

        public Error() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class
        /// from a given <see cref="Exception"/> instance.
        /// </summary>

        public Error(Exception e) : 
            this(e, null) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="Error"/> class
        /// from a given <see cref="Exception"/> instance and 
        /// <see cref="HttpContext"/> instance representing the HTTP 
        /// context during the exception.
        /// </summary>

        public Error(Exception e, HttpContext context)
        {
            if (e == null)
                throw new ArgumentNullException("e");

            _exception = e;
            Exception baseException = e.GetBaseException();

            //
            // Load the basic information.
            //

            _hostName = Environment.MachineName;
            _typeName = baseException.GetType().FullName;
            _message = baseException.Message;
            _source = baseException.Source;
            _detail = e.ToString();
            _user = StringEtc.MaskNull(Thread.CurrentPrincipal.Identity.Name);
            _time = DateTime.Now;

            //
            // If this is an HTTP exception, then get the status code
            // and detailed HTML message provided by the host.
            //

            HttpException httpException = e as HttpException;

            if (httpException != null)
            {
                _statusCode = httpException.GetHttpCode();
                _webHostHtmlMessage = StringEtc.MaskNull(httpException.GetHtmlErrorMessage());
            }

            //
            // If the HTTP context is available, then capture the
            // collections that represent the state request.
            //

            if (context != null)
            {
                HttpRequest request = context.Request;

                _serverVariables = CopyCollection(request.ServerVariables);
                _queryString = CopyCollection(request.QueryString);
                _form = CopyCollection(request.Form);
                _cookies = CopyCollection(request.Cookies);
            }
        }

        /// <summary>
        /// Get the <see cref="Exception"/> instance used to initialize this
        /// instance.
        /// </summary>
        /// <remarks>
        /// This is a run-time property only that is not written or read 
        /// during XML serialization via <see cref="FromXml"/> and 
        /// <see cref="ToXml"/>.
        /// </remarks>

        public Exception Exception
        {
            get { return _exception; }
        }

        /// <summary>
        /// Gets or sets the name of application in which this error occurred.
        /// </summary>

        public string ApplicationName
        { 
            get { return StringEtc.MaskNull(_applicationName); }
            set { _applicationName = value; }
        }

        /// <summary>
        /// Gets or sets name of host machine where this error occurred.
        /// </summary>
        
        public string HostName
        { 
            get { return StringEtc.MaskNull(_hostName); }
            set { _hostName = value; }
        }

        /// <summary>
        /// Get or sets the type, class or category of the error.
        /// </summary>
        
        public string Type
        { 
            get { return StringEtc.MaskNull(_typeName); }
            set { _typeName = value; }
        }

        /// <summary>
        /// Gets or sets the source that is the cause of the error.
        /// </summary>
        
        public string Source
        { 
            get { return StringEtc.MaskNull(_source); }
            set { _source = value; }
        }

        /// <summary>
        /// Gets or sets a brief text describing the error.
        /// </summary>
        
        public string Message 
        { 
            get { return StringEtc.MaskNull(_message); }
            set { _message = value; }
        }

        /// <summary>
        /// Gets or sets a detailed text describing the error, such as a
        /// stack trace.
        /// </summary>

        public string Detail
        { 
            get { return StringEtc.MaskNull(_detail); }
            set { _detail = value; }
        }

        /// <summary>
        /// Gets or sets the user logged into the application at the time 
        /// of the error.
        /// </summary>
        
        public string User 
        { 
            get { return StringEtc.MaskNull(_user); }
            set { _user = value; }
        }

        /// <summary>
        /// Gets or sets the date and time (in local time) at which the 
        /// error occurred.
        /// </summary>
        
        public DateTime Time 
        { 
            get { return _time; }
            set { _time = value; }
        }

        /// <summary>
        /// Gets or sets the HTTP status code of the output returned to the 
        /// client for the error.
        /// </summary>
        /// <remarks>
        /// For cases where this value cannot always be reliably determined, 
        /// the value may be reported as zero.
        /// </remarks>
        
        public int StatusCode 
        { 
            get { return _statusCode; }
            set { _statusCode = value; }
        }

        /// <summary>
        /// Gets or sets the HTML message generated by the web host (ASP.NET) 
        /// for the given error.
        /// </summary>
        
        public string WebHostHtmlMessage
        {
            get { return StringEtc.MaskNull(_webHostHtmlMessage); }
            set { _webHostHtmlMessage = value; }
        }

        /// <summary>
        /// Gets a collection representing the Web server variables
        /// captured as part of diagnostic data for the error.
        /// </summary>
        
        public NameValueCollection ServerVariables 
        { 
            get { return FaultIn(ref _serverVariables);  }
        }

        /// <summary>
        /// Gets a collection representing the Web query string variables
        /// captured as part of diagnostic data for the error.
        /// </summary>
        
        public NameValueCollection QueryString 
        { 
            get { return FaultIn(ref _queryString); } 
        }

        /// <summary>
        /// Gets a collection representing the form variables captured as 
        /// part of diagnostic data for the error.
        /// </summary>
        
        public NameValueCollection Form 
        { 
            get { return FaultIn(ref _form); }
        }

        /// <summary>
        /// Gets a collection representing the client cookies
        /// captured as part of diagnostic data for the error.
        /// </summary>

        public NameValueCollection Cookies 
        {
            get { return FaultIn(ref _cookies); }
        }

        /// <summary>
        /// Returns the value of the <see cref="Message"/> property.
        /// </summary>

        public override string ToString()
        {
            return this.Message;
        }

        /// <summary>
        /// Loads the error object from its XML representation.
        /// </summary>

        public void FromXml(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            //
            // Reader must be positioned on an element!
            //

            if (!reader.IsStartElement())
                throw new ArgumentOutOfRangeException("reader");

            //
            // Read out the attributes that contain the simple
            // typed state.
            //

            ReadXmlAttributes(reader);

            //
            // Move past the element. If it's not empty, then
            // read also the inner XML that contains complex
            // types like collections.
            //

            bool isEmpty = reader.IsEmptyElement;
            reader.Read();

            if (!isEmpty)
            {
                ReadInnerXml(reader);
                reader.ReadEndElement();
            }
        }

        /// <summary>
        /// Reads the error data in XML attributes.
        /// </summary>
        
        protected virtual void ReadXmlAttributes(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            if (!reader.IsStartElement())
                throw new ArgumentOutOfRangeException("reader");

            _applicationName = reader.GetAttribute("application");
            _hostName = reader.GetAttribute("host");
            _typeName = reader.GetAttribute("type");
            _message = reader.GetAttribute("message");
            _source = reader.GetAttribute("source");
            _detail = reader.GetAttribute("detail");
            _user = reader.GetAttribute("user");
            string timeString = StringEtc.MaskNull(reader.GetAttribute("time"));
            _time = timeString.Length == 0 ? new DateTime() : XmlConvert.ToDateTime(timeString, System.Xml.XmlDateTimeSerializationMode.Local);
            string statusCodeString = StringEtc.MaskNull(reader.GetAttribute("statusCode"));
            _statusCode = statusCodeString.Length == 0 ? 0 : XmlConvert.ToInt32(statusCodeString);
            _webHostHtmlMessage = reader.GetAttribute("webHostHtmlMessage");
        }

        /// <summary>
        /// Reads the error data in child nodes.
        /// </summary>

        protected virtual void ReadInnerXml(XmlReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            //
            // Loop through the elements, reading those that we
            // recognize. If an unknown element is found then
            // this method bails out immediately without
            // consuming it, assuming that it belongs to a subclass.
            //

            while (reader.IsStartElement())
            {
                //
                // Optimization Note: This block could be re-wired slightly 
                // to be more efficient by not causing a collection to be
                // created if the element is going to be empty.
                //

                NameValueCollection collection;

                switch (reader.LocalName)
                {
                    case "serverVariables" : collection = this.ServerVariables; break;
                    case "queryString"     : collection = this.QueryString; break;
                    case "form"            : collection = this.Form; break;
                    case "cookies"         : collection = this.Cookies; break;
                    default                : return;
                }

                if (reader.IsEmptyElement)
                {
                    reader.Read();
                }
                else
                {
                    ((IXmlExportable) collection).FromXml(reader);
                }
            }
        }

        /// <summary>
        /// Writes the error data to its XML representation.
        /// </summary>

        public void ToXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (writer.WriteState != WriteState.Element)
                throw new ArgumentOutOfRangeException("writer");

            //
            // Write out the basic typed information in attributes
            // followed by collections as inner elements.
            //

            WriteXmlAttributes(writer);
            WriteInnerXml(writer);
        }

        /// <summary>
        /// Writes the error data that belongs in XML attributes.
        /// </summary>

        protected virtual void WriteXmlAttributes(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            WriteXmlAttribute(writer, "application", _applicationName);
            WriteXmlAttribute(writer, "host", _hostName);
            WriteXmlAttribute(writer, "type", _typeName);
            WriteXmlAttribute(writer, "message", _message);
            WriteXmlAttribute(writer, "source", _source);
            WriteXmlAttribute(writer, "detail", _detail);
            WriteXmlAttribute(writer, "user", _user);
            if (_time != DateTime.MinValue)
                WriteXmlAttribute(writer, "time", XmlConvert.ToString(_time, XmlDateTimeSerializationMode.Local));
            if (_statusCode != 0)
                WriteXmlAttribute(writer, "statusCode", XmlConvert.ToString(_statusCode));
            WriteXmlAttribute(writer, "webHostHtmlMessage", _webHostHtmlMessage);
        }

        /// <summary>
        /// Writes the error data that belongs in child nodes.
        /// </summary>

        protected virtual void WriteInnerXml(XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            WriteCollection(writer, "serverVariables", _serverVariables);
            WriteCollection(writer, "queryString", _queryString);
            WriteCollection(writer, "form", _form);
            WriteCollection(writer, "cookies", _cookies);
        }

        private void WriteCollection(XmlWriter writer, string name, NameValueCollection collection)
        {
            Debug.Assert(writer != null);
            Debug.AssertStringNotEmpty(name);

            if (collection != null && collection.Count != 0)
            {
                writer.WriteStartElement(name);
                ((IXmlExportable) collection).ToXml(writer);
                writer.WriteEndElement();
            }
        }

        private void WriteXmlAttribute(XmlWriter writer, string name, string value)
        {
            Debug.Assert(writer != null);
            Debug.AssertStringNotEmpty(name);

            if (value != null && value.Length != 0)
            {
                writer.WriteAttributeString(name, value);
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>

        object ICloneable.Clone()
        {
            //
            // Make a base shallow copy of all the members.
            //

            Error copy = (Error) MemberwiseClone();

            //
            // Now make a deep copy of items that are mutable.
            //

            copy._serverVariables = CopyCollection(_serverVariables);
            copy._queryString = CopyCollection(_queryString);
            copy._form = CopyCollection(_form);
            copy._cookies = CopyCollection(_cookies);

            return copy;
        }

        private NameValueCollection CopyCollection(NameValueCollection collection)
        {
            if (collection == null || collection.Count == 0)
            {
                return null;
            }
            else
            {
                return new HttpValuesCollection(collection);
            }
        }

        private NameValueCollection CopyCollection(HttpCookieCollection cookies)
        {
            if (cookies == null || cookies.Count == 0)
            {
                return null;
            }

            NameValueCollection copy = new HttpValuesCollection(cookies.Count);

            for (int i = 0; i < cookies.Count; i++)
            {
                HttpCookie cookie = cookies[i];

                //
                // NOTE: We drop the Path and Domain properties of the 
                // cookie for sake of simplicity.
                //

                copy.Add(cookie.Name, cookie.Value);
            }

            return copy;
        }

        private static NameValueCollection FaultIn(ref NameValueCollection collection)
        {
            if (collection == null)
            {
                collection = new HttpValuesCollection();
            }

            return collection;
        }
    }
}
