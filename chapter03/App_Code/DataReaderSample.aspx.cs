using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

public partial class DataReaderDemo
{
  public static void Main()
  {
    List<string> products = GetProductList();
    products.ForEach(delegate(String name)
      {
        Console.WriteLine(name);
      }
    );
  }
  public static List<string> GetProductList()
  {
    List<string> products = new List<string>();
    string connectionString =
      ConfigurationManager.
      ConnectionStrings["NorthwindConnectionString"].
      ConnectionString;
    string query = "SELECT * FROM Products";
    using(SqlConnection connection =
      new SqlConnection(connectionString))
    using(SqlCommand command = new SqlCommand(query,connection))
    {
      connection.Open();
      IDataReader dr =
        command.ExecuteReader(
          CommandBehavior.CloseConnection
          );
      while (dr.Read())
      {
          products.Add(dr["ProductName"].ToString());
      }
    }
    return products;
  }
}