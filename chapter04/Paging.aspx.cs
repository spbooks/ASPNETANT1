using System;
using System.Web.UI.WebControls;

public partial class Paging : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GridView1.DataBound += GridView1_DataBound;
        GridViewRow row = GridView1.BottomPagerRow;
        if (row == null) return;

        DropDownList pages = (DropDownList)row.Cells[0].FindControl("pages");
        pages.SelectedIndexChanged += OnSelectedIndexChanged;
    }

    private void GridView1_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = GridView1.BottomPagerRow;
        if (row == null) return;

        // get your controls from the gridview
        DropDownList pages = (DropDownList)row.Cells[0].FindControl("pages");
        Label count = (Label)row.Cells[0].FindControl("count");

        if (pages != null)
        {
            // populate pager
            for (int i = 0; i < GridView1.PageCount; i++)
            {
                int pageNumber = i + 1;
                ListItem pageItem = new ListItem(pageNumber.ToString());

                if (i == GridView1.PageIndex)
                    pageItem.Selected = true;

                pages.Items.Add(pageItem);
            }
        }

        // populate page count
        if (count != null)
            count.Text = string.Format("<strong>{0}</strong>", GridView1.PageCount);

        LinkButton prev = (LinkButton)row.Cells[0].FindControl("prev");
        LinkButton next = (LinkButton)row.Cells[0].FindControl("next");
        LinkButton first = (LinkButton)row.Cells[0].FindControl("first");
        LinkButton last = (LinkButton)row.Cells[0].FindControl("last");

        //Set the pager nav state based on the current page.
        if (GridView1.PageIndex == 0)
        {
            prev.Enabled = false;
            first.Enabled = false;
        }
        else if (GridView1.PageIndex + 1 == GridView1.PageCount)
        {
            last.Enabled = false;
            next.Enabled = false;
        }
        else
        {
            last.Enabled = true;
            next.Enabled = true;
            prev.Enabled = true;
            first.Enabled = true;
        }
    }

    protected void OnSelectedIndexChanged(Object sender, EventArgs e)
    {
        GridViewRow pager = GridView1.BottomPagerRow;
        DropDownList pages = (DropDownList)pager.Cells[0].FindControl("pages");

        GridView1.PageIndex = pages.SelectedIndex;

        // a method to populate your grid
        GridView1.DataBind();
    }

}