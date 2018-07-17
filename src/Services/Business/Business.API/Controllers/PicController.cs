using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Business.Infrastructure;
using SaaSEqt.eShop.Services.Business.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    public class PicController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;

        public PicController(IHostingEnvironment env,
                             BusinessService businessService)
        {
            _businessService = businessService;
            _env = env;
        }

        [HttpGet]
        [Route("api/v1/business/sites/{siteId:Guid}/logo")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public async Task<IActionResult> GetSiteLogo(Guid siteId)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _businessService.FindExistingSite(siteId);

            if (item != null)
            {
                var webRoot = _env.WebRootPath;
                var path = Path.Combine(webRoot, item.Id.ToString(), item.Branding.Logo);

                string imageFileExtension = Path.GetExtension(item.Branding.Logo);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return File(buffer, mimetype);
            }

            return NotFound();
        }

        private string GetImageMimeTypeFromImageFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".png":
                    mimetype = "image/png";
                    break;
                case ".gif":
                    mimetype = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    mimetype = "image/jpeg";
                    break;
                case ".bmp":
                    mimetype = "image/bmp";
                    break;
                case ".tiff":
                    mimetype = "image/tiff";
                    break;
                case ".wmf":
                    mimetype = "image/wmf";
                    break;
                case ".jp2":
                    mimetype = "image/jp2";
                    break;
                case ".svg":
                    mimetype = "image/svg+xml";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}
