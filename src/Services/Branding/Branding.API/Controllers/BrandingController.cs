using Branding.API.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SaaSEqt.eShop.Services.Branding.API.Infrastructure;
using SaaSEqt.eShop.Services.Branding.API.Model;
using SaaSEqt.eShop.Services.Branding.API.ViewModel;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace SaaSEqt.eShop.Services.Branding.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class BrandingController : Controller
    {
        private readonly IHostingEnvironment _env;
        private readonly BrandingSettings _settings;
        private readonly BrandingContext _catalogContext;

        public BrandingController(IHostingEnvironment env,
                                      IOptionsSnapshot<BrandingSettings> settings,
            BrandingContext catalogContext)
        {
            _env = env;
            _catalogContext = catalogContext;
            _settings = settings.Value;
            ((DbContext)catalogContext).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        //POST api/v1/[controller]
        [HttpPost]
        [Route("[action]/oftype/{type:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> UpdateVersion([FromBody]string type, string language)
        {
            var currentVersion = await _catalogContext.Versions.Where(y => y.Type.Equals(type) && y.Language.Equals(language)).SingleOrDefaultAsync();
            if (currentVersion == null)
            {
                currentVersion = new Version()
                {
                    Type = type,
                    VersionNumber = 1,
                    Language = language
                };
                _catalogContext.Versions.Add(currentVersion);
            }
            else
            {

                currentVersion.VersionNumber++;

                _catalogContext.Versions.Update(currentVersion);
            }


            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVersionByType), new { type = type, language = language }, null);
        }

        [HttpGet]
        [Route("[action]/{type:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CategoryIcon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetVersionByType(string type, string language)
        {
            var item = await _catalogContext.Versions.Where(y => y.Type.Equals(type) && y.Language.Equals(language)).SingleAsync();
            if (item != null)
            {
                return Ok(item);
            }
            return NotFound();
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
        [Route("api/v1/branding/mobile/withtype/{type:minlength(1)}/language/{language:minlength(1)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        // GET: /<controller>/
        public async Task<IActionResult> GetIconsByType(string type, string language, [FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
            var totalItems = await _catalogContext.CategoryIcons
                                                  .Where(y => y.Type == type && y.Language.Equals(language))
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
        public async Task<IActionResult> AddCategoryIcon(CreateCategoryIconRequest request)
        {
            var currentVersion = await _catalogContext
                .Versions
                .Where(y => y.Type.Equals(request.Type) && y.Language.Equals(request.Language))
              .SingleAsync();
            if (currentVersion == null)
            {
                return BadRequest();
            }

            string imageFileExtension = Path.GetExtension(request.Image.FileName);
            var webRoot = _env.WebRootPath;
            var dir = webRoot; //Path.Combine(webRoot, "Pics");
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            var fileName = string.Format("{0}_{1}_{2}_{3}_{4}{5}", 
                                         request.Language, 
                                         request.Type,
                                         request.CategoryName,
                                         request.SubcategoryName,
                                         currentVersion.VersionNumber.ToString(),
                                         imageFileExtension);
                var path = Path.Combine(dir, fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }


            Size size = ImageHelper.GetDimensions(path);

            var item = new CategoryIcon
            {
                Name = request.Name,
                //IconUri = categoryIcon.IconUri,
                IconFileName = fileName,
                SubcategoryName = request.SubcategoryName,
                CategoryName = request.CategoryName,
                Type = request.Type,
                Order = request.Order,
                VersionNumber = currentVersion.VersionNumber,
                Width = size.Width,
                Height = size.Height,
                Language = request.Language
            };
            _catalogContext.CategoryIcons.Add(item);

            await _catalogContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCategoryIconById), new { id = currentVersion.Id }, null);
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
