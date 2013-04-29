<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>Chapter 2: String.Format</title>
  <script runat="server">
      void Page_Load() {
          messageLabel.Text = "Hello World!";
      }
  </script>
  <style type="text/css">
  .highlight {
    background-color: yellow;
  }
  </style>
</head>
<body>
  <form id="Form2" runat="server">
    <p>
      <asp:Label id="messageLabel" runat="server" Text="Well hi there!" CssClass="highlight" />
    </p>
    <p>
      <% string CowboyText = String.Format("I like {1}, {0}, and {2}", "ninjas", "pirates", 
        "cowboys"); %>   
      <%= CowboyText %>
    </p>

    <p>
      <% string LeftText = "";
         LeftText = "|" + String.Format("{0,10}", "right") + "|"; %>
      <%= LeftText %>
    </p>

    <p>
      <% string RightText = "";
         RightText = "|" + String.Format("{0,-10}", "left") + "|"; %>
      <%= RightText %>   
    </p>
    
    <p>
      <% string PiText = "|" + String.Format("{0,-8:G2}", 3.14159) + "|"; %>
      <%= PiText %>
    </p>
    
    <p>
      <% string NumText = "|" + String.Format("{0:c2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:d}", 3245) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:e2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:f2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:g2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:n2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:p2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:r2}", 3245.434532) + "|"; %>
      <%= NumText %>
      <% NumText = "|" + String.Format("{0:x}", 3245) + "|"; %>
      <%= NumText %>
    </p>
    <p>
      <% double d = 1234.56; %>
      <% string s = "|" + String.Format("{0:00.0000}", d) + "|"; %>
      <%= s %>
      <% s = "|" + String.Format("{0:(#).##}", d) + "|"; // (1234).56 digit placeholder %>
      <%= s %>
      <% s = "|" + String.Format("{0:0.0}", d) + "|"; // 1234.6 decimal point %>
      <%= s %>
      <% s = "|" + String.Format("{0:0,0}", d) + "|"; // 1,235 thousands %>
      <%= s %>
      <% s = "|" + String.Format("{0:0,.}", d) + "|"; // 1 number scaling %>
      <%= s %>
      <% s = "|" + String.Format("{0:0%}", d) + "|"; // 123456% percent %>
      <%= s %>
      <% s = "|" + String.Format("{0:00e+0}", d) + "|"; // 12e+2 scientific %>
      <%= s %>
    </p>
    <p>
      <% string i = "|" + String.Format("{0:d}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:D}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:f}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:F}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:g}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:G}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:M}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:o}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:R}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:s}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:t}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:T}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:u}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:U}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:Y}", DateTime.Now) + "|"; %>
      <%= i %>
    </p>
    <p>
      <% i = "|" + String.Format("{0:m}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:mmm}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:M}", DateTime.Now) + "|"; %>
      <%= i %>
      <% i = "|" + String.Format("{0:MMM}", DateTime.Now) + "|"; %>
      <%= i %>
    </p>
    <p>
    <% int j = 235; %>
    <% string k = String.Format("{0:positive;negative;zero}", j); %>
    <%= k %>
    </p>
    <p>

    </p>
  </form>
</body>
</html>