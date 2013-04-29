<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        string northwindConnection =
            System.Web.Configuration.WebConfigurationManager.ConnectionStrings["NorthwindConnectionString1"].ConnectionString;
        System.Data.SqlClient.SqlDependency.Start(northwindConnection);
    }
</script>
