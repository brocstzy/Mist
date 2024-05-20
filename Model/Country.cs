using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Iso3 { get; set; }

    public string? NumericCode { get; set; }

    public string? Iso2 { get; set; }

    public string? Phonecode { get; set; }

    public string? Currency { get; set; }

    public string? CurrencyName { get; set; }

    public string? CurrencySymbol { get; set; }

    public string? Native { get; set; }

    public string? Nationality { get; set; }

    public string? Emoji { get; set; }

    public string? EmojiU { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual ICollection<State> States { get; set; } = new List<State>();

    public virtual ICollection<User> UserCountries { get; set; } = new List<User>();

    public virtual ICollection<User> UserDisplayCountries { get; set; } = new List<User>();
}
