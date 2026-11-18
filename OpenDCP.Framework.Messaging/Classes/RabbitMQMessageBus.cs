using Microsoft.Extensions.Configuration;
using OpenDCP.Framework.Exceptions;
using OpenDCP.Framework.Messaging.Constants;
using OpenDCP.Framework.Messaging.Interfaces;
using OpenDCP.Framework.Messaging.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CertificateReader = OpenDCP.Framework.Hosting.Classes.CertificateReader;
namespace OpenDCP.Framework.Messaging.Classes
{
    public class RabbitMQMessageBus : IMessageBus
    {
        private readonly IConfiguration _configuration;
        private static IConnection _connection;
        private static IModel _channel;
        private static ConcurrentDictionary<ulong, string> deliveryTags = new ConcurrentDictionary<ulong, string>();

        public RabbitMQMessageBus(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task BindConsumerAsync(string queueName, Func<string, string, string, Task> consumerCallBack)
        {
            if (_channel == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.Received += async (bc, ea) =>
            {
                try
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    var routingKey = ea.RoutingKey;

                    var type = ea.BasicProperties.Type;

                    await consumerCallBack(message, type, routingKey);

                    // ... process the message
                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch
                {
                    _channel.BasicNack(ea.DeliveryTag, false, false);
                }

                await Task.Yield();
            };

            _channel.BasicConsume(queueName, false, consumer);
        }
        public void Connect(string connnectionName)
        {
            var certReader = new CertificateReader();

            var factory = new ConnectionFactory
            {
                // connection that will recover automatically
                AutomaticRecoveryEnabled = true,
                DispatchConsumersAsync = true,
                HostName = _configuration["MessageBusProvider:Settings:Host"],
                UserName = _configuration["MessageBusProvider:Settings:User"],
                Password = _configuration["MessageBusProvider:Settings:Password"],
                Port = int.Parse(_configuration["MessageBusProvider:Settings:Port"]),
            };

            if (bool.Parse(_configuration["MessageBusProvider:Settings:UseSSL"]))
            {
                factory.Ssl = new SslOption
                {
                    CertPassphrase = _configuration["MessageBusProvider:Settings:SslCertPasspharse"],
                    ServerName = Dns.GetHostEntry("LocalHost").HostName,
                    Version = System.Security.Authentication.SslProtocols.Tls12,
                    Enabled = true,
                    Certs = certReader.FindCertificateByName(_configuration["MessageBusProvider:Settings:SslCertName"]),
                    AcceptablePolicyErrors = SslPolicyErrors.RemoteCertificateNameMismatch
                };
            }

            _connection = factory.CreateConnection(connnectionName);
            _channel = _connection.CreateModel();
        }
        public void InitializeExchange(string exchangeName, string exchangeType, bool exchangeDurable)
        {
            if (_channel == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }

            _channel.ExchangeDeclare(exchange: exchangeName,
                                 type: exchangeType, durable: exchangeDurable, autoDelete: false, arguments: null);

        }
        public void InitializeQueue(string queueName)
        {
            if (_channel == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }

            _channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
        }

        public void BindQueue(string queueName, string exchangerName, List<string> keys)
        {
            if (_channel == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }

            foreach (var key in keys)
            {
                _channel.QueueBind(queueName, exchangerName, key, null);
            }
        }
        public void DirectPublish(JsonElement message, string queueName, BasicPropertiesDTO basicProperties = null)
        {
            if (_channel == null || _connection == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }
            if (_connection.IsOpen)
            {
                var body = Encoding.UTF8.GetBytes(message.ToString());
                //This will be dynamic and values into array will be added base on if given device webId
                //is presented into list from Cache, chromTa , BatchNotificaion
                //https://www.rabbitmq.com/blog/2011/03/23/sender-selected-distribution/


                //https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html
                deliveryTags.TryAdd(_channel.NextPublishSeqNo, message.ToString());

                IBasicProperties props = _channel.CreateBasicProperties();

                if (basicProperties != null)
                {
                    if (basicProperties.Headers != null)
                    {
                        props.Headers = basicProperties.Headers;
                    }
                    if (!string.IsNullOrEmpty(basicProperties.Type))
                    {
                        props.Type = basicProperties.Type;
                    }

                    if (!string.IsNullOrEmpty(basicProperties.ContentEncoding))
                    {
                        props.ContentEncoding = basicProperties.ContentEncoding;
                    }

                    if (!string.IsNullOrEmpty(basicProperties.ContentType))
                    {
                        props.ContentType = basicProperties.ContentType;
                    }
                }

                _channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    basicProperties: props,
                    body: body
                );
            }
        }
        public void Publish(JsonElement message, string exchangerName, List<string> keys, BasicPropertiesDTO basicProperties = null)
        {
            if (_channel == null || _connection == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }
            if (_connection.IsOpen)
            {
                var body = Encoding.UTF8.GetBytes(message.ToString());
                //This will be dynamic and values into array will be added base on if given device webId
                //is presented into list from Cache, chromTa , BatchNotificaion
                //https://www.rabbitmq.com/blog/2011/03/23/sender-selected-distribution/


                //https://www.rabbitmq.com/tutorials/tutorial-seven-dotnet.html
                deliveryTags.TryAdd(_channel.NextPublishSeqNo, message.ToString());

                IBasicProperties props = _channel.CreateBasicProperties();

                if (basicProperties != null)
                {
                    if (basicProperties.Headers != null)
                    {
                        props.Headers = basicProperties.Headers;
                    }
                    if (!string.IsNullOrEmpty(basicProperties.Type))
                    {
                        props.Type = basicProperties.Type;
                    }

                    if (!string.IsNullOrEmpty(basicProperties.ContentEncoding))
                    {
                        props.ContentEncoding = basicProperties.ContentEncoding;
                    }

                    if (!string.IsNullOrEmpty(basicProperties.ContentType))
                    {
                        props.ContentType = basicProperties.ContentType;
                    }
                }

                _channel.BasicPublish(
                    exchange: exchangerName,
                    routingKey: keys.FirstOrDefault(),
                    basicProperties: props,
                    body: body
                );
            }
        }
        //Check this method it always returns false
        public async Task<bool> IsMessageDeliveredAsync()
        {
            if (_channel == null)
            {
                throw new DCPBadRequestException(ErrorMessages.NotConnectedToMessageBroker);
            }

            bool isMessageDelivered = false;
            _channel.BasicAcks += async (sender, ea) =>
            {
                deliveryTags.TryGetValue(ea.DeliveryTag, out string body);

                CleaningDeliveryTags(ea.DeliveryTag, ea.Multiple);
                isMessageDelivered = true;

            };
            return isMessageDelivered;
        }

        public void Disconnect()
        {
            if (_channel != null && _channel.IsOpen)
            {
                _channel.Close();
                _channel = null;
            }

            if (_connection != null && _connection.IsOpen)
            {
                _connection.Close();
                _connection = null;
            }
        }

        private static void CleaningDeliveryTags(ulong sequenceNumber, bool multiple)
        {
            if (multiple)
            {
                var confirmed = deliveryTags.Where(k => k.Key <= sequenceNumber);
                foreach (var entry in confirmed)
                    deliveryTags.TryRemove(entry.Key, out _);
            }
            else
                deliveryTags.TryRemove(sequenceNumber, out _);
        }
    }
}