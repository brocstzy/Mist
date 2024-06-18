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

    public int RoleId { get; set; }

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<Developer> Developers { get; set; } = new List<Developer>();

    public virtual City? DisplayCity { get; set; }

    public virtual Country? DisplayCountry { get; set; }

    public virtual State? DisplayState { get; set; }

    public virtual ICollection<Friendship> FriendshipRecipients { get; set; } = new List<Friendship>();

    public virtual ICollection<Friendship> FriendshipSenders { get; set; } = new List<Friendship>();

    public virtual ICollection<GroupComment> GroupComments { get; set; } = new List<GroupComment>();

    public virtual ICollection<GroupMember> GroupMembers { get; set; } = new List<GroupMember>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Message> MessageRecipients { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<UserComment> UserCommentPosters { get; set; } = new List<UserComment>();

    public virtual ICollection<UserComment> UserCommentUsers { get; set; } = new List<UserComment>();

    public virtual ICollection<UserGame> UserGames { get; set; } = new List<UserGame>();

    public User()
    {

    }
    public User(string login,
                string password,
                DateTime creationDate,
                string email,
                string nickname,
                decimal balance,
                int countryId,
                int roleId)
    {
        Login = login;
        Password = password;
        CreationDate = creationDate;
        Email = email;
        Nickname = nickname;
        Balance = balance;
        CountryId = countryId;
        RoleId = roleId;
    }
}
