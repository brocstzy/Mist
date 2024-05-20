using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Review
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public int AuthorId { get; set; }

    public int Result { get; set; }

    public string Text { get; set; } = null!;

    public virtual User Author { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;
}
