using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using eaas.Models;
using System.Net.Mail;

namespace eaas.Actions
{
    public static class Email 
    {
        public static string sendError {get; set;}
        public static bool Send(Alert alert)
        {
            try
            {
                Smtp smtpconfig = new Smtp();
                SmtpClient client = new SmtpClient(smtpconfig.server);
        
                MailMessage email = new MailMessage(alert.sender, alert.recipient);
                if (alert.cc != string.Empty) 
                {
                    foreach (string address in alert.cc.Split(',')) 
                    {
                        email.CC.Add(address);
                    }
                }
                email.Subject = alert.subject;
                switch (alert.html) {
                    case 0:
                        email.IsBodyHtml = false;
                        break;
                    case 1:
                        email.IsBodyHtml = true;
                        break;
                    default:
                        email.IsBodyHtml = false;
                        break;
                }
                email.Body = alert.body;
                if (alert.priority == 1) 
                {
                    email.Priority = MailPriority.High;
                }
                
                client.Send(email);
                return true;
            }
            catch (System.Exception e)
            {
                Email.sendError = e.Message;
                return false;
            }
        } 
    }
}