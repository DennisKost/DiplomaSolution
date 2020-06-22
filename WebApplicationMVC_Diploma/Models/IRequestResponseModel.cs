using System.Collections.Generic;

namespace WebApplicationMVC_Diploma.Models
{
    public interface IRequestResponseModel
    {
        bool GetRequest(out string[] result);
        void AddResult(KeyValuePair<string[], string[]> message);
        void AddRequest(string[] data);
        bool GetResult(out KeyValuePair<string[], string[]> data);
        string SearchTerm { get; set; }
        List<string> ResponseList { get; set; }
        int Commit();
    }
}
