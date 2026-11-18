namespace OpenDCP.Framework.Messaging.Message.EventBus
{
    public class DeleteSiteMessage
    {
        public int Id { get; set; }
        public bool CleanHistoryTables { get; set; }
        public int CurrentUserId { get; set; }
        public bool IsGxp { get; set; }
    }
}
