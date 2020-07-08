using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataAccess
{
    public class ActivityLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        [BsonElement("AuthToken")]
        public string AuthToken { get; set; }
        [BsonElement("IPAddress")]
        public string IPAddress { get; set; }
        [BsonElement("UrlRequest")]
        public string UrlRequest { get; set; }
        [BsonElement("BodyMessage")]
        public string BodyMessage { get; set; }
    }
}
