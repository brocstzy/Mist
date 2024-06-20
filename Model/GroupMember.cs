using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class GroupMember
{
    public GroupMember()
    {
    }

    public int Id { get; set; }

    public int GroupId { get; set; }

    public int MemberId { get; set; }

    public DateTime JoinDate { get; set; }

    public virtual Group Group { get; set; } = null!;

    public virtual User Member { get; set; } = null!;

    public GroupMember(int groupId, int memberId, DateTime joinDate)
    {
        GroupId = groupId;
        MemberId = memberId;
        JoinDate = joinDate;
    }
}
