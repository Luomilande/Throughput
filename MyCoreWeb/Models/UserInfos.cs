using System;
using System.Collections.Generic;

namespace MyCoreWeb.Models
{
    public partial class UserInfos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Phone { get; set; }
    }
}
