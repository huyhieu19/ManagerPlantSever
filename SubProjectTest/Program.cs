using ManagerServer.Common.Constant;
using ManagerServer.Model.RawData;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel> (Constant.ConnectionStringMongoDb
                      , Constant.DbName, Constant.Mycollection);
        var result = await mongoDbServiceAsync.DeleteAll ();



    }
    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}