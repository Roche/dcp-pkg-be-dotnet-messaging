using System;

namespace OpenDCP.Framework.Messaging.Message.EventSubscription
{
    [Obsolete]
    public class LegacyEventMessage
    {
        public string DeviceWebId { get; set; }
        public int SiteId { get; set; }
        public string EventFrameId { get; set; }
        public string BatchStartTime { get; set; }
        public string BatchEndTime { get; set; }
        public string BatchId { get; set; }
        public string BatchValues { get; set; }
        public bool BadValueDetected { get; set; }
        public bool IsGxp { get; set; }
    }
}