using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Source.Application.DTO;
using Project.Source.Domain;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider _serviceInterface;

        public HomeController(IDataProvider serviceInterface)
        {
            _serviceInterface = serviceInterface;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [Route("FileUpload")]
        public IActionResult FileUpload()
        {
            return View();
        }
        
        [Route("Files")]
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
