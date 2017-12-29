using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Source.Application.DTO;
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
        
        public IActionResult Files()
        {   
            ViewData["Message"] = "Your application description page.";
            ViewData["Folders"] =  _serviceInterface.ListFolder("");
            ViewData["Files"] =  _serviceInterface.ListFiles("");
            
            return View();
        }
        
        public JsonResult ListFoldersAndFiles(string path)
        {
            List<Folder> folders = _serviceInterface.ListFolder(path ?? "");
            List<File> files = _serviceInterface.ListFiles(path ?? "");
            
            List<IFileSystemNode> result = folders.Concat<IFileSystemNode>(files).ToList();
            
            result.Sort((x, y) => x.GetTitle().CompareTo(y.GetTitle()));
           
            return Json(result);
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
            
            foreach (var directory in _serviceInterface.ListFolder(""))
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
