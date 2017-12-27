using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Project.Source.Domain;
using System.Web;

namespace Project.Controllers
{
    public class UploadController : Controller
    {
        private readonly DataProviderInterface _dataProvider;

        public UploadController(DataProviderInterface dataProvider)
        {
            _dataProvider = dataProvider;
        }
        
        public void Save()
        {
            var filename = Request.Form["filename"].ToString();
            /*
            foreach (string file in Request.Files)
            {
                HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                if (hpf.ContentLength == 0)
                    continue;
                string savedFileName = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory, 
                    Path.GetFileName(hpf.FileName));

                
            }*/
        }
    }
}
