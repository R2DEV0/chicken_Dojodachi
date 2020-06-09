using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace Dojodachi
{
    public class HomeController : Controller
    {

        [HttpGet("")]
        public ViewResult Index()
        {
            // checks if you are dead or alive //
            bool Died = false;
            // checks if you won yet //
            bool Won = false;

            int? Fullness = HttpContext.Session.GetInt32("fullness");
            if(Fullness == null)
            {
                HttpContext.Session.SetInt32("fullness", 20);
            }
            if(Fullness <= 0)
            {
                Died = true;
            }
            if(Fullness >= 100)
            {
                Won = true;
            }

            int? Happiness = HttpContext.Session.GetInt32("happiness");
            if(Happiness == null)
            {
                HttpContext.Session.SetInt32("happiness", 20);
            }
            if(Happiness <= 0)
            {
                Died = true;
            }
            if(Happiness >= 100)
            {
                Won = true;
            }

            int? Energy = HttpContext.Session.GetInt32("energy");
            if(Energy == null)
            {
                HttpContext.Session.SetInt32("energy", 50);
            }
            if(Energy <= 0)
            {
                Died = true;
            }
            if(Energy >= 100)
            {
                Won = true;
            }

            int? Meals = HttpContext.Session.GetInt32("meals");
                if(Meals == null)
                {
                    HttpContext.Session.SetInt32("meals", 3);
                }
            string Message = HttpContext.Session.GetString("message");
                if(Message == null)
                {
                    HttpContext.Session.SetString("message", "Your Chicken is ready for life!");
                }

            ViewBag.Meals = HttpContext.Session.GetInt32("meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("energy");
            ViewBag.Happiness = HttpContext.Session.GetInt32("happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("fullness");
            ViewBag.Message = HttpContext.Session.GetString("message");
            ViewBag.Died = Died;
            ViewBag.Won = Won;

            return View("Index");
        }


        [HttpPost("Feed")]
        public IActionResult feed()
        {
            int? Meals = HttpContext.Session.GetInt32("meals");
            Random rand = new Random();
            if(Meals > 0)
            {
                HttpContext.Session.SetInt32("meals", (int)Meals-1);
                int? Fullness = HttpContext.Session.GetInt32("fullness");
                HttpContext.Session.SetInt32("fullness", (int)Fullness + rand.Next(5, 10));
                HttpContext.Session.SetString("message", "Your Chicken enjoyed the meal");
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpPost("Play")]
        public IActionResult play()
        {
            int? Energy = HttpContext.Session.GetInt32("energy");
            Random rand = new Random();
            if(Energy > 0)
            {
                HttpContext.Session.SetInt32("energy", (int)Energy-5);
                int? Happiness = HttpContext.Session.GetInt32("happiness");
                HttpContext.Session.SetInt32("happiness", (int)Happiness + rand.Next(5, 10));
                HttpContext.Session.SetString("message", "You played with your chicken and he had a great time");
            }
            return RedirectToAction("Index");
        }


        [HttpPost("Work")]
        public IActionResult work()
        {
            int? Energy = HttpContext.Session.GetInt32("energy");
            Random rand = new Random();
            if(Energy > 0)
            {
                HttpContext.Session.SetInt32("energy", (int)Energy-5);
                int? Meals = HttpContext.Session.GetInt32("meals");
                HttpContext.Session.SetInt32("meals", (int)Meals + rand.Next(1, 3));
                HttpContext.Session.SetString("message", "Your Chicken worked for his meal");
            }
            return RedirectToAction("Index");
        }


        [HttpPost("Sleep")]
        public IActionResult sleep()
        {
            int? Happiness = HttpContext.Session.GetInt32("happiness");
            int? Fullness = HttpContext.Session.GetInt32("fullness");
            if(Happiness > 0 && Fullness > 0)
            {
                int? Energy = HttpContext.Session.GetInt32("energy");
                HttpContext.Session.SetInt32("energy", (int)Energy + 15);
                HttpContext.Session.SetInt32("happiness", (int)Happiness - 5);
                HttpContext.Session.SetInt32("fullness", (int)Fullness - 5);
                HttpContext.Session.SetString("message", "Your Chicken took a nap and feels great!");
            }
            return RedirectToAction("Index");
        }


        [HttpPost("Restart")]
        public IActionResult restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}