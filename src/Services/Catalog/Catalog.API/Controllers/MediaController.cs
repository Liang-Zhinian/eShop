using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eva.eShop.Services.Catalog.API.Infrastructure;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Eva.eShop.Services.Catalog.API.Model;
using System.Linq;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Eva.eShop.Services.Catalog.API.Controllers
{ 
    public class MediaController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly CatalogContext _catalogContext;

        public MediaController(IHostingEnvironment env,
            CatalogContext catalogContext)
        {
            _env = env;
            _catalogContext = catalogContext;
        }

        [HttpGet]
        [Route("api/v1/catalog/items/{catalogItemId:int}/media/{catalogMediaId:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public async Task<IActionResult> GetMedia(int catalogItemId, int catalogMediaId)
        {
            if (catalogItemId <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogContext.CatalogItems
                .Include(ci => ci.CatalogMedias)
                .SingleOrDefaultAsync(ci => ci.Id == catalogItemId);

            if (item != null)
            {
                var media = item.CatalogMedias.SingleOrDefault(m => m.Id == catalogMediaId);

                if (media != null)
                {
                    var webRoot = _env.WebRootPath;
                    var path = Path.Combine(webRoot, media.MediaFileName);

                    string mediaFileExtension = Path.GetExtension(media.MediaFileName);
                    string mimetype = GetMediaMimeTypeFromMediaFileExtension(mediaFileExtension);

                    var buffer = System.IO.File.ReadAllBytes(path);

                    return File(buffer, mimetype);
                }
            }

            return NotFound();
        }

        private string GetMediaMimeTypeFromMediaFileExtension(string extension)
        {
            string mimetype;

            switch (extension)
            {
                case ".mp4":
                    mimetype = "video/mp4";
                    break;
                default:
                    mimetype = "application/octet-stream";
                    break;
            }

            return mimetype;
        }
    }
}
