using System;
using System.Web.UI.WebControls;

public partial class MultiSorting : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.GridView1.Sorting += new GridViewSortEventHandler(GridView1_Sorting);
    }

    void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string currentExpression = GridView1.SortExpression;
        if (currentExpression.Length == 0)
            return; //First sort.

        //Want to keep the clicked sort expression in the front.
        string[] sortedColumns = currentExpression.Split(',');
        string newSortExpression = e.SortExpression;
        foreach (string sortExpression in sortedColumns)
        {
            if(sortExpression != e.SortExpression)
                newSortExpression += "," + sortExpression;
        }
    }
}
