<%@ Control Language="C#" ClassName="joke5" %>

<div id="joke" onmouseover="jokeMouseOver(this);">
    <div>
        There are 10 kinds of people in this world ... 
    </div>
    <div id="answer" runat="server" style="display:none;">
        Those who can count in binary and those who can't. 
    </div>
</div>

<script type="text/javascript">
function jokeMouseOver(sender)
{                                                     
    var divs = sender.getElementsByTagName("DIV");    
    divs[1].style.display = '';           
}
</script>
