using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class About: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID {  get; set; }
        public string ImageUrl {  get; set; }
        public string Description1 { get;set; }
        public string Description2 { get;set; }
        public string Description3 { get;set; }
        public bool Status { get; set; }
    }
}
