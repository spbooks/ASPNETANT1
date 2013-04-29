<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OutputCache.aspx.cs" Inherits="chapter_15_performance.Performance.OutputCache" %>

<%@ OutputCache Duration="30" VaryByParam="none" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Output Cache Example</title>

    <script type="text/javascript">
        var d = new Date();
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <!-- Pausing 5 seconds to simulate a database hit -->
        <% System.Threading.Thread.Sleep(5000); %>
        <h1>
            Output Cache Example</h1>
        <div>
            Time written by ASP.NET:
            <%= DateTime.Now.ToLongTimeString() %>
        </div>
        <div>
            Time written by Javascript:

            <script type="text/javascript">
            document.write(d.toLocaleTimeString())
            </script>

        </div>
        <div id="divCached" style="margin-top: 25px; width: 300px;">

            <script type="text/javascript">
            var aspTime = new Date();
            aspTime.setSeconds(<%= DateTime.Now.Second %>);
            
            // If there are more than two seconds difference 
            // between the ASP.NET render and the javascript 
            // evaluation, the page is almost certainly cached.
            if(Math.abs(d - aspTime) > 2000)
            {
                document.write('Probably Cached');
                document.getElementById("divCached")
                    .style.backgroundColor = "Coral";
            }
            else
            {
                document.write('Not Cached');
                document.getElementById("divCached")
                    .style.backgroundColor = "Aqua";
            }
            </script>

        </div>
    </form>
</body>
</html>
