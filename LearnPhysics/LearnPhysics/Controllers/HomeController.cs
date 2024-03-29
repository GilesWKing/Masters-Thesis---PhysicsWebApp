﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LearnPhysics.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Web;

namespace LearnPhysics.Controllers
{
    public class HomeController : Controller
    {
        private readonly GilesContext _context;
        private readonly Common _common;

        public HomeController(GilesContext context, Common common)
        {
            _context = context;
            _common = common;
        }

        public IActionResult Index()
        {
            if (Convert.ToBoolean(HttpContext.Session.GetString("Authenticated")))
            {
                return RedirectToAction("TopicsIndex", "Topics");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(User loginData)
        {
            User user = _context.User.SingleOrDefault(table=>table.Username==loginData.Username);

            if (user != null)
            {
                // Compare the database password against the password that's encrypted from login.
                if (user.Password == CreatePasswordHash(loginData.Password, user.PasswordSalt))
                {
                    //Use 'session' to limit login capability.
                    HttpContext.Session.SetString("Authenticated", true.ToString());

                    HttpContext.Session.SetInt32("UserId", user.UserId);
                    //Return TopicsIndex screen once logged in.
                    return RedirectToAction("TopicsIndex", "Topics");
                    //return View("~/Views/Topics/TopicsIndex.cshtml");
                }
            }

           ViewBag.NotValidUser = "Not valid login credentials.";
           
           return View();
        }

        public string CreateSalt()
        {
            var rng = RandomNumberGenerator.Create();
            byte[]buff = new byte[32];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string CreatePasswordHash(string password, string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            var hashAlgorithm = SHA256.Create();
            bytes = hashAlgorithm.ComputeHash(bytes);
            return Convert.ToBase64String(bytes);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("Authenticated", false.ToString());
            return Redirect("/");
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(User registrationData)
        {
            registrationData.UserId = 0;

            // INSERT VALIDATION HERE!!! //
            // if validation fails, return view with registration data //
            // if validation passes continue //
            // use viewbags to return individual messages for each failure //
            // i.e. gender, password, age - each has individual viewbag. //

            // Stores unencrypted password in model rather than encrypted version.
            string password = registrationData.Password;
            // Creates a new salt stored in a variable called salt.
            string salt = CreateSalt();

            // Storing created salt in the registration model.
            registrationData.PasswordSalt = salt;
            // Storing a concatenated encrypted password and salt in model.
            registrationData.Password = CreatePasswordHash(password, salt);
            
            try
            {
                _context.Reset();
                _context.User.Add(registrationData);
                _context.SaveChanges();

                User user = new User();
                user.Username = registrationData.Username;
                user.Password = password;

                // Not returning index -> running login function.
                return Index(user);
            }
            catch (Exception e)
            {
                registrationData.Password = "";
                ViewBag.UsernameTaken = "Username already exists.";
            }

            return View(registrationData);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
