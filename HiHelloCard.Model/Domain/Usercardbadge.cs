using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Usercardbadge
{
    public int Id { get; set; }

    public string? BadgePath { get; set; }

    public int? CardId { get; set; }

    public virtual Usercard? Card { get; set; }
}
