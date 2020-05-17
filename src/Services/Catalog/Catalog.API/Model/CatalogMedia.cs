// Author: 	sprite
// On:		2020/5/11
using System;
using System.IO;

namespace Eva.eShop.Services.Catalog.API.Model
{
    public class CatalogMedia
    {
        public int Id { get; set; }

        public int CatalogItemId { get; set; }

        public string MediaFileName { get; set; }

        public string MediaUri { get; set; }

        public virtual CatalogItem CatalogItem { get; set; }
    }
}
