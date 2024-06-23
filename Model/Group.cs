using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Group
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public byte[]? Pfp { get; set; }

    public string Tag { get; set; } = null!;

    public string? Bio { get; set; }

    public bool IsPrivate { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<GroupComment> GroupComments { get; set; } = new List<GroupComment>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual User Owner { get; set; } = null!;

    public Group() { }
    public Group(int ownerId, string name, byte[]? pfp, string tag, string? bio, bool isPrivate, DateTime creationDate)
    {
        OwnerId = ownerId;
        Name = name;
        Pfp = pfp;
        Tag = tag;
        Bio = bio;
        IsPrivate = isPrivate;
        CreationDate = creationDate;
    }

    public int GetMembersCount()
    {
        using (MistContext mc = new MistContext())
        {
            return mc.GroupMembers.Where(x => x.GroupId == this.Id).Count();
        }
    }
}
