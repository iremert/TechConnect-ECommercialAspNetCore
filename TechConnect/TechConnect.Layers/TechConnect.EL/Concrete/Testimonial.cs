using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechConnect.EL.Concrete
{
    public class Testimonial:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ID {  get; set; }
        public string Quote { get; set; }
        public string TestimonialName { get; set; }
        public string TestimonialJob { get; set; }
        public string TestimonialImageUrl {  get; set; }    
        public string TestimonialImageUrl2 {  get; set; }    
        public bool Status {  get; set; }
    }
}
