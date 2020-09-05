using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb项目.Models
{
    public class MyWebDbContext: DbContext
    {
        public MyWebDbContext(DbContextOptions<MyWebDbContext> options) : base(options)
        {

        }
        public DbSet<student> student { get; set; }
    }
}
