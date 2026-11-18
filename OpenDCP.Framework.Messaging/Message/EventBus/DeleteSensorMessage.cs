namespace OpenDCP.Framework.Messaging.Message.EventBus
{
    public class DeleteSensorMessage
    {
        public int ModelId { get; set; }
        public int SiteId { get; set; }
        public bool IsGxp { get; set; }
    }
}
