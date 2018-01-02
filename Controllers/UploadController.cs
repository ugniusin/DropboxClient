using System.IO;
using Microsoft.AspNetCore.Mvc;
using Project.Source.Domain;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;

        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        
        [HttpPost]
        public IActionResult UploadFile(string uploadPath, IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                        
                    _fileManager.Upload(uploadPath ?? "", file.FileName, fileBytes);
                }
            }
            
            return Ok();
        }
        
        [HttpGet]
        public IActionResult DownloadFile(string path, string fileName)
        {
            byte[] fileBytes = _fileManager.Download(path, fileName);
            
            var content = new MemoryStream(fileBytes);
            var contentType = "application/octet-stream";
            return File(content, contentType, fileName);
        }
    }
}
