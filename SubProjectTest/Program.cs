using ManagerServer.Common.Constant;
using ManagerServer.Model.RawData;
using System.Text;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

internal class Program
{
    private static  void  Main(string[] args)
    {
        MqttClient client = new MqttClient("broker.emqx.io");
        var a = Guid.NewGuid().ToString();

        client.Connect(a);
        client.MqttMsgPublishReceived += (s, e) =>
        {
            Console.WriteLine(e.Topic.ToString() +": " + Encoding.UTF8.GetString(e.Message) );
        };
        client.Subscribe(new string[] { Constant.SystemUrl + "/#" }, new byte[] { MqttMsgBase.QOS_LEVEL_AT_LEAST_ONCE });



    }
    class Product
    {
        public string Name { get; set; }
        public string Description { get; set; }

    }
}