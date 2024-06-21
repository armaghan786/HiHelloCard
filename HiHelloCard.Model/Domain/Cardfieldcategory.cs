using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Cardfieldcategory
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Cardfield> Cardfields { get; set; } = new List<Cardfield>();
}
