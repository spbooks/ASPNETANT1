#region Byline & Disclaimer
//
//  Author(s):
//
//      Atif Aziz (atif.aziz@skybow.com, http://www.raboof.com)
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
    using System.Web.UI;
    using System.Web.UI.WebControls;

    using CultureInfo = System.Globalization.CultureInfo;
    using ArrayList = System.Collections.ArrayList;

	#endregion

    /// <summary>
    /// Renders an HTML page displaying a page of errors from the error log.
    /// </summary>

    internal sealed class ErrorLogPage : ErrorPageBase
	{
        private int _pageIndex;
        private int _pageSize; 
        private int _totalCount;
        private ArrayList _errorEntryList;
        
        private const int _defaultPageSize = 15;
        private const int _maximumPageSize = 100;

        protected override void OnLoad(EventArgs e)
        {
            //
            // Get the page index and size parameters within their bounds.
            //

            _pageSize = Convert.ToInt32(this.Request.QueryString["size"], CultureInfo.InvariantCulture);
            _pageSize = Math.Min(_maximumPageSize, Math.Max(0, _pageSize));

            if (_pageSize == 0)
            {
                _pageSize = _defaultPageSize;
            }

            _pageIndex = Convert.ToInt32(this.Request.QueryString["page"], CultureInfo.InvariantCulture);
            _pageIndex = Math.Max(1, _pageIndex) - 1;

            //
            // Read the error records.
            //

            _errorEntryList = new ArrayList(_pageSize);
            _totalCount = this.ErrorLog.GetErrors(_pageIndex, _pageSize, _errorEntryList);

            //
            // Set the title of the page.
            //

            this.Title = string.Format("Error log for {0} on {1} (Page {2})", 
                this.ApplicationName, Environment.MachineName, 
                (_pageIndex + 1).ToString());

            base.OnLoad(e);
        }

        protected override void RenderHead(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            base.RenderHead(writer);

            //
            // Write a <link> tag to relate the RSS feed.
            //

            writer.AddAttribute("rel", "alternate");
            writer.AddAttribute(HtmlTextWriterAttribute.Type, "application/rss+xml");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, "RSS");
            writer.AddAttribute(HtmlTextWriterAttribute.Href, this.BasePageName + "/rss");
            writer.RenderBeginTag(HtmlTextWriterTag.Link);
            writer.RenderEndTag();
            writer.WriteLine();

            //
            // If on the first page, then enable auto-refresh every minute
            // by issuing the following markup:
            //
            //      <meta http-equiv="refresh" content="60">
            //

            if (_pageIndex == 0)
            {
                writer.AddAttribute("http-equiv", "refresh");
                writer.AddAttribute("content", "60");
                writer.RenderBeginTag(HtmlTextWriterTag.Meta);
                writer.RenderEndTag();
                writer.WriteLine();
            }
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            //
            // Write out the page title in the body.
            //

            RenderTitle(writer);

            if (_errorEntryList.Count != 0)
            {
                //
                // Write error number range displayed on this page and the
                // total available in the log, followed by stock
                // page sizes.
                //

                writer.RenderBeginTag(HtmlTextWriterTag.P);

                RenderStats(writer);
                RenderStockPageSizes(writer);
                
                writer.RenderEndTag(); // </p>
                writer.WriteLine();

                //
                // Write out the main table to display the errors.
                //

                RenderErrors(writer);

                //
                // Write out page navigation links.
                //

                RenderPageNavigators(writer);
            }
            else
            {
                //
                // No errors found in the log, so display a corresponding
                // message.
                //

                RenderNoErrors(writer);
            }

            base.RenderContents(writer);
        }

	    private void RenderPageNavigators(HtmlTextWriter writer)
	    {
            Debug.Assert(writer != null);

	        //
	        // If not on the last page then render a link to the next page.
	        //
    
	        writer.RenderBeginTag(HtmlTextWriterTag.P);
    
            int nextPageIndex = _pageIndex + 1;
            bool moreErrors = nextPageIndex * _pageSize < _totalCount;
    
	        if (moreErrors)
	        {
	            RenderLinkToPage(writer, "Next errors", nextPageIndex);
	        }

            //
            // If not on the first page then render a link to the firs page.
            //
    
	        if (_pageIndex > 0 && _totalCount > 0)
	        {
	            if (moreErrors)
	                writer.Write("; ");

	            RenderLinkToPage(writer, "Back to first page", 0);
	        }
    
	        writer.RenderEndTag(); // </p>
            writer.WriteLine();
	    }

	    private void RenderStockPageSizes(HtmlTextWriter writer)
	    {
            Debug.Assert(writer != null);

	        //
	        // Write out a set of stock page size choices. Note that
	        // selecting a stock page size re-starts the log 
	        // display from the first page to get the right paging.
	        //
    
	        writer.Write("Start with ");
    
	        int[] stockSizes = new int[] { 10, 15, 20, 25, 30, 50, 100 };
    
	        for (int stockSizeIndex = 0; stockSizeIndex < stockSizes.Length; stockSizeIndex++)
	        {
	            int stockSize = stockSizes[stockSizeIndex];

	            if (stockSizeIndex > 0)
	            {
	                writer.Write(stockSizeIndex + 1 < stockSizes.Length ? ", " : " or ");
	            }
                    
	            RenderLinkToPage(writer, stockSize.ToString(), 0, stockSize);
	        }
    
	        writer.Write(" errors per page.");
	    }

	    private void RenderStats(HtmlTextWriter writer)
	    {
            Debug.Assert(writer != null);
            
            int firstErrorNumber = _pageIndex * _pageSize + 1;
	        int lastErrorNumber = firstErrorNumber + _errorEntryList.Count - 1;
            int totalPages = (int) Math.Ceiling((double) _totalCount / _pageSize);
    
	        writer.Write("Errors {0} to {1} of total {2} (page {3} of {4}). ",
	                     firstErrorNumber.ToString(), 
	                     lastErrorNumber.ToString(),
	                     _totalCount.ToString(),
                         (_pageIndex + 1).ToString(),
                         totalPages.ToString());
	    }

	    private void RenderTitle(HtmlTextWriter writer)
        {
            Debug.Assert(writer != null);

            //
            // If the application name matches the APPL_MD_PATH then its
            // of the form /LM/W3SVC/.../<name>. In this case, use only the 
            // <name> part to reduce the noise. The full application name is 
            // still made available through a tooltip.
            //

            string simpleName = this.ApplicationName;

            if (string.Compare(simpleName, this.Request.ServerVariables["APPL_MD_PATH"], 
                true, CultureInfo.InvariantCulture) == 0)
            {
                int lastSlashIndex = simpleName.LastIndexOf('/');

                if (lastSlashIndex > 0)
                {
                    simpleName = simpleName.Substring(lastSlashIndex + 1);
                }
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "PageTitle");
            writer.RenderBeginTag(HtmlTextWriterTag.P);
            writer.Write("Error Log for ");

            writer.AddAttribute(HtmlTextWriterAttribute.Id, "ApplicationName");
            writer.AddAttribute(HtmlTextWriterAttribute.Title, this.Server.HtmlEncode(this.ApplicationName));
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            Server.HtmlEncode(simpleName, writer);
            writer.Write(" on ");
            Server.HtmlEncode(Environment.MachineName, writer);
            writer.RenderEndTag(); // </span>

            writer.RenderEndTag(); // </p>
            writer.WriteLine();
        }

        private void RenderNoErrors(HtmlTextWriter writer)
        {
            Debug.Assert(writer != null);

            writer.RenderBeginTag(HtmlTextWriterTag.P);

            writer.Write("No errors found. ");

            //
            // It is possible that there are no error at the requested 
            // page in the log (especially if it is not the first page).
            // However, if there are error in the log
            //
            
            if (_pageIndex > 0 && _totalCount > 0)
            {
                RenderLinkToPage(writer, "Go to first page", 0);
                writer.Write(". ");
            }

            writer.RenderEndTag();
            writer.WriteLine();
        }

        private void RenderErrors(HtmlTextWriter writer)
        {
            Debug.Assert(writer != null);

            //
            // Create a table to display error information in each row.
            //

            Table table = new Table();
            table.ID = "ErrorLog";
            table.CellSpacing = 0;

            //
            // Create the table row for headings.
            //
            
            TableRow headRow = new TableRow();

            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Host", "host-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Code", "code-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Type", "type-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Error", "error-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "User", "user-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Date", "date-col"));
            headRow.Cells.Add(FormatCell(new TableHeaderCell(), "Time", "time-col"));

            table.Rows.Add(headRow);

            //
            // Generate a table body row for each error.
            //

            for (int errorIndex = 0; errorIndex < _errorEntryList.Count; errorIndex++)
            {
                ErrorLogEntry errorEntry = (ErrorLogEntry) _errorEntryList[errorIndex];
                Error error = errorEntry.Error;

                TableRow bodyRow = new TableRow();
                bodyRow.CssClass = errorIndex % 2 == 0 ? "even-row" : "odd-row";

                //
                // Format host and status code cells.
                //

                bodyRow.Cells.Add(FormatCell(new TableCell(), error.HostName, "host-col"));
                bodyRow.Cells.Add(FormatCell(new TableCell(), error.StatusCode.ToString(), "code-col"));
                bodyRow.Cells.Add(FormatCell(new TableCell(), GetSimpleErrorType(error), "type-col", error.Type));
                    
                //
                // Format the message cell, which contains the message 
                // text and a details link pointing to the page where
                // all error details can be viewed.
                //

                TableCell messageCell = new TableCell();
                messageCell.CssClass = "error-col";

                Label messageLabel = new Label();
                messageLabel.Text = this.Server.HtmlEncode(error.Message);

                HyperLink detailsLink = new HyperLink();
                detailsLink.NavigateUrl = this.Request.Path + "/detail?id=" + errorEntry.Id;
                detailsLink.Text = "[Details]";

                messageCell.Controls.Add(messageLabel);
                messageCell.Controls.Add(new LiteralControl(" "));
                messageCell.Controls.Add(detailsLink);

                bodyRow.Cells.Add(messageCell);

                //
                // Format the user, date and time cells.
                //
                    
                bodyRow.Cells.Add(FormatCell(new TableCell(), error.User, "user-col"));
                bodyRow.Cells.Add(FormatCell(new TableCell(), error.Time.ToShortDateString(), "date-col", 
                    error.Time.ToLongDateString()));
                bodyRow.Cells.Add(FormatCell(new TableCell(), error.Time.ToLongTimeString(), "time-col"));

                //
                // Finally, add the row to the table.
                //

                table.Rows.Add(bodyRow);
            }

            table.RenderControl(writer);
        }

        private TableCell FormatCell(TableCell cell, string contents, string cssClassName)
        {
            return FormatCell(cell, contents, cssClassName, string.Empty);
        }

        private TableCell FormatCell(TableCell cell, string contents, string cssClassName, string toolTip)
        {
            Debug.Assert(cell != null);
            Debug.AssertStringNotEmpty(cssClassName);

            cell.Wrap = false;
            cell.CssClass = cssClassName;

            if (contents.Length == 0)
            {
                cell.Text = "&nbsp;";
            }
            else
            {
                string encodedContents = this.Server.HtmlEncode(contents);
                
                if (toolTip.Length == 0)
                {
                    cell.Text = encodedContents;
                }
                else
                {
                    Label label = new Label();
                    label.ToolTip = toolTip;
                    label.Text = encodedContents;
                    cell.Controls.Add(label);
                }
            }

            return cell;
        }

        private string GetSimpleErrorType(Error error)
        {
            Debug.Assert(error != null);

            if (error.Type.Length == 0)
            {
                return string.Empty;
            }

            string simpleType = error.Type;

            int lastDotIndex = CultureInfo.InvariantCulture.CompareInfo.LastIndexOf(simpleType, '.');
                
            if (lastDotIndex > 0)
            {
                simpleType = simpleType.Substring(lastDotIndex + 1);
            }

            const string conventionalSuffix = "Exception";

            if (simpleType.Length > conventionalSuffix.Length)
            {
                int suffixIndex = simpleType.Length - conventionalSuffix.Length;
                
                if (string.Compare(simpleType, suffixIndex, conventionalSuffix, 0,
                    conventionalSuffix.Length, true, CultureInfo.InvariantCulture) == 0)
                {
                    simpleType = simpleType.Substring(0, suffixIndex);
                }
            }

            return simpleType;
        }

        private void RenderLinkToPage(HtmlTextWriter writer, string text, int pageIndex)
        {
            RenderLinkToPage(writer, text, pageIndex, _pageSize);
        }

        private void RenderLinkToPage(HtmlTextWriter writer, string text, int pageIndex, int pageSize)
        {
            Debug.Assert(writer != null);
            Debug.Assert(text != null);
            Debug.Assert(pageIndex >= 0);
            Debug.Assert(pageSize >= 0);

            string href = string.Format("{0}?page={1}&size={2}", 
                this.Request.Path,
                (pageIndex + 1).ToString(CultureInfo.InvariantCulture),
                pageSize.ToString(CultureInfo.InvariantCulture));

            writer.AddAttribute(HtmlTextWriterAttribute.Href, href);
            writer.RenderBeginTag(HtmlTextWriterTag.A);
            this.Server.HtmlEncode(text, writer);
            writer.RenderEndTag();
        }
    }
}
