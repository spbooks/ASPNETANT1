using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class Simple : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string connectString = 
                @"server=.\SQLEXPRESS;database=Orders;Trusted_Connection=yes";             

            using (SqlConnection connection = new SqlConnection(connectString))
            using (SqlCommand sqlCommand = new SqlCommand())
            {
                connection.Open();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "SELECT * FROM Orders";

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    // party on the data
                }
            }
        }
    }
}
