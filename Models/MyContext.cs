using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfobridgeEx.Models
{
    public class MyContext :DbContext
    {
        public MyContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Student> Students  { get; set; }

       
    }
}
