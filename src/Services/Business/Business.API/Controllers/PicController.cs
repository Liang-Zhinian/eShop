using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.Business.Infrastructure;
using SaaSEqt.eShop.Services.Business.Services;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.Business.API.Controllers
{
    public class PicController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BusinessService _businessService;
        private readonly ILogger<PicController> _logger;

        public PicController(IHostingEnvironment env,
                             BusinessService businessService,
                             ILogger<PicController> logger)
        {
            _businessService = businessService;
            _env = env;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/v1/sites/{siteId:Guid}/logo")]
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
                _logger.LogDebug("webRoot: " + webRoot);

                var path = Path.Combine(webRoot, item.Id.ToString(), item.Branding.Logo);
                _logger.LogDebug("path: " + path);

                string imageFileExtension = Path.GetExtension(item.Branding.Logo);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return File(buffer, mimetype);
            }

            return NotFound();
        }

        // GET api/v1/business/locations/ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}/pic
        [HttpGet]
        [Route("api/v1/locations/ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLocationImage(Guid siteId, Guid locationId)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _businessService.FindExistingLocation(siteId, locationId);

            if (item != null)
            {
                var webRoot = _env.WebRootPath;
                _logger.LogDebug("webRoot: " + webRoot);

                var path = Path.Combine(webRoot, item.Image);
                _logger.LogDebug("path: " + path);

                string imageFileExtension = Path.GetExtension(item.Image);
                string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                var buffer = System.IO.File.ReadAllBytes(path);

                return File(buffer, mimetype);
            }

            return NotFound();
        }

        // GET api/v1/business/locations/ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}/itemId/{itemId:Guid}/pic
        [HttpGet]
        [Route("api/v1/locations/ofSiteId/{siteId:Guid}/locationId/{locationId:Guid}/itemId/{itemId:Guid}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetLocationAdditionalImage(Guid siteId, Guid locationId, Guid itemId)
        {
            if (siteId == Guid.Empty)
            {
                return BadRequest();
            }

            var item = await _businessService.FindExistingLocation(siteId, locationId);

            if (item != null && item.AdditionalLocationImages != null)
            {
                var img = item.AdditionalLocationImages.SingleOrDefault(y => y.Id.Equals(itemId));
                if (img != null)
                {
                    var webRoot = _env.WebRootPath;
                    _logger.LogDebug("webRoot: " + webRoot);

                    var path = Path.Combine(webRoot, img.Image);
                    _logger.LogDebug("path: " + path);

                    string imageFileExtension = Path.GetExtension(item.Image);
                    string mimetype = GetImageMimeTypeFromImageFileExtension(imageFileExtension);

                    var buffer = System.IO.File.ReadAllBytes(path);

                    return File(buffer, mimetype);
                }
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
