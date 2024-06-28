using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Carddesign
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Content { get; set; }

    public virtual ICollection<Usercard> Usercards { get; set; } = new List<Usercard>();
}
