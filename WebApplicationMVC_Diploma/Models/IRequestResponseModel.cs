using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC_Diploma.Models
{
    public interface IRequestResponseModel
    {
        public bool GetRequest(out string[] result);
        public void AddResult(KeyValuePair<string[], string[]> message);
        public void AddRequest(string[] data);
        public bool GetResult(out KeyValuePair<string[], string[]> data);
        public string SearchTerm { get; set; }
        public List<string> ResponseList { get; set; }
    }
}
