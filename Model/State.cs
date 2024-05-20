using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class State
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public string CountryCode { get; set; } = null!;

    public string? FipsCode { get; set; }

    public string? Iso2 { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<City> Cities { get; set; } = new List<City>();

    public virtual Country Country { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
