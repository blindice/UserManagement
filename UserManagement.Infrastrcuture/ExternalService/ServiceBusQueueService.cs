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
        readonly ServiceBusClient _client;

        public ServiceBusQueueService(ServiceBusClient client)
        {
            _client = client;
        }

        public async Task SendMessageToQueue<T>(T data, string queueName)
        {
            var sender = _client.CreateSender(queueName);
            var body = JsonSerializer.Serialize(data);
            var message = new ServiceBusMessage(body);
            await sender.SendMessageAsync(message);
        }
    }
}
