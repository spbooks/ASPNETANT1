using System;
using System.Drawing;
using System.Web.UI.WebControls;

namespace SitePoint.Cookbook.GridViews
{
    public class SortableGridView : GridView
    {
        protected override void OnLoad(EventArgs e)
        {
            AllowSorting = true;
            if (!Page.IsPostBack)
            {
                foreach (DataControlField column in Columns)
                {
                    if (ViewState[column.SortExpression] == null)
                        ViewState[column.SortExpression] = column.HeaderText;
                }
            }

            base.OnLoad(e);
        }

        protected override void OnSorting(GridViewSortEventArgs e)
        {
            foreach (DataControlField column in Columns)
            {
                if (column.SortExpression == e.SortExpression)
                {
                    column.HeaderStyle.CssClass = "sorted";
                    column.HeaderStyle.BackColor = Color.Khaki;

                    if (e.SortDirection == SortDirection.Descending)
                        column.HeaderText = ViewState[column.SortExpression] + " [asc]";
                    else
                        column.HeaderText = ViewState[column.SortExpression] + " [desc]";
                }
                else
                {
                    if (ViewState[column.SortExpression] != null)
                        column.HeaderText = ViewState[column.SortExpression] as string;
                    column.HeaderStyle.CssClass = "";
                    column.HeaderStyle.BackColor = Color.White;
                }
            }

            base.OnSorting(e);
        }
    }   
}
