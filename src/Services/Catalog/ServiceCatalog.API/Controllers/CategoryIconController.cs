using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Infrastructure;
using SaaSEqt.eShop.Services.ServiceCatalog.API.Model;
using SaaSEqt.eShop.Services.ServiceCatalog.API.ViewModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.ServiceCatalog.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoryIconController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly ServiceCatalogSettings _settings;
        private readonly CatalogContext _catalogContext;

        public CategoryIconController(IHostingEnvironment env,
                                      IOptionsSnapshot<ServiceCatalogSettings> settings, 
            CatalogContext catalogContext)
        {
            _env = env;
            _catalogContext = catalogContext;
            _settings = settings.Value;
            ((DbContext)catalogContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }



        [HttpGet]
        //[Route("mobile/{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryIcon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryIconById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var item = await _catalogContext.CategoryIcons.SingleOrDefaultAsync(ci => ci.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound();
        }

        [HttpGet]
        [Route("api/v1/categories/mobile/withtype/{type:minlength(1)}/pic")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public async Task<IActionResult> GetIconsByType(string type, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var totalItems = await _catalogContext.CategoryIcons
                                                  .Where(ci => ci.Type == type)
                                                  .LongCountAsync();

            var itemsOnPage = await _catalogContext.CategoryIcons
                .Where(ci => ci.Type == type)
                .OrderBy(c => c.Order)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            itemsOnPage = ChangeUriPlaceholder(itemsOnPage);

            var model = new PaginatedItemsViewModel<CategoryIcon>(
                pageIndex, pageSize, totalItems, itemsOnPage);

            return Ok(model);
        }

        //POST api/v1/[controller]
        [HttpPost]
        //[Route("serviceitems")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCategoryIcon([FromBody]CreateCategoryIconRequest request)
        {
            string imageFileExtension = Path.GetExtension(request.Image.FileName);
            var webRoot = _env.WebRootPath;
            var dir = Path.Combine(webRoot, "mobile");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var path = Path.Combine(dir, request.Name + "." + imageFileExtension);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            var item = new CategoryIcon { 
                Name = request.Name,
                //IconUri = categoryIcon.IconUri,
                IconFileName = "mobile" + request.Name + "." + imageFileExtension,
                ServiceCategoryId = request.ServiceCategoryId,
                Type = request.Type,
                Order = request.Order,
            };
            _catalogContext.CategoryIcons.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryIconById), new { id = item.Id }, null);
        }

        private List<CategoryIcon> ChangeUriPlaceholder(List<CategoryIcon> items)
        {
            var baseUri = _settings.PicBaseUrl;
            var azureStorageEnabled = _settings.AzureStorageEnabled;

            foreach (var item in items)
            {
                item.FillCategoryIconUrl(baseUri, azureStorageEnabled: azureStorageEnabled);
            }

            return items;
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
