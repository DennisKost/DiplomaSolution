using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApp_NetCore.Models
{
    public class AppContext : DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();
        }
        //public DbSet<Data> Data { get; set; }
    }
}
