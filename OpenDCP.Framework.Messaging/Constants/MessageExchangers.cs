using System;

namespace OpenDCP.Framework.Messaging.Constants
{
    public static class MessageExchangers
    {
        public const string EventBus = "dcp.eventbus.v2.topic";
        public const string EventSubscription = "dcp.eventsubscription.v2.direct";
        [Obsolete("This is the old queue - using core-v1 messaging format which will be removed")]
        public const string LegacyEventBus = "DCPEventBusExchange";
        [Obsolete("This is the old queue - using core-v1 messaging format which will be removed")]
        public const string LegacyEventSubscription = "DCPActiveBatchExchange";
    }
}
