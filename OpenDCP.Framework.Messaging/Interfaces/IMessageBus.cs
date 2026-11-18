using OpenDCP.Framework.Messaging.Models;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenDCP.Framework.Messaging.Interfaces
{
    public interface IMessageBus
    {
        Task BindConsumerAsync(string queueName, Func<string, string, string, Task> consumerCallBack);
        void Connect(string connnectionName);
        void InitializeExchange(string exchangeName, string exchangeType, bool exchangeDurable);
        void InitializeQueue(string queueName);
        void BindQueue(string queueName, string exchangerName, List<string> keys);
        void DirectPublish(JsonElement message, string queueName, BasicPropertiesDTO basicProperties = null);
        void Publish(JsonElement message, string exchangerName, List<string> keys, BasicPropertiesDTO basicProperties = null);
        Task<bool> IsMessageDeliveredAsync();
        void Disconnect();
    }
}
