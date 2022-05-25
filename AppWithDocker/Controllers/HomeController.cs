using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppWithDocker.Models;
using AppWithDocker.Logging.Interface;

namespace AppWithDocker.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly ILoggerManager _logger;

        public HomeController(ILoggerManager logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInfo("The home controller index method is accessed");
            _logger.LogInfo("Here is info message from the controller.");
            _logger.LogDebug("Here is debug message from the controller.");
            _logger.LogWarn("Here is warn message from the controller.");
            _logger.LogError("Here is error message from the controller.");
            //return new string[] { "value1", "value2" };
            //_logger.LogInformation("The home page is accessed");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInfo("The Privacy method is accessed");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
