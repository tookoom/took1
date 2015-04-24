using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TK1.Bizz;
using TK1.Bizz.Net;
using TK1.Data.Bizz.Client.Controller;
using TK1.Data.Bizz.Client.Model;

namespace App.ConsoleDev4.Tests
{
    public class MailTests
    {
        public void SendTK1Mail()
        {
            var mail = new MailHelper();
            mail.SendMail("DevTests", "DevTests: MailTests.SendTK1Mail()", "andre.v.mattos@gmail.com", false);
        }
    }
}
