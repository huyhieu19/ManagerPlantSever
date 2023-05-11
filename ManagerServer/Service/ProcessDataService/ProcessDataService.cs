using Common.Model.SlaveModel;
using ManagerServer.Constan;
using ManagerServer.Database;
using ManagerServer.Database.Entity;
using ManagerServer.Model.RawData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ManagerServer.Service.ProcessDataService
{
    public class ProcessDataService : BackgroundService
    {
        private MongoDbServiceAsync<RawDataModel> mongoDbServiceAsync;
        private DbContextOptionsBuilder<ManagerDbContext> optionsBuilder = new DbContextOptionsBuilder<ManagerDbContext>();
        public ProcessDataService()
        {
            mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel>(Constans.ConnectionStringMongoDb
                    , Constans.DbName, Constans.Mycollection);
            optionsBuilder.UseSqlServer(Constans.ConnectionStringSqlServer);         
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int i = 0;
            while (!stoppingToken.IsCancellationRequested)
            {

               try
                {
                    if (!await mongoDbServiceAsync.CheckConnection())
                    {
                        mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel>(Constans.ConnectionStringMongoDb
                        , Constans.DbName, Constans.Mycollection);
                        Console.WriteLine("MongoDb Conneted");
                    }
                    var query = await QueryDataAsycn(i);
                    if (query != null)
                    {
                       
                        var dataToSave = new DataProcessingEntity();
                       
                         var isProcessSucess =  ProcessData(query,dataToSave);
                        if (isProcessSucess)
                        {
                            //await SaveData(dataToSave);
                            Console.WriteLine("Save success!");

                        }
                        i++;
                    }
                }

                catch(Exception ex)
                {
                   //handel
                }

                await Task.Delay(2000, stoppingToken);
            }
        }

        private async Task SaveData(DataProcessingEntity dataToSave)
        {

            using (var context = new ManagerDbContext(optionsBuilder.Options))
            {
                try
                {
                        await context.DataProcessingEntites.AddAsync(dataToSave);
                        await context.SaveChangesAsync();
                }
                         
                    
                catch (Exception ex)
                {

                    Console.WriteLine("eror in Savedata: " + ex.Message);
                }
            };        
        }

        private static bool ProcessData(RawDataModel data, DataProcessingEntity dataSave)
        {
           try
            {
                var tempArr = data.Topic.Split("/");

                TimeZoneInfo ictZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                DateTime ictTime = TimeZoneInfo.ConvertTimeFromUtc(data.RetreveAt.Value, ictZone);
                dataSave.FarmId = int.Parse(tempArr[1]);
                dataSave.SmallHoldingId = int.Parse(tempArr[2]);
                dataSave.Mode = tempArr[3];
                dataSave.Topic = tempArr[4];
                dataSave.Payload = data.Payload;
                dataSave.RetrieveAt = ictTime;
                return true;
            }
            catch(Exception ex)
            {
                
                Console.WriteLine("Process Eror: "+ ex.Message);
                return false;
            }

        }

        private async Task<RawDataModel> QueryDataAsycn(int i)
        {
           try
            {
                var reuslt = await mongoDbServiceAsync.GetByIndex(i);
                return reuslt;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Querydata error: "+ ex.Message);
                return null;
            }
        }
    }
}
