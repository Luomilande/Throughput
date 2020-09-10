using Microsoft.EntityFrameworkCore;
using MyCoreAPI.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Models
{
    public class APIContext: DbContext
    {
        public APIContext(DbContextOptions<APIContext> options)
    : base(options)
        {
        }

        public DbSet<UserInfo> UserInfos { get; set; }
    }
}
