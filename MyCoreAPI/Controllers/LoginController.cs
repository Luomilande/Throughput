﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCoreAPI.Filter;
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





        /// <summary>
        /// 验证Token
        /// </summary>
        /// <param name="tokenStr">token</param>
        /// <returns></returns>
        [HttpGet]
        public ReturnModel ValiToken(string tokenStr)
        {
            var ret = new ReturnModel
            {
                TnToken = new TnToken()
            };
            bool isvilidate = tokenHelper.ValiToken(tokenStr);
            if (isvilidate)
            {
                ret.Code = 200;
                ret.Msg = "Token验证成功";
                ret.TnToken.TokenStr = tokenStr;
            }
            else
            {
                ret.Code = 500;
                ret.Msg = "Token验证失败";
                ret.TnToken.TokenStr = tokenStr;
            }
            return ret;
        }
        /// <summary>
        /// 验证Token 带返回状态
        /// </summary>
        /// <param name="tokenStr"></param>
        /// <returns></returns>
        [HttpGet]
        [ServiceFilter(typeof(TokenFilter))]
        public ReturnModel ValiTokenState()
        {
            string tokenStr=Request.Headers["Authorization"];

            var ret = new ReturnModel
            {
                TnToken = new TnToken()
            };
            string loginID = "";
            TokenType tokenType = tokenHelper.ValiTokenState(tokenStr, a => a["iss"] == "WYY" && a["aud"] == "EveryTestOne", action => { loginID = action["loginID"]; });
            if (tokenType == TokenType.Fail)
            {
                ret.Code = 202;
                ret.Msg = "token验证失败";
                return ret;
            }
            if (tokenType == TokenType.Expired)
            {
                ret.Code = 205;
                ret.Msg = "token已经过期";
                return ret;
            }

            //..............其他逻辑
            var data = new List<Dictionary<string, string>>();
            var bb = new Dictionary<string, string>
            {
                { "Wyy", "123456" }
            };
            data.Add(bb);
            ret.Code = 200;
            ret.Msg = "访问成功!";
            ret.Data = data;
            return ret;
        }
    }
}
