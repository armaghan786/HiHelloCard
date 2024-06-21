using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Cardfield
{
    public int Id { get; set; }

    public string? Description { get; set; }

    public int? CategoryId { get; set; }

    public virtual Cardfieldcategory? Category { get; set; }

    public virtual ICollection<Usercardfield> Usercardfields { get; set; } = new List<Usercardfield>();
}
