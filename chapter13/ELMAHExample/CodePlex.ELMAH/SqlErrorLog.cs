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
    using System.Data;
    using System.Data.SqlClient;
    using System.Collections;
    using System.Configuration;
    using System.IO;
    using System.Xml;

    #endregion

    /// <summary>
    /// An <see cref="ErrorLog"/> implementation that uses Microsoft SQL 
    /// Server 2000 as its backing store.
    /// </summary>
    
    public class SqlErrorLog : ErrorLog
    {
        private readonly string _connectionString;
        private const string CONNECTION_STRING_NAME = "connectionStringName";

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlErrorLog"/> class
        /// using a dictionary of configured settings.
        /// </summary>

        public SqlErrorLog(IDictionary config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            //
            // Get the connection string. If it is empty, then check for
            // another setting called connectionStringAppKey. The latter
            // specifies the key in appSettings that contains the actual
            // connection string to be used.
            //

            string connectionStringName = StringEtc.MaskNull((string) config[CONNECTION_STRING_NAME]);

            if (connectionStringName.Length == 0)
            {
                string connectionStringAppKey = StringEtc.MaskNull((string)config["connectionStringAppKey"]);

                if (connectionStringAppKey.Length != 0)
                {
                    _connectionString = ConfigurationManager.AppSettings[connectionStringAppKey];
                }
            }
            else
            {
                if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
                    throw new ApplicationException(string.Format("Connection string name '{0}' not found in ConfigurationManager.ConnectionStrings", connectionStringName));
                _connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
            }

            //
            // If there is no connection string to use then throw an 
            // exception to abort construction.
            //

            if (_connectionString.Length == 0)
            {
                throw new ApplicationException("Connection string is missing for the SQL error log.");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlErrorLog"/> class
        /// to use a specific connection string for connecting to the database.
        /// </summary>

        public SqlErrorLog(string connectionString)
        {
            if (connectionString == null)
                throw new ArgumentNullException(CONNECTION_STRING_NAME);

            if (connectionString.Length == 0)
                throw new ArgumentOutOfRangeException(CONNECTION_STRING_NAME);
            
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets the name of this error log implementation.
        /// </summary>
        
        public override string Name
        {
            get { return "Microsoft SQL Server Error Log"; }
        }

        /// <summary>
        /// Gets the connection string used by the log to connect to the database.
        /// </summary>
        
        public virtual string ConnectionString
        {
            get { return _connectionString; }
        }

        /// <summary>
        /// Logs an error to the database.
        /// </summary>
        /// <remarks>
        /// Use the stored procedure called by this implementation to set a
        /// policy on how long errors are kept in the log. The default
        /// implementation stores all errors for an indefinite time.
        /// </remarks>

        public override void Log(Error error)
        {
            if (error == null)
                throw new ArgumentNullException("error");

            StringWriter errorStringWriter = new StringWriter();
            XmlTextWriter errorXmlWriter = new XmlTextWriter(errorStringWriter);
            errorXmlWriter.Formatting = Formatting.Indented;

            errorXmlWriter.WriteStartElement("error");
            error.ToXml(errorXmlWriter);
            errorXmlWriter.WriteEndElement();
            errorXmlWriter.Flush();
            
            string errorXml = errorStringWriter.ToString();

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand("ELMAH_LogError", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter;

                parameter = command.Parameters.Add("@ErrorId", SqlDbType.UniqueIdentifier);
                parameter.Value = Guid.NewGuid();

                parameter = command.Parameters.Add("@Application", SqlDbType.NVarChar, 60);
                parameter.Value = this.ApplicationName;
                
                parameter = command.Parameters.Add("@Host", SqlDbType.NVarChar, 30);
                parameter.Value = error.HostName;

                parameter = command.Parameters.Add("@Type", SqlDbType.NVarChar, 100);
                parameter.Value = error.Type;
                
                parameter = command.Parameters.Add("@Source", SqlDbType.NVarChar, 60);
                parameter.Value = error.Source;

                parameter = command.Parameters.Add("@Message", SqlDbType.NVarChar, 500);
                parameter.Value = error.Message;
                
                parameter = command.Parameters.Add("@User", SqlDbType.NVarChar, 50);
                parameter.Value = error.User;

                parameter = command.Parameters.Add("@AllXml", SqlDbType.NText);
                parameter.Value = errorXml;

                parameter = command.Parameters.Add("@StatusCode", SqlDbType.Int);
                parameter.Value = error.StatusCode;
                
                parameter = command.Parameters.Add("@TimeUtc", SqlDbType.DateTime);
                parameter.Value = error.Time.ToUniversalTime();

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Returns a page of errors from the databse in descending order 
        /// of logged time.
        /// </summary>

        public override int GetErrors(int pageIndex, int pageSize, IList errorEntryList)
        {
            if (pageIndex < 0)
                throw new ArgumentOutOfRangeException("pageIndex");

            if (pageSize < 0)
                throw new ArgumentOutOfRangeException("pageSite");

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand("ELMAH_GetErrorsXml", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter;

                parameter = command.Parameters.Add("@Application", SqlDbType.NVarChar, 60);
                parameter.Value = this.ApplicationName;

                parameter = command.Parameters.Add("@PageIndex", SqlDbType.Int);
                parameter.Value = pageIndex;
                
                parameter = command.Parameters.Add("@PageSize", SqlDbType.Int);
                parameter.Value = pageSize;

                SqlParameter totalParameter = command.Parameters.Add("@TotalCount", SqlDbType.Int);
                totalParameter.Direction = ParameterDirection.Output;

                connection.Open();

                XmlReader reader = command.ExecuteXmlReader();

                try
                {
                    while (reader.IsStartElement("error"))
                    {
                        string id = reader.GetAttribute("errorId");
                        
                        Error error = NewError();
                        error.FromXml(reader);

                        if (errorEntryList != null)
                        {
                            errorEntryList.Add(new ErrorLogEntry(this, id, error));
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }

                return (int) totalParameter.Value;
            }
        }

        /// <summary>
        /// Returns the specified error from the database, or null 
        /// if it does not exist.
        /// </summary>

        public override ErrorLogEntry GetError(string id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            if (id.Length == 0)
                throw new ArgumentOutOfRangeException("id");

            Guid errorGuid;

            try
            {
                errorGuid = new Guid(id);
            }
            catch (FormatException e)
            {
                throw new ArgumentOutOfRangeException("id", id, e.Message);
            }

            using (SqlConnection connection = new SqlConnection(this.ConnectionString))
            using (SqlCommand command = new SqlCommand("ELMAH_GetErrorXml", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter;

                parameter = command.Parameters.Add("@Application", SqlDbType.NVarChar, 60);
                parameter.Value = this.ApplicationName;

                parameter = command.Parameters.Add("@ErrorId", SqlDbType.UniqueIdentifier);
                parameter.Value = errorGuid;
                
                connection.Open();

                string errorXml = (string) command.ExecuteScalar();

                StringReader errorStringReader = new StringReader(errorXml);
                XmlTextReader errorXmlReader = new XmlTextReader(errorStringReader);

                if (!errorXmlReader.IsStartElement("error"))
                {
                    throw new ApplicationException("The error XML is not in the expected format.");
                }

                Error error = NewError();
                error.FromXml(errorXmlReader);

                return new ErrorLogEntry(this, id, error);
            }
        }

        /// <summary>
        /// Creates a new and empty instance of the <see cref="Error"/> class.
        /// </summary>

        protected virtual Error NewError()
        {
            return new Error();
        }
    }
}
