using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Source.Domain;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataProviderInterface _serviceInterface;

        public HomeController(DataProviderInterface serviceInterface)
        {
            _serviceInterface = serviceInterface;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult About()
        {   
            ViewData["Message"] = "Your application description page." + _serviceInterface.DropBoxInfo();
            
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.SyncOrAsync = "Asynchronous";
            ViewData["Message"] = "Your contact page.";
            
            foreach (var directory in _serviceInterface.ListRootFolder())
            {
                ViewData["Message"] += "\n" + directory;
            }
           

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
