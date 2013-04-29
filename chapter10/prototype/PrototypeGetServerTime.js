var contentManager;
Event.observe(window, 'load', windowLoad);

function windowLoad()
{
    contentManager = new ContentManager();
    contentManager.updateServerTime();
    Event.observe($('getContentButton'), 'click', getContent);
}

function getContent()
{
    contentManager.updateServerTime();
}

var ContentManager = Class.create();
ContentManager.prototype.initialize = function()
{
    this.content = $('content');
}

ContentManager.prototype.updateServerTime = function()
{
    var ajax = new Ajax.Request(
                "../ServerTime.ashx",
                {
                    method: 'get', 				    
				    onComplete: 
				      function(response) 
				      { 
				        contentManager.updateContent(response);
				      } 
                });
}

ContentManager.prototype.updateContent = function(response)
{
    this.content.innerHTML = response.responseText;
}

