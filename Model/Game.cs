using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Game
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public decimal UsdPrice { get; set; }

    public byte[]? LibraryLogo { get; set; }

    public byte[]? BackgroundLibraryImage { get; set; }

    public byte[]? VerticalLibraryImage { get; set; }

    public string Bio { get; set; } = null!;

    public string Description { get; set; } = null!;

    public DateOnly ReleaseDate { get; set; }

    public int DeveloperId { get; set; }

    public byte[] LibraryIcon { get; set; } = null!;

    public byte[] FrontImage { get; set; } = null!;

    public byte[] StoreSmallImage { get; set; } = null!;

    public virtual Developer Developer { get; set; } = null!;

    public virtual ICollection<GameImage> GameImages { get; set; } = new List<GameImage>();

    public virtual ICollection<GameSysReq> GameSysReqs { get; set; } = new List<GameSysReq>();

    public virtual ICollection<GameVideo> GameVideos { get; set; } = new List<GameVideo>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();

    public Game()
    {

    }
    public Game(
                string name,
                decimal usdPrice,
                byte[]? libraryLogo,
                byte[]? backgroundLibraryImage,
                byte[]? verticalLibraryImage,
                string bio,
                string description,
                DateOnly releaseDate,
                int developerId,
                byte[] libraryIcon,
                byte[] frontImage,
                byte[] storeSmallImage
                )
    {
        Name = name;
        UsdPrice = usdPrice;
        LibraryLogo = libraryLogo;
        BackgroundLibraryImage = backgroundLibraryImage;
        VerticalLibraryImage = verticalLibraryImage;
        Bio = bio;
        Description = description;
        ReleaseDate = releaseDate;
        DeveloperId = developerId;
        LibraryIcon = libraryIcon;
        FrontImage = frontImage;
        StoreSmallImage = storeSmallImage;
    }
}
