using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ManagerServer.Constan;
using ManagerServer.Model.RawData;
using Microsoft.Extensions.Hosting;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace MyProject.Services
{
    public class ListeningService : BackgroundService
    {
        private readonly MqttClient _client;
        private readonly string _clientId;
        private  MongoDbServiceAsync<RawDataModel> mongoDbServiceAsync;

        public ListeningService()
        {
            _client = new MqttClient(Constans.BrokerURL);
            _clientId = Guid.NewGuid().ToString();
            mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel>(Constans.ConnectionStringMongoDb
                    ,Constans.DbName,Constans.Mycollection);          
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _client.MqttMsgPublishReceived += async (s, e) =>
            {
                await SaveToDb(e);
            };
            while (!stoppingToken.IsCancellationRequested)
            {
               
               try
                {
                    if (!_client.IsConnected)
                    {
                        _client.Connect(_clientId);
                        Console.WriteLine("MQTT connected");
                    }

                    _client.Subscribe(new string[] { Constans.SystemUrl + "/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
                    if (!await mongoDbServiceAsync.CheckConnection())
                    {
                        mongoDbServiceAsync = new MongoDbServiceAsync<RawDataModel>(Constans.ConnectionStringMongoDb
                        , Constans.DbName, Constans.Mycollection);
                        Console.WriteLine("MongoDb Conneted");
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine("fail listening main"+e.Message);
                }
                await Task.Delay(2000, stoppingToken);
            }
        }

        private async Task<bool> SaveToDb(MqttMsgPublishEventArgs e)
        {
           try
            {
              await mongoDbServiceAsync.Create(new RawDataModel()
                    {
                        Topic = e.Topic,
                        Payload = Encoding.UTF8.GetString(e.Message),
                        RetreveAt = DateTime.Now,
                    });            
                return await Task.FromResult(true);
               
            }
            catch(Exception ex)
            {
                Console.WriteLine("save erro:"+ ex.Message);
                throw;
            }
        }
    }
}
