using OpenDCP.Framework.DataTypes.Equipment.Entities.EventframeContext;
using System;
using System.Collections.Generic;

namespace OpenDCP.Framework.Messaging.Message.EventSubscription
{
    public class EventStartMessage
    {
        public string DeviceWebId { get; set; }
        public int SiteId { get; set; }
        public string EventId { get; set; }
        public DateTime EventStartTime { get; set; }
        public string EventDisplayId { get; set; }
        public List<EventFrameValueDTO> Values { get; set; }
        public bool IsGxP { get; set; }
    }
}
