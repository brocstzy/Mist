using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Message
{
    public Message()
    {
    }

    public int Id { get; set; }

    public int SenderId { get; set; }

    public int RecipientId { get; set; }

    public string Message1 { get; set; } = null!;

    public DateTime Datetime { get; set; }

    public bool Seen { get; set; }

    public virtual User Recipient { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;

    public Message(int senderId, int recipientId, string message1, DateTime datetime, bool seen)
    {
        SenderId = senderId;
        RecipientId = recipientId;
        Message1 = message1;
        Datetime = datetime;
        Seen = seen;
    }
}
