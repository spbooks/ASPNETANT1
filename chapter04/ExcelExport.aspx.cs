using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class ExcelExport : System.Web.UI.Page 
{
    GridView gridToExport = null;

    protected void btnExport_Click(object sender, EventArgs e)
    {
        gridToExport = grdProducts;
    }

    protected override void Render(HtmlTextWriter writer)
    {
        if (gridToExport as GridView != null)
            ExportGridToExcel(gridToExport, "Products.xls");

        base.Render(writer);
    }

    private void ExportGridToExcel(GridView grid, string filename)
    {
        if (string.IsNullOrEmpty(filename))
            throw new ArgumentException(
                "Export filename is required");
        if (!filename.EndsWith(".xls"))
            filename += ".xls";

        grid.AllowPaging = false;
        grid.AllowSorting = false;
        grid.DataBind();

        StringWriter tw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(tw);

        Response.Clear();
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader(
            "content-disposition",
            "attachment;filename=" + filename);
        Response.Charset = string.Empty;
        Page.EnableViewState = false;

        grid.RenderControl(hw);
        Response.Write(
            "<html xmlns:x=\"urn:schemas-microsoft-com:" +
            "office:excel\" >");
        Response.Write(GetExcelStyle(grid));
        Response.Write(tw.ToString());
        Response.End();
    }

    private string GetExcelStyle(GridView grid)
    {
        if(grid == grdProducts)
            return 
                "<style>" +
                    "excelCurrency{mso-number-format:" + 
                    "\"\\0022$\\0022\\#\\,\\#\\#0\\.00\";" + 
                "</style>";
        return string.Empty;
    }

    /// <summary>
    /// Need to override this to prevent checking that controls are
    /// in a webform, since we're rendering the gridview by itself.
    /// </summary>
    /// <param name="control"></param>
    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void grdProducts_RowDataBound(
        object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
            foreach (TableCell cell in e.Row.Cells)
                cell.Attributes.Add("x:autofilter", "all");
    }
}
