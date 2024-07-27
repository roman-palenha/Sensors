using Microsoft.AspNetCore.SignalR;
using Sensors.Domain.Entities;

namespace Sensors.Business.Hubs
{
    public class SensorHub : Hub
    {
        public async Task SendSensorData(List<SensorState> sensor)
        {
            await Clients.All.SendAsync("ReceiveSensorData", sensor);
        }
    }
}
