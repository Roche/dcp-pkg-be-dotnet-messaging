using Microsoft.Extensions.Configuration;
using OpenDCP.Framework.Messaging.Classes;
using OpenDCP.Framework.Messaging.Interfaces;
using System;

namespace OpenDCP.Framework.Messaging.Factories
{
    public class MessageBusFactory
    {
        public IMessageBus Load(string driver, IConfiguration configuration)
        {
            switch (driver.ToLower())
            {
                case "rabbitmq":
                    return new RabbitMQMessageBus(configuration);
                default:
                    throw new Exception($"The driver {driver} is not known as a message bus");
            }
        }
    }
}
