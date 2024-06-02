using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class GameVideo
{
    public int Id { get; set; }

    public int GameId { get; set; }

    public byte[] Video { get; set; } = null!;

    public virtual Game Game { get; set; } = null!;

    public GameVideo()
    {

    }
    public GameVideo(int gameId, byte[] video)
    {
        GameId = gameId;
        Video = video;
    }
}
