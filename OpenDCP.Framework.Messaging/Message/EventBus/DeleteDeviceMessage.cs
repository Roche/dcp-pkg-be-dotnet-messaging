namespace OpenDCP.Framework.Messaging.Message.EventBus
{
    public class DeleteDeviceMessage
    {
        public string DeviceWebId { get; set; }
        public int SiteId { get; set; }
        public bool IsGxp { get; set; }
    }
}
