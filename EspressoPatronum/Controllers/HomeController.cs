using EspressoPatronum.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EspressoPatronum.Controllers
{
    public class HomeController : Controller
    {
     
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
