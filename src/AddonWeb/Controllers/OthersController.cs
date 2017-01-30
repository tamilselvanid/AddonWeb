using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AddonWeb.Controllers
{
    public class OthersController : Controller
    {
        public IActionResult Prices()
        {
            return View();
        }
    }
}