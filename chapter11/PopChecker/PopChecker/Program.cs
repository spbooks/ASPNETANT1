using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace PopChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            CheckInbox();
        }

        static void CheckInbox()
        {
            Pop3.Pop3MailClient p = new Pop3.Pop3MailClient(
                "pop.gmail.com", 995, true, "someone@gmail.com", "password");
            p.IsAutoReconnect = true;
            p.ReadTimeout = 60000;
            p.Connect();
            int mailcount;
            int size;
            string email;
            p.GetMailboxStats(out mailcount, out size);
            for (int i = mailcount; i > 0; i--)
            {
                if (p.GetEmailSize(i) < 131072)
                {
                    p.GetRawEmail(i, out email);
                    if (MatchesSubject(email, "subcription change"))
                    {
                        if (MatchesBody(email, "unsubscribe"))
                        {
                            // do something with the email here..
                            p.DeleteEmail(i);
                        }
                    }
                }
            }
            p.Disconnect();
        }

        static Boolean MatchesSubject(string email, string subject)
        {
            return Regex.IsMatch(email, @"^subject:\s.*" + subject + ".*$",
              RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }

        static Boolean MatchesBody(string email, string text)
        {
            // the body starts after the first blank line
            int bodystart = Regex.Matches(email, "^\r\n",
              RegexOptions.Multiline)[0].Index + 2;
            string body = email.Substring(bodystart);
            return Regex.IsMatch(body, text, RegexOptions.IgnoreCase);
        }
    }
}
