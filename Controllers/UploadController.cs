using System.IO;
using Microsoft.AspNetCore.Mvc;
using Project.Source.Domain;
using Microsoft.AspNetCore.Http;

namespace Project.Controllers
{
    public class UploadController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IFileUploader _fileUploader;

        public UploadController(IDataProvider dataProvider, IFileUploader fileUploader)
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
