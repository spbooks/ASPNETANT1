using System;

namespace chapter_12_rendering_binary_content
{
    public partial class HelloWorld : System.Web.UI.Page
    {
protected void Page_Load(object sender, EventArgs e)
{
    Byte[] binaryData = new byte[] 
        {   
            0x48, 0x65, 0x6C, 0x6C
            , 0x6F, 0x20, 0x57, 0x6F
            , 0x72, 0x6C, 0x64, 0x21
        };
    Response.OutputStream.Write(binaryData, 0, binaryData.Length);
}
    }
}
