namespace OpenDCP.Framework.Messaging.Models
{
    public class RabbitMQMessageBusConfig
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
        public string User { get; set; }
        public string SslCertPasspharse { get; set; }
        public string SslCertName { get; set; }
        public bool UseSSL { get; set; }
    }
}
