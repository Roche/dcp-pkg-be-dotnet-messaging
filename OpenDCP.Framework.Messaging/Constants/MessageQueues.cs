using System;

namespace OpenDCP.Framework.Messaging.Constants
{
    public static class MessageQueues
    {
        public const string EventBus = "dcp.core.v2.eventbus";
        public const string MessageDelivery = "dcp.core.v2.messagedelivery";
        public const string Default = "dcp.core.v2.default";
        [Obsolete("This is the old queue - using core-v1 messaging format which will be removed")]
        public const string LegacyMessageDelivery = "CoreEmailQueue";
        [Obsolete("This is the old queue - using core-v1 messaging format which will be removed")]
        public const string LegacyDefault = "DefaultCoreQueue";
        [Obsolete("This is the old queue - using core-v1 messaging format which will be removed")]
        public const string LegacyEventBus = "CoreEventBusQueue";
    }
}
