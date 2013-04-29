<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script runat="server">
		protected virtual void Page_Load(object sender, EventArgs e)
		{
			if (Request.QueryString != null && Request.QueryString[0] != null)
			{
				string url = Request.QueryString[0];

				// Note: The following is true for 404
                                // requests that are NOT mapped to ASP.NET
                                // IIS will send the string "404;intendedurl"
                                // in the query string.
                                int semiColonIndex = queryString.IndexOf(";");

                                // The following line works whether
                                // there's a semicolon or not.
                                string url = queryString.Substring(semiColonIndex + 1);

                                Trace.Write("Rewriting","Querystring = " + queryString);
                                Trace.Write("Rewriting","Intended URL = " + url);
				qry.Text = url;
			}
		}
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
		<asp:Literal ID="qry" runat="server" />
    </div>
    </form>
</body>
</html>
