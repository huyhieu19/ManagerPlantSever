using ManagerServer.Constan;
using ManagerServer.Model.RawData;
using uPLibrary.Networking.M2Mqtt;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel>(Constans.ConnectionStringMongoDb
                      , Constans.DbName, Constans.Mycollection);
        var result = await mongoDbServiceAsync.DeleteAll();



    }
    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}