using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class UserComment
{
    public UserComment()
    {
    }

    public int Id { get; set; }

    public int UserId { get; set; }

    public int PosterId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual User Poster { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public UserComment(int userId, int posterId, string comment, DateTime timestamp)
    {
        UserId = userId;
        PosterId = posterId;
        Comment = comment;
        Timestamp = timestamp;
    }
}
