using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Developer
{
    public int Id { get; set; }

    public int OwnerId { get; set; }

    public byte[]? Pfp { get; set; }

    public byte[]? BackgroundImage { get; set; }

    public string Name { get; set; } = null!;

    public string Bio { get; set; } = null!;

    public virtual ICollection<DeveloperFollower> DeveloperFollowers { get; set; } = new List<DeveloperFollower>();

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual User Owner { get; set; } = null!;

    public Developer()
    {

    }
    public Developer(int ownerId, byte[]? pfp, byte[]? backgroundImage, string name, string bio)
    {
        OwnerId = ownerId;
        Pfp = pfp;
        BackgroundImage = backgroundImage;
        Name = name;
        Bio = bio;
    }
}
