using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class DeveloperFollower
{
    public int Id { get; set; }

    public int DeveloperId { get; set; }

    public int FollowerId { get; set; }

    public virtual Developer Developer { get; set; } = null!;

    public virtual User Follower { get; set; } = null!;
}
