using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Friendship
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public int RecipientId { get; set; }

    public bool Pending { get; set; }

    public DateTime? FriendsSince { get; set; }

    public virtual User Recipient { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
