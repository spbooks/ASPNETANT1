using System;
using System.Web.UI;

public partial class UserControls_Default : 
                        Page, ISearchTermSource
{
    public string SearchTerm
    {
        get { return Header.SearchTerm; }
    }
}
