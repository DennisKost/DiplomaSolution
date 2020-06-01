using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp_NetCore.Models
{
    public class RequestResponse
    {
        public int id { get; set; }
        public string[] request { get; set; }
        public string[] response { get; set; }
    }
}
