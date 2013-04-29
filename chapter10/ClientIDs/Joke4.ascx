<%@ Control Language="C#" AutoEventWireup="true" CodeFile="joke4.ascx.cs" Inherits="ClientIDs_joke4" %>
<div id="joke" onmouseover="jokeMouseOver();">
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
    Sys.Debug.assert(form1.answerId.value.length > 0);    
    Sys.Debug.assert(answerIds.length > 0);


    // retrieve id from hidden field
    var id = form1.answerId.value;
    $get(id).style.display = '';    
    
    // retrieve ids from array declaration
    for(var i = 0; i < answerIds.length; i++)
    {
        $get(answerIds[i]).style.display = '';    
    }                                              
}
</script>