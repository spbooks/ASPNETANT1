using System;

namespace chapter_12_rendering_binary_content
{
    public partial class ExcelTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "image/gif";

            //Binary data as a base64 encoded string.
            string encoded = "R0lGODdhFAAUAIACABMT0kJCWiwAA"
                          + "AAAFAAUAAACKISPocvowGJ4S"
                          + "S567MVQT+59WMh1WkmCHrq"
                          + "qp2ux79jSM5XaMSzJVgEAOw==";

            // convert to raw bytes.
            byte[] binary = Convert.FromBase64String(encoded);
            
            Response.BinaryWrite(binary);
            Response.Flush();            
        }
    }
}
