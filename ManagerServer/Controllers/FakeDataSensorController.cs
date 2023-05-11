using ManagerServer.Service;
using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class FakeDataSensorController : ControllerBase
    {
        private readonly FakeSensorDevice sensorDevice;
        public FakeDataSensorController()
        {
            sensorDevice = new FakeSensorDevice();
        }

        [HttpPost("publish")]
        public void Publish(string topic, string payload)
        {
            sensorDevice.Publish(topic, payload);
        }
        [HttpGet("start")]
        public bool Start()
        {
            return sensorDevice.Start();
        }
    }
}
