using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            ViewBag.lose = false;
            ViewBag.win = false;
            if (TempData["face"] == null)
            {
                ViewBag.face = "http://cartoonbros.com/wp-content/uploads/2016/08/pikachu-6.png";
            }
            else
            {
                ViewBag.face = TempData["face"];
            }
            if(HttpContext.Session.GetObjectFromJson<Pet>("Pet") == null )
            {
                var new_pet = new Pet();
                HttpContext.Session.SetObjectAsJson("Pet", new_pet);  
            }
            Pet Pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            if (Pet.fullness >= 100 || Pet.happiness >= 100){
                ViewBag.win = true;
            }
            if (Pet.fullness <= 0 || Pet.happiness <= 0)
            {
                ViewBag.lose = true;
            }
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            return View();
        }
        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            Pet Pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            TempData["face"] = Pet.feed();
            HttpContext.Session.SetObjectAsJson("Pet", Pet); 
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            Pet Pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            TempData["face"] = Pet.play();
            HttpContext.Session.SetObjectAsJson("Pet", Pet); 
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            Pet Pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            TempData["face"] = Pet.work();
            HttpContext.Session.SetObjectAsJson("Pet", Pet); 
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            Pet Pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            TempData["face"] = Pet.sleep();
            HttpContext.Session.SetObjectAsJson("Pet", Pet); 
            ViewBag.pet = HttpContext.Session.GetObjectFromJson<Pet>("Pet");
            return RedirectToAction("index");
        }
        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("index");
        }
    }  
    public class Pet
    {
        public int happiness = 20;
        public int fullness = 20;
        public int energy = 50;
        public int meals = 3;
        string message;

        public string feed()
        {
            if (meals > 0)
            {
                meals -= 1;
                int percent = new Random().Next(0,100);
                if (percent > 25)
                    {
                        fullness += new Random().Next(5,10);
                        string happy_img = "http://www.imgnaly.com/wp-content/uploads/2015/05/Pikachu-Drinking-Pocky-Chocolate-Giant.png";
                        return happy_img;
                    }
                else
                    {
                        unhappy();
                        string sad_img = "https://lh3.googleusercontent.com/-PhjWzNZwKUE/V_7dg8WbsXI/AAAAAAAACoE/nF8uCXmHC8EBi8LIdpQ1B6KzVqXzHX0VACJoC/w500-h500/large.png";
                        return sad_img;
                    }
            }
            else
            {
                unhappy();
                string sad_img = "https://lh3.googleusercontent.com/-PhjWzNZwKUE/V_7dg8WbsXI/AAAAAAAACoE/nF8uCXmHC8EBi8LIdpQ1B6KzVqXzHX0VACJoC/w500-h500/large.png";
                return sad_img;
            }
        }
        public string play()
        {
            int percent = new Random().Next(0,100);
            energy -= 5;
            if (percent > 25)
            {
                happiness += new Random().Next(5,10);
                string happy_img = "http://i572.photobucket.com/albums/ss169/xxxxxforgottenxxxxx/pikawii.jpg";
                return happy_img;
            }
            else
            {
                unhappy();
                string sad_img = "https://lh3.googleusercontent.com/-PhjWzNZwKUE/V_7dg8WbsXI/AAAAAAAACoE/nF8uCXmHC8EBi8LIdpQ1B6KzVqXzHX0VACJoC/w500-h500/large.png";
                return sad_img;
            }
        }
        public string work()
        {
            energy -= 5;
            meals += new Random().Next(1,3);
            string happy_img = "http://static1.e926.net/data/4b/48/4b488a3777f93805b55fe1f86b4a130c.jpg";
            return happy_img;
        }
        public string sleep()
        {
            energy += 15;
            fullness -= 5;
            happiness -= 5;
            string happy_img = "http://data.whicdn.com/images/5359424/large.jpg";
            return happy_img;
        }
        public void unhappy()
        {
            message = "she's unhappy!";
        }
    }
}
