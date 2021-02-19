using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddleWarePrac.Controllers
{
    public class TestController:Controller
    {
        public IActionResult Index()
        {
            return Content("hello");
        }
    }
}
