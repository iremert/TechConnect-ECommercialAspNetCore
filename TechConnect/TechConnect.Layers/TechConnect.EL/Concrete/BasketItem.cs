using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class BasketItem
    {
        public string ProductId { get; set; }
        [BsonIgnore]
        public Product Product { get; set; }
        public int Quantity { get; set; }
   
    }
}
