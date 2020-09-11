using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using MyCoreAPI.Jwt;
using MyCoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI.Filter
{
    public class TokenFilter : Attribute, IActionFilter
    {
        private ITokenHelper _tokenHelper;
        private readonly JWTConfig _options;
        public TokenFilter(ITokenHelper tokenHelper, IOptions<JWTConfig> options) //通过依赖注入得到数据访问层实例
        {
            _tokenHelper = tokenHelper;
            _options = options.Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ReturnModel ret = new ReturnModel();
            //获取token
            //object tokenobj = context.ActionArguments["token"];
            object tokenobj= context.HttpContext.Request.Headers["Authorization"];

            if (tokenobj == null)
            {
                ret.Code = 201;
                ret.Msg = "token不能为空";
                context.Result = new JsonResult(ret);
                return;
            }

            string token = tokenobj.ToString();

            string userId = "";
            //验证jwt,同时取出来jwt里边的用户ID
            
            TokenType tokenType = _tokenHelper.ValiTokenState(token, a => a["iss"] == _options.Issuer && a["aud"] == _options.Audience, action => { userId = action["loginID"]; });
            if (tokenType == TokenType.Fail)
            {
                ret.Code = 202;
                ret.Msg = "token验证失败";
                context.Result = new JsonResult(ret);
                return;
            }
            if (tokenType == TokenType.Expired)
            {
                ret.Code = 205;
                ret.Msg = "token已经过期";
                context.Result = new JsonResult(ret);
            }
            if (!string.IsNullOrEmpty(userId))
            {
                //给控制器传递参数(需要什么参数其实可以做成可以配置的，在过滤器里边加字段即可)
                //context.ActionArguments.Add("userId", Convert.ToInt32(userId));
            }
        }
    }
}
