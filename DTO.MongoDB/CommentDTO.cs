using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;

namespace DTO.MongoDB
{
    public class CommentDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("body")]
        public string CommentBody { get; set; }
        [BsonElement("surname")]
        public string Surname { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("date")]
        public object PostCreateDate { get; set; }
    }
}
