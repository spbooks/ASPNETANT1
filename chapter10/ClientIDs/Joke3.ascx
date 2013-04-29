<%@ Control Language="C#" AutoEventWireup="true"  %>
<div onmouseover="jokeMouseOver();">
    <div>
        My software never has bugs ... 
    </div>
    <div id="answer" runat="server" style="display:none;">
        It just develops random features.
    </div>
</div>

<script type="text/javascript">
// internal
var answerId = '<%= answer.ClientID %>';

// external
function jokeMouseOver()
{
    $get(answerId).style.display = '';    
}
</script>