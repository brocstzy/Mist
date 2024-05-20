using System;
using System.Collections.Generic;

namespace Mist.Model;

public partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int StateId { get; set; }

    public string StateCode { get; set; } = null!;

    public int CountryId { get; set; }

    public string CountryCode { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual State State { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
