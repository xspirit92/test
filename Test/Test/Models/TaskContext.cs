using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using Test.Models;

namespace Test
{
    public class TaskContext: DbContext
    {
        public DbSet<Tasks> Tasks { set; get; }
    }
}