using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace usersApi.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonElement("username")]
        public string username { get; set; }

        [BsonElement("email")]
        public string email { get; set; }

        [BsonElement("address")]
        public address address { get; set; }

        [BsonElement("phone")]
        public string phone { get; set; }

        [BsonElement("website")]
        public string website { get; set; }

        [BsonElement("company")]
        public company company { get; set; }

    }
}
