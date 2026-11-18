namespace OpenDCP.Framework.Messaging.Message.MessageDelivery
{
    /// <summary>
    /// Data Transfer Object representing a user message attachment.
    /// Can be either embedded binary data or a reference URL.
    /// Supports inline rendering for emails and chat platforms.
    /// </summary>
    public class UserMessageAttachmentDTO
    {
        /// <summary>
        /// Logical name to display or identify the attachment.
        /// E.g. "invoice.pdf" or "chart.png".
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// MIME type of the attachment, e.g. "application/pdf", "image/png".
        /// Useful across email and webhook APIs.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Raw binary data of the attachment (if embedded).
        /// Null if attachment is provided via DataUrl.
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// URL reference to the attachment (for large files, cloud storage, MMS, chat APIs).
        /// Null if attachment is provided via Data.
        /// </summary>
        public string DataUrl { get; set; }

        /// <summary>
        /// Whether this should be rendered inline (e.g. inline image in email/HTML body).
        /// For email this maps to "ContentId", for chat platforms it may translate to inline previews.
        /// </summary>
        public bool IsInline { get; set; } = false;

        /// <summary>
        /// Logical identifier for inline usage.
        /// For email: ContentId (CID), for chat: reference handle.
        /// Optional, only required if IsInline is true.
        /// </summary>
        public string ReferenceId { get; set; }
    }
}
