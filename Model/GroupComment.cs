using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class GroupComment
{
    public int Id { get; set; }

    public int GroupId { get; set; }

    public int PosterId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime Timestamp { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User Poster { get; set; } = null!;

    public GroupComment()
    {

    }

    public GroupComment(int groupId, int posterId, string comment, DateTime timestamp)
    {
        GroupId = groupId;
        PosterId = posterId;
        Comment = comment;
        Timestamp = timestamp;
    }


}
