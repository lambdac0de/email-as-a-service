using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using eaas.Models;
using eaas.Actions;

namespace eaas.Controllers
{
    [Route("/")]
    public class  HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(string sender = null, string cc = null, string recipient = null, string subject = null, string body = null, int priority = 0, int html = 0, string token = null)
        {
            Alert alert = new Alert();
            if (token == null || ValidTokens.Valid.Contains(token) == false ) 
            {   
                return this.Content(JsonConvert.SerializeObject("Error: Invalid token specified"), "application/json");
            }
            if (recipient == null || recipient == String.Empty) 
            {
                return this.Content(JsonConvert.SerializeObject("Error: No email recipient was specified"), "application/json");
            }
            else 
            {
                alert.recipient = recipient;
            }
            if (body == null || body == String.Empty) 
            {
                return this.Content(JsonConvert.SerializeObject("Error: No email body was specified"), "application/json");
            }
            else 
            {
                alert.body = body;
            }
            if (sender != null) { alert.sender = sender; }
            if (cc != null) { alert.cc = sender; }
            if (subject != null) { alert.subject = subject; }
            if (priority != 0)  { alert.priority = priority; }
            if (html != 0)  { alert.html = html; }

            bool result = Email.Send(alert);
            if (result == true) {
                return this.Content(JsonConvert.SerializeObject("Success"), "application/json");
            }
            else {
                return this.Content(JsonConvert.SerializeObject("ERROR: " + Email.sendError), "application/json");
            }
        }

        [HttpPost]
        public ActionResult Index([FromBody] string body)
        {
            Alert alert = new Alert();
            try {
                alert = JsonConvert.DeserializeObject<Alert>(body);
            }
            catch (Exception e) {
                return this.Content(JsonConvert.SerializeObject("ERROR: Invalid JSON format. " + e.Message), "application/json");
            }
            if (alert.token == null || ValidTokens.Valid.Contains(alert.token) == false ) 
            {   
                return this.Content(JsonConvert.SerializeObject("Error: Invalid token specified"), "application/json");
            }

            bool result = Email.Send(alert);
            if (result == true) {
                return this.Content(JsonConvert.SerializeObject("Success"), "application/json");
            }
            else {
                return this.Content(JsonConvert.SerializeObject("ERROR: TO " + alert.recipient + alert.sender + alert.body + Email.sendError), "application/json");
            }
        }
    }
}