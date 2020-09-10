﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoreAPI.Jwt;
using MyCoreAPI.Models;

namespace MyCoreAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ITokenHelper tokenHelper = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_tokenHelper"></param>
        public LoginController(ITokenHelper _tokenHelper)
        {
            tokenHelper = _tokenHelper;
        }
        [HttpPost]
        public ReturnModel Login([FromBody]UserDto user)
        {
            var ret = new ReturnModel();
            try
            {
                if (string.IsNullOrWhiteSpace(user.LoginID) || string.IsNullOrWhiteSpace(user.Password))
                {
                    ret.Code = 201;
                    ret.Msg = "用户名密码不能为空";
                    return ret;
                }
                //登录操作 我就没写了 || 假设登录成功
                if (1 == 1)
                {
                    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
                    {
                        { "loginID", user.LoginID }
                    };
                    ret.Code = 200;
                    ret.Msg = "登录成功";
                    ret.TnToken= tokenHelper.CreateToken(keyValuePairs);
                }
            }
            catch(Exception ex)
            {
                ret.Code = 500;
                ret.Msg = "登录失败:"+ex.Message;
            }
            return ret;
        }
    }
}
