using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eaas.Models
{
    public class Smtp 
    {
        public string server {get; set;}
        public int port {get; set;}
        public Smtp()
        {
            this.server = "smarthost.nodomain.com";
            this.port = 25;
        }
    }

    public static class ValidTokens
    {
        // these are not real tokens! the web service does not really need any kind of authentication. This is only here to minimize the possibility of the service being 'abused' if the Url gets known
        // If a decent security control is needed, it should be added
        public static string[] Valid = {
                "RYCIKQLSIYHD",
                "NSNZVSBYAWPQ",
                "JPXAFWEPCBSP",
                "KDDBFZFMPCZB",
                "PONFILPVYITQ"
            };
    } 
}