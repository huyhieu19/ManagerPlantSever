//using System;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using ManagerServer.Common.Constant;
//using ManagerServer.Database;
//using ManagerServer.Model.RawData;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Hosting;
//using MongoDB.Driver.Core.Configuration;
//using uPLibrary.Networking.M2Mqtt;
//using uPLibrary.Networking.M2Mqtt.Messages;

//namespace MyProject.Services
//{
//    public class ListeningService : BackgroundService
//    {
//        private readonly MqttClient _client;
//        private readonly string _clientId;
//        private readonly DbContextOptionsBuilder<ManagerDbContext> builder =
//        new DbContextOptionsBuilder<ManagerDbContext>().UseSqlServer(Constant.ConnectionStringSqlServer);

//        public ListeningService()
//        {
//            _client = new MqttClient(Constant.BrokerURL);
//            _clientId = Guid.NewGuid().ToString();
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _client.MqttMsgPublishReceived += async (s, e) =>
//            {
//                await SaveToDb(e);
//            };
//            while (!stoppingToken.IsCancellationRequested)
//            {

//                try
//                {
//                    if (!_client.IsConnected)
//                    {
//                        _client.Connect(_clientId);
//                        Console.WriteLine("MQTT connected");
//                        _client.Subscribe(new string[] { Constant.SystemUrl + "/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });
//                    }

//                }
//                catch (Exception e)
//                {
//                    Console.WriteLine("fail listening main" + e.Message);
//                }
//                await Task.Delay(2000, stoppingToken);
//            }
//        }

//        private async Task<bool> SaveToDb(MqttMsgPublishEventArgs e)
//        {
//            try
//            {
//                //using (var context = new ManagerDbContext(builder.Options))
//                //{
//                //    //save
//                //}
//                return await Task.FromResult(true);

//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("save erro:" + ex.Message);
//                throw;
//            }
//        }
//    }
//}
