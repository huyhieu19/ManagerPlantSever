using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

public class MongoDbServiceAsync<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoDbServiceAsync(string connectionString, string databaseName, string collectionName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAll()
    {
        var documents = await _collection.FindAsync(new BsonDocument());
        return await documents.ToListAsync();
    }
    public async Task<T> GetByIndex(int index)
    {
        var filter = Builders<T>.Filter.Empty;
        var options = new FindOptions<T>
        {
            Limit = 1,
            Skip = index
        };
        var documents = await _collection.FindAsync(filter, options);
        return await documents.FirstOrDefaultAsync();
    }
    public async Task<bool> CheckConnection()
    {
        try
        {
            await _collection.Database.RunCommandAsync((Command<BsonDocument>)"{ping:1}");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }
    public async Task<T> GetById(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var document = await _collection.FindAsync(filter);
        return await document.FirstOrDefaultAsync();
    }

    public async Task Create(T document)
    {
        try
        {
            await _collection.InsertOneAsync(document);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    public async Task<bool> Update(string id, T document)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.ReplaceOneAsync(filter, document);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        var result = await _collection.DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }
    public async Task<bool> DeleteAll()
    {
       
        var result = await _collection.DeleteManyAsync(Builders<T>.Filter.Empty);

        return result.DeletedCount > 0;
    }
}
