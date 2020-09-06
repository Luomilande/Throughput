using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyCoreWeb.Models;

namespace MyCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private MyCoreWebContext _context;
        public HomeController(MyCoreWebContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }
        [ActonFilter]
        public JsonResult json()
        {
            for(int i=0;i<100;i++)
            {
                _context.Topics.Add(new Topics()
                {
                    Title = "Hello World",
                    Content = "你好!",
                    CreateTime = DateTime.Now
                });
            }
            _context.SaveChanges();
            return Json(_context.Topics.Take(2).ToList());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
