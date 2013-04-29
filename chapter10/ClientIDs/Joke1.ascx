<%@ Control Language="C#" AutoEventWireup="true"  %>
<div onmouseover="jokeMouseOver();">
    <div>
        What does a proud computer call his son?
    </div>
    <div id="answer" runat="server" style="display:none;">
        A microchip off the old block..
    </div>
</div>

<script type="text/javascript">
function jokeMouseOver()
{
    $get('Joke1_answer').style.display = '';    
}
</script>

