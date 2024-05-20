using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class GameSysReq
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public string Type { get; set; } = null!;

    public string Os { get; set; } = null!;

    public string Cpu { get; set; } = null!;

    public string Memory { get; set; } = null!;

    public string Gpu { get; set; } = null!;

    public string Directx { get; set; } = null!;

    public string Storage { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;
}
