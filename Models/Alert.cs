using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eaas.Models
{
    public class Alert 
    {
        [JsonProperty("recipient")]
        public string recipient {get; set;}
        [JsonProperty("cc")]
        public string cc {get; set;}
        [JsonProperty("sender")]
        public string sender {get; set;}
        [JsonProperty("priority")]
        public int priority {get; set;}
        [JsonProperty("html")]
        public int html {get; set;}
        [JsonProperty("subject")]
        public string subject {get; set;}
        [JsonProperty("body")]
        public string body {get; set;}
        [JsonProperty("token")]
        public string token {get; set;}
        
        public Alert()
        {
            this.sender = "Alert Monitor <noone@nodomain.com>";
            this.subject = "An alert has been invoked";
            this.recipient = string.Empty;
            this.cc = string.Empty;
            this.body = string.Empty;
            this.priority = 0;
            this.html = 0;
            this.token = string.Empty;
        }
    }
}