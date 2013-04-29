using System;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Web.UI.WebControls;
using System.Data;

public partial class Daab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string command = "SELECT * FROM Orders";
        Database db = DatabaseFactory.CreateDatabase("OrderDB");

        using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, command))
        {
            GridView1.DataSource = dataReader;
            GridView1.DataBind();
        }
    }
}

//using System.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data;

//// ...

//    string command = "SELECT * FROM Orders";
//    Database db = DatabaseFactory.CreateDatabase("OrderDB");

//     using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, command))
//     {
//        // party on the data
//     }
