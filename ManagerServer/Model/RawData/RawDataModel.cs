namespace ManagerServer.Model.RawData
{
    public class RawDataModel
    {
        [MongoDB.Bson.Serialization.Attributes.BsonElement("_id")]
        public MongoDB.Bson.ObjectId Id { get; set; }
        public string? Topic { get; set; }
        public string? Payload { get; set; }
        public DateTime? RetreveAt { get; set; } = DateTime.Now;
    }
}
