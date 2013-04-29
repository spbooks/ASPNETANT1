var xmlHttp;

function getServerTime() 
{
    xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = onReadyStateChange;
    xmlHttp.open("GET", "../ServerTime.ashx", true);
    xmlHttp.send();
}

function onReadyStateChange()
{    
    if (xmlHttp.readyState == 4 && xmlHttp.status == 200) 
    {		
	    updateContent(xmlHttp.responseText);	    
	}	
}

function updateContent(text)
{
    var content = document.getElementById('content');
    content.innerHTML =  text;
}
 