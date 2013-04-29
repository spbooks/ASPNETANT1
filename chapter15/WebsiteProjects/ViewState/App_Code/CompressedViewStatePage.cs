using System;
using System.IO.Compression;
using System.IO;
using System.Web.UI;

public class CompressedViewStatePage : System.Web.UI.Page
{
  static public byte[] Compress(byte[] b)
  {
    MemoryStream ms = new MemoryStream();
    GZipStream zs = new GZipStream(ms, CompressionMode.Compress);
    zs.Write(b, 0, b.Length);
    return ms.ToArray();
  }

  static public byte[] Decompress(byte[] b)
  {
    MemoryStream ms = new MemoryStream(b.Length);
    ms.Write(b, 0, b.Length);

    // last 4 bytes of GZipStream = length of decompressed data
    ms.Seek(-4, SeekOrigin.Current);
    byte[] lb = new byte[4];
    ms.Read(lb, 0, 4);
    int len = BitConverter.ToInt32(lb, 0);
    ms.Seek(0, SeekOrigin.Begin);

    byte[] ob = new byte[len];
    GZipStream zs = new GZipStream(ms, CompressionMode.Decompress);
    zs.Read(ob, 0, len);

    return ob;
  }

  protected override object LoadPageStateFromPersistenceMedium()
  {
    byte[] b = Convert.FromBase64String(Request.Form["__VSTATE"]);
    LosFormatter lf = new LosFormatter();
    return lf.Deserialize(Convert.ToBase64String(Decompress(b)));
  }

  protected override void SavePageStateToPersistenceMedium(object state)
  {
    LosFormatter lf = new LosFormatter();
    StringWriter sw = new StringWriter();
    lf.Serialize(sw, state);
    byte[] b = Convert.FromBase64String(sw.ToString());
    ClientScript.RegisterHiddenField("__VSTATE", Convert.ToBase64String(Compress(b)));
  }

}
