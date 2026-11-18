using OpenDCP.Framework.Configuration.Model.Constants.MessageDelivery;
using System.Collections.Generic;

namespace OpenDCP.Framework.Messaging.Message.MessageDelivery
{
    public class UserMessageDTO
    {
        /// <summary>
        /// Gets or sets the delivery channels through which the message will be sent.
        /// Defaults to email.
        /// </summary>
        public List<MessageDeliveryChannelType> MessageChannels { get; set; }
            = new List<MessageDeliveryChannelType> { MessageDeliveryChannelType.Mail };

        /// <summary>
        /// Gets or sets the subject of the message.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the plain-text message content.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the formatted message content (e.g., HTML).
        /// </summary>
        public string FormattedMessage { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the message template identifier or template content
        /// used to generate the message.
        /// </summary>
        public string MessageTemplate { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the format of the message content.
        /// </summary>
        public MessageDeliveryMessageFormat Format { get; set; }

        /// <summary>
        /// Gets or sets the collection of attachments included with the message.
        /// </summary>
        public List<UserMessageAttachmentDTO> Attachments { get; set; } = new List<UserMessageAttachmentDTO>();

        /// <summary>
        /// Gets or sets the collection of user identifiers who will receive the message.
        /// </summary>
        public List<int> UserIds { get; set; } = new List<int>();

        /// <summary>
        /// Gets or sets a value indicating whether the message is subject
        /// to GxP (Good Practice) compliance requirements.
        /// </summary>
        public bool IsGxP { get; set; } = false;
    }
}
