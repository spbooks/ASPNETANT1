function ContentManager() 
{    
    this.updateServerTime = function()
    {        
        xmlHttp = new XMLHttpRequest();
        xmlHttp.onreadystatechange = onreadystatechanged;
        xmlHttp.open("GET", "../ServerTime.ashx", true);        
        xmlHttp.send(); 
        return false; 
    }  
      
    var onreadystatechanged = function()
    {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200) 
        {		
	        updateContent(xmlHttp.responseText);
	    }         
    }

    var updateContent = function(text)
    {              
        content.innerHTML = text;
    }   
    
    var xmlHttp;    
    var content = document.getElementById('content');
}
