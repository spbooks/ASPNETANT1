using System;

public class SendEmailEventArgs : EventArgs 
{
	public SendEmailEventArgs(string emailAddress)
	{
        _emailAddress = emailAddress;
	}

    private string _emailAddress;
    public string EmailAddress
    {
        get { return _emailAddress; }
        set { _emailAddress = value; }
    }
}

public delegate void SendEmailEventHandler(object sender, SendEmailEventArgs args);