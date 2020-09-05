using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb项目.Models
{
    public class student
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int s_number { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string s_name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public bool sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime birthday { get; set; }
        public string native { get; set; }
        public string c_number { get; set; }
        public string dep_number { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string zipcode { get; set; }
        public string email { get; set; }
        public string note { get; set; }
    }
}
