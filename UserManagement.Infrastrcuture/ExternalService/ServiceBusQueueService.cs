using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagement.Application.Interfaces;

namespace UserManagement.Infrastrcuture.ExternalService
{
    public sealed class ServiceBusQueueService : IServiceBusQueueService
    {
        readonly IConfiguration _config;

        public ServiceBusQueueService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMessageToQueue<T>(T data, string queueName)
        {
            var client = new ServiceBusClient("Endpoint=sb://ludwigsb.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=2r8lxe4pFGnemHbdLn88gdPiDAmx2sTIp+ASbKGx8j0=");
            var sender = client.CreateSender(queueName);
            var body = JsonSerializer.Serialize(data);
            var message = new ServiceBusMessage(body);
            await sender.SendMessageAsync(message);
        }
    }
}
