using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Submit_Image.ImageMongoModel
{
    public class Image
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public byte[] Image_ { get; set; }
        public string GUID { get; set; }

    }
}
