using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCoreAPI.User;

namespace MyCoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        public JsonResult GetUserInfo()
        {
            return Json(new List<UserInfo> {
            new UserInfo
            {
                Id=1,
                Name="张三",
                Age=12,
                Password="****",
                Phone="17677402334",
                UpdateTime=DateTime.Now
            },
            new UserInfo
            {
                Id=2,
                Name="张四",
                Age=22,
                Password="******",
                Phone="176774026544",
                UpdateTime=DateTime.Now
            }
            });
        }
    }
}
