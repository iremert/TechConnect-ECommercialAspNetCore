using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class BasketTotal:IEntity
    {
        public BasketTotal(List<BasketItem> basketItems)
        {
            BasketItems = new List<BasketItem>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }

        public string UserId { get; set; }
        public string DiscountId { get; set; }
        [BsonIgnore]
        public Discount discount { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsFinished { get; set; }


    }
}
