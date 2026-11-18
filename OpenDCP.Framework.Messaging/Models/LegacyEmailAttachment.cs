using System;

namespace OpenDCP.Framework.Messaging.Models
{
    [Obsolete]
    public class LegacyEmailAttachment
    {
        public string FileName { get; set; }
        public bool IsEmbedded { get; set; }
        /// <summary>
        /// File extension
        /// </summary>
        public string MediaType { get; set; }
        public string Data { get; set; }
        public Guid ContentId { get; set; }
    }
}
