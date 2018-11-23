using System;
namespace Catalog.API.Model
{
    public class Color
    {
        public Color()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MerchantId { get; set; }
    }
}
