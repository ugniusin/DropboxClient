using System.IO;
using Microsoft.AspNetCore.Mvc;
using Project.Source.Domain;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{
    public class UploadController : Controller
    {
        private readonly DataProviderInterface _dataProvider;
        private readonly FileUploaderInterface _fileUploader;

        public UploadController(DataProviderInterface dataProvider, FileUploaderInterface fileUploader)
        {
            _dataProvider = dataProvider;
            _fileUploader = fileUploader;
        }
        
        [HttpPost]
        public IActionResult UploadFile(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    byte[] fileBytes = ms.ToArray();
                        
                    _fileUploader.Upload("", file.FileName, fileBytes);
                }
            }
            
            return Ok();
        }
    }
}
