using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaaSEqt.eShop.Services.IndustryStandardCategory.API.Infrastructure;
using SaaSEqt.eShop.Services.IndustryStandardCategory.API.Model;
using SaaSEqt.eShop.Services.IndustryStandardCategory.API.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SaaSEqt.eShop.Services.IndustryStandardCategory.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryContext _categoryContext;
        private readonly CategorySettings _settings;

        public CategoryController(CategoryContext context, IOptionsSnapshot<CategorySettings> settings)
        {
            _categoryContext = context ?? throw new ArgumentNullException(nameof(context));

            _settings = settings.Value;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/v1/[controller]/categories
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Categories()
        {
            var model = await _categoryContext.Categories
                                        .OrderBy(y => y.Name)
                                        .ToListAsync();

            return Ok(model);
        }

        // GET api/v1/[controller]/categories/withname/samplename
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Categories(string name)
        {
            var model = await _categoryContext.Categories
                                              .Where(y => y.Name.Equals(name))
                                        .ToListAsync();

            return Ok(model);
        }

        // GET api/v1/[controller]/subcategories/withparent/samplename
        [HttpGet]
        [Route("[action]/withparent/{parent:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Subcategory>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Subcategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Subcategories(string parent)
        {
            var model = await _categoryContext.Subcategories
                                              .Where(y => y.CategoryName.Equals(parent))
                                        .OrderBy(y => y.Name)
                                        .ToListAsync();
            
            return Ok(model);
        }

        // GET api/v1/[controller]/subcategories/withname/samplename
        [HttpGet]
        [Route("[action]/withname/{name:minlength(1)}")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Subcategory>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Subcategory>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SubcategoriesWithName(string name)
        {
            var model = await _categoryContext.Subcategories
                                              .Where(y => y.Name.Equals(name))
                                        .OrderBy(y => y.Name)
                                        .ToListAsync();

            return Ok(model);
        }
    }
}
