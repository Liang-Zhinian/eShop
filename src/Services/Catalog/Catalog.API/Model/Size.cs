using System;
namespace Catalog.API.Model
{
    public class Size
    {
        public Size()
        {
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid MerchantId { get; set; }
    }
}
