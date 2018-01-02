using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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
            try {
                ViewData["Name"] = _dataProvider.Name();
                ViewData["Email"] = _dataProvider.Email();
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
            }
            
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
            try {
                ViewData["Folders"] = _dataProvider.ListFolder("");
                ViewData["Files"] = _dataProvider.ListFiles("");
            } catch (Exception e) {
                ViewData["ErrorMessage"] = e.Message;
            }

            return View();
        }
        
        public JsonResult ListFoldersAndFiles(string path)
        {
            try {
                List<Folder> folders = _dataProvider.ListFolder(path ?? "");
                List<File> files = _dataProvider.ListFiles(path ?? "");
                List<IFileSystemNode> result = folders.Concat<IFileSystemNode>(files).ToList();

                result.Sort((x, y) => x.GetTitle().CompareTo(y.GetTitle()));
                
                return Json(result);
            } catch (Exception e) {
                return Json(new { Error = e });
            }
        }
        
        public JsonResult ListFolders(string path)
        {
            try {
                List<Folder> folders = _dataProvider.ListFolder(path ?? "");

                return Json(folders);
            } catch (Exception e) {
                return Json(new {error = e.Message});
            }
        }
    }
}
