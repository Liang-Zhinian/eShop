using System;
namespace SaaSEqt.eShop.Business.Domain.Model.Catalog
{
    public class CategoryIcon
    {
        public CategoryIcon()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUri { get; set; }
        public string IconFileName { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; }
        public string Type { get; set; }
        public int Order { get; set; }
    }
}
