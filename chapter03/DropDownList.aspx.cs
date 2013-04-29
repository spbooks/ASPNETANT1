using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void OnSelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Write("You picked " + ddlProducts.SelectedItem.Text);
    }

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!Page.IsPostBack)
    //    {
    //        string connectionString =
    //          ConfigurationManager.
    //          ConnectionStrings["NorthwindConnectionString"].
    //          ConnectionString;
    //        string query = "SELECT * FROM Products";
    //        using (SqlConnection connection =
    //          new SqlConnection(connectionString))
    //        using (SqlCommand command = new SqlCommand(query, connection))
    //        {
    //            connection.Open();
    //            IDataReader dr =
    //              command.ExecuteReader(CommandBehavior.CloseConnection);
    //            ddlProducts.DataSource = dr;
    //            ddlProducts.DataValueField = "ProductID";
    //            ddlProducts.DataTextField = "ProductName";
    //            ddlProducts.DataBind();
    //        }
    //    }
    //    else
    //        Response.Write("You picked " + ddlProducts.SelectedItem.Text);
    //}
}
