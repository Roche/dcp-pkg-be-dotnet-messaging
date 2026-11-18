using System.Collections.Generic;

namespace OpenDCP.Framework.Messaging.Models
{
    public class BasicPropertiesDTO
    {
        public IDictionary<string, object> Headers { get; set; }
        public string Type { get; set; }
        public string ContentType { get; set; }
        public string ContentEncoding { get; set; }
    }
}
