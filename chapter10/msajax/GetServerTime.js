var contentManager;

function pageLoad()
{
    contentManager = new SitePoint.ContentManager();
    contentManager.updateServerTime();
    $addHandler($get('getContentButton'), 'click', getContent);
}

function getContent()
{
    contentManager.updateServerTime();
}

Type.registerNamespace("SitePoint");

SitePoint.ContentManager = function()
{
    this.content = $get('content');
}

SitePoint.ContentManager.prototype.updateServerTime = function()
{
    ServerTime.GetServerTime
        (
            function(result)
            {
                contentManager.updateContent(result);
            }        
        );
}

SitePoint.ContentManager.prototype.updateContent = function(text)
{
    this.content.innerHTML = text;
}

SitePoint.ContentManager.registerClass('SitePoint.ContentManager');

if (typeof(Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();