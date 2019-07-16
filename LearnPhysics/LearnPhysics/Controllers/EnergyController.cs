using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LearnPhysics.Controllers
{
    public class EnergyController : Controller
    {
        private enum Quizzes
        {
            OpeningQuiz = 1,
            ChapterQuiz = 2
        }

        public IActionResult EnergyIndex()
        {
            return View();
        }

        public IActionResult OpeningQuiz(Quizzes quiz)
        {
            if (quiz == Quizzes.OpeningQuiz)
            {

            }
            return View();
        }

        public IActionResult Lesson1()
        {
            return View();
        }

        public IActionResult Lesson2()
        {
            return View();
        }

        public IActionResult Lesson3()
        {
            return View();
        }

        public IActionResult Lesson4()
        {
            return View();
        }

        public IActionResult Lesson5()
        {
            return View();
        }

        public IActionResult Lesson6()
        {
            return View();
        }
    }
}