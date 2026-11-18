using OpenDCP.Framework.Messaging.Models;
using System;
using System.Collections.Generic;

namespace OpenDCP.Framework.Messaging.Message.MessageDelivery
{
    [Obsolete]
    public class LegacyEmailQueueMessage
    {
        public DateTime ToSend { get; set; }
        public string Email { get; set; }
        public string CC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<LegacyEmailAttachment> Attachments { get; set; }
        public bool IsGxp { get; set; }
    }
}
