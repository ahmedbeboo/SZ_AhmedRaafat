using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net.Http.Headers;

namespace Net_AhmedRaafat.Controllers
{
    //[Authorize]
    [Route("api/FileUpload")]
    public class FileUploadController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public FileUploadController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("{newFileName}"), DisableRequestSizeLimit]
        public ActionResult UploadFile(string newFileName)
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Uploads";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = Path.Combine(webRootPath, folderName);



                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    //string OldfileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    //string fileName = newFileName+"."+ Path.GetExtension(OldfileName); ;
                    string fullPath = Path.Combine(newPath, newFileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }

                return Json("Upload Successfull.");

            }
            catch (Exception ex)
            {
                return Json("Upload Failed: " + ex.Message);
            }
        }
    }
}
