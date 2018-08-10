using System;
namespace SaaSEqt.eShop.Services.Catalog.API.Model
{
    public class IndustryStandardCategory
    {
        public IndustryStandardCategory()
        {
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int ParentCategoryId { get; private set; }
        public virtual IndustryStandardCategory ParentCategory { get; private set; }
    }
}
