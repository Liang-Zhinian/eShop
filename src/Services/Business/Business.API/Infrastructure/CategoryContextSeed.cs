extern alias MySqlConnectorAlias;

namespace SaaSEqt.eShop.Business.API.Infrastructure
{
    using Microsoft.Extensions.Logging;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Options;
    using Polly;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using SaaSEqt.eShop.Business.Domain.Model.Categories;
    using SaaSEqt.eShop.Business.Infrastructure.Data;

    public class CategoryContextSeed
    {
        IList<Category> _categories = new List<Category>();
        IList<Subcategory> _subcategories = new List<Subcategory>();

        public async Task SeedAsync(BusinessDbContext context, IHostingEnvironment env, IOptions<BusinessSettings> settings, ILogger<CategoryContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(CategoryContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.Categories.Any())
                {
                    GetCategoriesFromFile(contentRootPath, logger);

                    await context.Categories.AddRangeAsync(useCustomizationData
                                                           ? _categories
                                                           : GetPreconfiguredRootCategories());

                    await context.Subcategories.AddRangeAsync(useCustomizationData
                                                           ? _subcategories
                                                              : GetPreconfiguredSubcategories());
                    await context.SaveChangesAsync();
                }
            });
        }

        private IEnumerable<Category> GetPreconfiguredCategories()
        {
            return new List<Category>()
            {
                //new Category() { Name = "Facial", ParentCategoryId=1},
                //new Category() { Name = "Facial", ParentCategoryId=1},
                //new Category() { Name = "Facial", ParentCategoryId=1},
            };
        }
        private IEnumerable<Subcategory> GetPreconfiguredSubcategories()
        {
            return new List<Subcategory>()
            {
                //new Category() { Name = "Facial", ParentCategoryId=1},
                //new Category() { Name = "Facial", ParentCategoryId=1},
                //new Category() { Name = "Facial", ParentCategoryId=1},
            };
        }

        private void GetCategoriesFromFile(string contentRootPath, ILogger<CategoryContextSeed> logger)
        {
            string csvFileCategories = Path.Combine(contentRootPath, "Setup", "beauty.csv");
            GetCategoriesFromFile("Beauty", csvFileCategories, logger);

            csvFileCategories = Path.Combine(contentRootPath, "Setup", "fitness.csv");
            GetCategoriesFromFile("Fitness", csvFileCategories, logger);

            csvFileCategories = Path.Combine(contentRootPath, "Setup", "wellness.csv");
            GetCategoriesFromFile("Wellness", csvFileCategories, logger);
        }

        private void GetCategoriesFromFile(string rootCategoryName, string csvFileCategories, ILogger<CategoryContextSeed> logger)
        {
            //string csvFileCategories = Path.Combine(contentRootPath, "Setup", "beauty.csv");

            if (!File.Exists(csvFileCategories))
            {
                return;
            }

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "subcategory", "category" };
                string[] optionalheaders = { };
                csvheaders = GetHeaders(csvFileCategories, requiredHeaders, optionalheaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return;
            }

            //var rootCategoryIdLookup = context.Categories.Where(y=>y.Id.Equals(0)).ToDictionary(ct => ct.Name, ct => ct.Id);
            //var secondCategoryIdLookup = context.Categories.Where(y => y.ParentCategoryId.Equals(0)).ToDictionary(ct => ct.Name, ct => ct.Id);

            var lines = File.ReadAllLines(csvFileCategories)
                        .Skip(1) // skip header row
                            .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"));

            foreach (var line in lines)
            {
                CreateCategory(line, csvheaders);
            }

            //lines.SelectTry(column => CreateCategory(column, csvheaders));
                //        .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                //.Where(x => x != null);



                    //lines
                       //.SelectTry(column => CreateSubcategory(column, csvheaders))
                        //.OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
                        //.Where(x => x != null);

            //return File.ReadAllLines(csvFileCategories)
            // .Skip(1) // skip header row
            // .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)") )
            //.SelectTry(column => CreateCategory(column, csvheaders, rootCategoryName))
            // .OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
            // .Where(x => x != null)
            //.Union(
            //    File.ReadAllLines(csvFileCategories)
            // .Skip(1) // skip header row
            // .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
            //.SelectTry(column => CreateCategory(column, csvheaders, ""))
            //.OnCaughtException(ex => { logger.LogError(ex.Message); return null; })
            //.Where(x => x != null)
            //);
        }

        private Category CreateCategory(string[] column, string[] headers)
        {
            if (column.Count() != headers.Count())
            {
                throw new Exception($"column count '{column.Count()}' not the same as headers count'{headers.Count()}'");
            }

            string subcategory = column[Array.IndexOf(headers, "subcategory")].Trim('"').Trim();
            string category = column[Array.IndexOf(headers, "category")].Trim('"').Trim();

            var categoryObj = new Category()
            {
                Name = category,
            };
            if (!_categories.Where(y=>y.Name.Equals(category)).Any()) _categories.Add(categoryObj);

            var subcategoryObj = new Subcategory()
            {
                Name = subcategory,
                CategoryName = category,
            };
            if (!_subcategories.Where(y => y.Name.Equals(subcategory) && y.CategoryName.Equals(category)).Any()) _subcategories.Add(subcategoryObj);

            return categoryObj;
            
        }

        private IEnumerable<Category> GetPreconfiguredRootCategories()
        {
            return new List<Category>()
            {
                //new Category { Name = "Beauty", ParentCategoryName="Beauty" },
                //new Category { Name = "Fitness", ParentCategoryName="Fitness" },
                //new Category { Name = "Wellness", ParentCategoryName="Wellness" },
                //new Category { Name = "Drink", ParentCategoryName="Drink" },
            };
        }

        private string[] GetHeaders(string csvfile, string[] requiredHeaders, string[] optionalHeaders = null)
        {
            string[] csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() < requiredHeaders.Count())
            {
                throw new Exception($"requiredHeader count '{ requiredHeaders.Count()}' is bigger then csv header count '{csvheaders.Count()}' ");
            }

            if (optionalHeaders != null)
            {
                if (csvheaders.Count() > (requiredHeaders.Count() + optionalHeaders.Count()))
                {
                    throw new Exception($"csv header count '{csvheaders.Count()}'  is larger then required '{requiredHeaders.Count()}' and optional '{optionalHeaders.Count()}' headers count");
                }
            }

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                {
                    throw new Exception($"does not contain required header '{requiredHeader}'");
                }
            }

            return csvheaders;
        }

        private Policy CreatePolicy(ILogger<CategoryContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<MySqlConnectorAlias::MySql.Data.MySqlClient.MySqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogTrace($"[{prefix}] Exception {exception.GetType().Name} with message ${exception.Message} detected on attempt {retry} of {retries}");
                    }
                );
        }
    }
}
