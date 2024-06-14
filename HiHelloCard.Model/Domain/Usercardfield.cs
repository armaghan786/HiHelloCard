using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Usercardfield
{
    public int Id { get; set; }

    public int? CardFieldId { get; set; }

    public int? CardId { get; set; }

    public string? Link { get; set; }

    public string? Description { get; set; }

    public virtual Usercard? Card { get; set; }

    public virtual Cardfield? CardField { get; set; }
}
