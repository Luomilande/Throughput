using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCoreAPI.Models;
using MyCoreAPI.User;

namespace MyCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private APIContext _context;
        public UserController(APIContext context)
        {
            _context = context;
        }
        public JsonResult GetUserInfo(int num)
        {
            for(int i = 0; i <= num; i++)
            {
                _context.UserInfos.Add(new UserInfo()
                {
                    Name = "张三",
                    Age = 12,
                    Password = "****",
                    Phone = "17677402334",
                    Update_Time = "2020-12-11"

                });
            }
            _context.SaveChanges();
            return Json(_context.SaveChanges());
        }
        public async Task<JsonResult> Addinfo()
        {
            await _context.UserInfos.AddAsync(new UserInfo()
            {
                Name = "张三",
                Age = 12,
                Password = "****",
                Phone = "17677402334",
                Update_Time = "2020-12-11"

            });
            _context.SaveChanges();
            return Json(_context.UserInfos.Take(4).ToList());
        }

        public string LogOn(LogOn info)
        {
            string tip = "用户名:" + info.username + ",密码:" + info.password;
            return "1";
        }

    }
}
