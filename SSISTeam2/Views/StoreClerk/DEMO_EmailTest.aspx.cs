﻿using SSISTeam2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSISTeam2.Views.StoreClerk
{
    public partial class DEMO_EmailTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var fromAddress = "sa44ssisteamtwo+sender@gmail.com";
            string fromName = "From Name";
            var toAddress = "sa44ssisteamtwo+receive@gmail.com";
            string toName = "To Name";
            const string subject = "test mail";
            const string body = "This is some message again";

            new Emailer(fromAddress, fromName)
                .SendEmail(toAddress, toName, subject, body);
        }
    }
}