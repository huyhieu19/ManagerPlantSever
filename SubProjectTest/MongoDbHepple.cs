using MongoDB.Driver;

public class MongoHelper
{
    private readonly IMongoDatabase _database = null;

    public MongoHelper(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        if (client != null)
            _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}