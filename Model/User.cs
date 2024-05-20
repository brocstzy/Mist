using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public byte[]? Pfp { get; set; }

    public string Nickname { get; set; } = null!;

    public decimal Balance { get; set; }

    public int CountryId { get; set; }

    public int? DisplayCountryId { get; set; }

    public int? DisplayStateId { get; set; }

    public int? DisplayCityId { get; set; }

    public string? Bio { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Developer> Developers { get; set; } = new List<Developer>();

    public virtual City? DisplayCity { get; set; }

    public virtual Country? DisplayCountry { get; set; }

    public virtual State? DisplayState { get; set; }

    public virtual ICollection<Friendship> FriendshipRecipients { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipSenders { get; set; } = new List<Friendship>();

    public virtual ICollection<Message> MessageRecipients { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();
}
