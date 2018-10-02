using System;
namespace SaaSEqt.eShop.Services.Business.Domain.Model.Categories
{
    public class Subcategory
    {
        public Subcategory()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string CategoryName { get; set; }
    }
}
