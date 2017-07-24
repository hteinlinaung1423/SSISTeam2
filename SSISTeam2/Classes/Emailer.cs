using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace SSISTeam2.Classes
{
    public class Emailer
    {
        string fromEmail, fromName;
        string loginAddress = "sa44ssisteamtwo@gmail.com";
        string loginPassword = "ssisteamtwo";

        public Emailer (string fromEmail, string fromName)
        {
            this.fromEmail = fromEmail;
            this.fromName = fromName;
        }
        public void SendEmail(string toEmail, string toName, string subject, string body)
        {
            var fromAddress = new MailAddress(fromEmail, fromName);
            var toAddress = new MailAddress(toEmail, toName);

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(loginAddress, loginPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }

            //SmtpClient client = new SmtpClient("smtp.nus.edu.sg", 587);
            //client.Credentials = new System.Net.NetworkCredential(@"e0167136", "swissbutchery25*");
            //client.EnableSsl = true;
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //MailMessage mm = new MailMessage("e0167136@u.nus.edu", "a0105465@u.nus.edu");
            //mm.Subject = "test subject";
            //mm.Body = "test body";
            //client.Send(mm);

        }
    } 
}