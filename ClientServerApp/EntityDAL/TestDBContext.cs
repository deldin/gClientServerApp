using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;

namespace EntityModel
{
    public class TestDBContext : DbContext
    {
         public TestDBContext()
        {

        }

         public DbSet<LogItem> LogItem { get; set; }
    }
}
