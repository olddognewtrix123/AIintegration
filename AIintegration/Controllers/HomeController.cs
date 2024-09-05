using GroqSharp;
using AIintegration.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AIintegration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Passing this in using dependency injection:
        private IGroqClient _groqClient;

        public HomeController(ILogger<HomeController> logger, IGroqClient groqClient) // the Services container passes in the IGrogClient object
        {
            _logger = logger;
            _groqClient = groqClient; // initializes instance field right here
        }


        // This is the starting point of the application!! It simply sends out the view Index.cshtml to client.
        [HttpGet] // I added this. It is implied but I added it just so things make more sense to the reader.
        public IActionResult Index()
        {
            return View();
        }

        // Here is my method to post to
        [HttpPost]
        public async Task<IActionResult> Index(string question)
        {
            question = String.Concat(question, " Please answer in 50 characters or less with a Chamath Palihapitiya impersonation.");
            string answer = await _groqClient.CreateChatCompletionAsync(new GroqSharp.Models.Message { Content = question });
            // ViewBag is just a way of sending data from the controller to the view.
            ViewBag.Answer = answer;
            // Now, just return to the view. 
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
