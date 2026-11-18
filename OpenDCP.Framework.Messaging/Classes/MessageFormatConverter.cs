using OpenDCP.Framework.Messaging.Message.EventSubscription;
using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OpenDCP.Framework.Messaging.Classes
{
    [Obsolete]
    public static class MessageFormatConverter
    {
        public static LegacyEventMessage Convert(EventStartMessage msg)
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };

            return new LegacyEventMessage
            {
                DeviceWebId = msg.DeviceWebId,
                BadValueDetected = msg.Values.Any(x => !x.Good),
                BatchId = msg.EventDisplayId,
                BatchStartTime = msg.EventStartTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                EventFrameId = msg.EventId,
                IsGxp = msg.IsGxP,
                SiteId = msg.SiteId,
                BatchValues = JsonSerializer.Serialize(
                msg.Values.Select(v => new
                {
                    v.Name,
                    Value = new
                    {
                        v.Value,
                        v.Good
                    }
                }),
                options)
            };
        }

        public static LegacyEventMessage Convert(EventEndMessage msg)
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };

            return new LegacyEventMessage
            {
                DeviceWebId = msg.DeviceWebId,
                BadValueDetected = msg.Values.Any(x => !x.Good),
                BatchId = msg.EventDisplayId,
                BatchStartTime = msg.EventStartTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                BatchEndTime = msg.EventEndTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                EventFrameId = msg.EventId,
                IsGxp = msg.IsGxP,
                SiteId = msg.SiteId,
                BatchValues = JsonSerializer.Serialize(
                msg.Values.Select(v => new
                {
                    v.Name,
                    Value = new
                    {
                        v.Value,
                        v.Good
                    }
                }),
                options)
            };
        }

        public static LegacyEventMessage Convert(EventMetaDataChangeMessage msg)
        {
            var options = new JsonSerializerOptions
            {
                NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals
            };

            return new LegacyEventMessage
            {
                DeviceWebId = msg.DeviceWebId,
                BadValueDetected = msg.Values.Any(x => !x.Good),
                BatchId = msg.EventDisplayId,
                BatchStartTime = msg.EventStartTime.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"),
                EventFrameId = msg.EventId,
                IsGxp = msg.IsGxP,
                SiteId = msg.SiteId,
                BatchValues = JsonSerializer.Serialize(
                msg.Values.Select(v => new
                {
                    v.Name,
                    Value = new
                    {
                        v.Value,
                        v.Good
                    }
                }),
                options)
            };
        }
    }
}
