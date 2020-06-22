using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationMVC_Diploma.Models
{
    public interface ISqlModel
    {
        bool GetRequest(out string[] result);
        void AddResult(Entities.Dictionary result);
        void AddRequest(string[] data);
        bool GetResult(out KeyValuePair<string[], string[]> data);
        string SearchTerm { get; set; }
        List<string> ResponseList { get; set; }
        int Commit();
        void Clear();
    }
}
