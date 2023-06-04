using ManagerServer.Common.Constant;
using ManagerServer.Common.Enum;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ManagerServer.Service
{
    public class FakeSensorDevice
    {
        private readonly MqttClient client;
        private readonly string clientId = Guid.NewGuid ().ToString ();
        private readonly string topicTest = Constant.SystemUrl;
        public FakeSensorDevice()
        {
            client = new MqttClient ("broker.emqx.io");
            client.MqttMsgPublishReceived += (s, e) =>
            {
                this.HandleMessage (e);
            };
            client.Connect (clientId);
            this.SubcribeSystemDefault ();
            this.SenFakeData ();
        }
        public void Subcribe(string[] topics)
        {
            client.Subscribe (topics, new byte[] { 0 });

        }
        public bool Start()
        {
            return client.IsConnected;
        }
        public void Publish(string topic, string payload)
        {
            client.Publish (topic, Encoding.UTF8.GetBytes (payload));
        }
        public void SubcribeSystemDefault()
        {
            //this.Subcribe (new string[] { topicTest + "/1" + "/R" + "/Temparute" });
            //this.Subcribe (new string[] { topicTest + "/1" + "/R" + "/Mosuli" });
            //this.Subcribe (new string[] { topicTest + "/1" + "/R" + "/Brightness" });
        }
        public async Task SenFakeData()
        {
            var task = new Task (() =>
            {
                while ( true )
                {
                    var temp = new Random ().Next (30, 37);
                    this.Publish (topicTest + "/1" + "/R" + $"/{TopicType.Temperature}", temp.ToString ());

                    var mosuli = new Random ().Next (70, 80);
                    this.Publish (topicTest + "/1" + "/R" + $"/{TopicType.Moisture}", mosuli.ToString ());
                    var human = new Random().Next(70, 80);
                    this.Publish(topicTest + "/1" + "/R" + $"/{TopicType.Humidity}", human.ToString());
                    var bigh = new Random().Next(70, 80);
                    this.Publish(topicTest + "/1" + "/R" + $"/{TopicType.Brightness}", bigh.ToString());


                    this.Publish (topicTest + "/1" + "/W" + $"/{TopicType.IsOnFan}", 0.ToString ());
                    this.Publish (topicTest + "/1" + "/W" + $"/{TopicType.IsOnWater}", 0.ToString ());
                    this.Publish (topicTest + "/1" + "/W" + $"/{TopicType.IsOnLamp}", 0.ToString ());

                    Thread.Sleep (4000);
                }
            });
            task.Start ();
            await task;

        }
        private void HandleMessage(MqttMsgPublishEventArgs e)
        {

        }
    }
}
