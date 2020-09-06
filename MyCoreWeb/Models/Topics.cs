using System;
using System.Collections.Generic;

namespace MyCoreWeb.Models
{
    public partial class Topics
    {
        public Topics()
        {
            Posts = new HashSet<Posts>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual ICollection<Posts> Posts { get; set; }
    }
}
