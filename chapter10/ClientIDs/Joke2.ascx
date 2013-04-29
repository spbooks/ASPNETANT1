<%@ Control Language="C#" AutoEventWireup="true"  %>
<div onmouseover="jokeMouseOver();">
    <div>
        Why do computer programmers confuse Halloween and Christmas?
    </div>
    <div id="answer" runat="server" style="display:none;">
        Because oct31 = dec25.
    </div>
</div>

<script type="text/javascript">
function jokeMouseOver()
{
    $get('<%= answer.ClientID %>').style.display = '';    
}
</script>