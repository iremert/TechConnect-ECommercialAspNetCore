using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class Comment:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID { get; set; }
        public string UserId { get; set; }
        public string CommentReal{ get; set; }
        public int Rating { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
        public string ProductId { get; set; }
    }
}
