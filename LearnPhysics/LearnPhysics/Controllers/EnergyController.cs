using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnPhysics.Controllers
{
    public class EnergyController : Controller
    {
        public IActionResult EnergyIndex()
        {
            return View();
        }

        public IActionResult Lesson1()
        {
            return View();
        }
    }
}