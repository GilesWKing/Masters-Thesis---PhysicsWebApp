﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnPhysics.Controllers
{
    public class TopicsController : Controller
    {
        public IActionResult TopicsIndex()
        {
            return View();
        }
    }
}