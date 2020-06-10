using System;
using System.Collections.Generic;

namespace WebApplicationMVC_Diploma.Models
{
    public class SqlRequestResponseModel : ISqlModel
    {
        private readonly WebAppDbContext db;

        public SqlRequestResponseModel(WebAppDbContext db)
        {
            this.db = db;
        }

        public string SearchTerm { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> ResponseList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddRequest(string[] data)
        {
            throw new NotImplementedException();
        }

        public void AddResult(Entities.Dictionary result)
        {
            db.Add(result);
        }

        public void Clear()
        {
            db.Dictionary.RemoveRange(db.Dictionary);
        }

        public int Commit()
        {
            return db.SaveChanges();
        }

        public bool GetRequest(out string[] result)
        {
            throw new NotImplementedException();
        }

        public bool GetResult(out KeyValuePair<string[], string[]> data)
        {
            throw new NotImplementedException();
        }
    }
}
