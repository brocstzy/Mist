using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class UserGame
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int GameId { get; set; }

    public DateTime AcquiredAt { get; set; }

    public virtual Game Game { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
