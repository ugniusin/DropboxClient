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
        private readonly IDataProvider _dataProvider;

        public HomeController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
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
            ViewData["Folders"] =  _dataProvider.ListFolder("");
            ViewData["Files"] =  _dataProvider.ListFiles("");
            
            return View();
        }
        
        public JsonResult ListFoldersAndFiles(string path)
        {
            List<Folder> folders = _dataProvider.ListFolder(path ?? "");
            List<File> files = _dataProvider.ListFiles(path ?? "");
            List<IFileSystemNode> result = folders.Concat<IFileSystemNode>(files).ToList();
            
            result.Sort((x, y) => x.GetTitle().CompareTo(y.GetTitle()));
           
            return Json(result);
        }
        
        public JsonResult ListFolders(string path)
        {
            List<Folder> folders = _dataProvider.ListFolder(path ?? "");
           
            return Json(folders);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
