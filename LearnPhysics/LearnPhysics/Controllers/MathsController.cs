using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnPhysics.Controllers
{
    public class MathsController : Controller
    {
        public IActionResult MathsIndex()
        {
            return View();
        }

        public IActionResult Algebra()
        {
            return View();
        }
    }
}