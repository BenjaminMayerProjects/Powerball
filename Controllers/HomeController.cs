// HomeController.cs
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Powerball.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var model = new PowerballModel
            {
                RandomNumbers = GenerateRandomNumbers(),
                UserNumbers = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CheckNumbers(string userNumbers)
        {
            var randomNumbers = GenerateRandomNumbers();
            var userNumberList = userNumbers.Split(',').Select(int.Parse).ToList();

            var model = new PowerballModel
            {
                RandomNumbers = randomNumbers,
                UserNumbers = userNumberList,
                UserWon = userNumberList.Intersect(randomNumbers).Any()
            };

            return View("Index", model);
        }

        private List<int> GenerateRandomNumbers()
        {
            Random random = new Random();
            return Enumerable.Range(1, 5).Select(_ => random.Next(1, 100)).ToList();
        }
    }
}
