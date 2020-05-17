using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Eva.eShop.Services.Marketing.API.Model
{
    public class MarketingData
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public List<Location> Locations { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
