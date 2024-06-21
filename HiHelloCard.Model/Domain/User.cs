using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class User
{
    public int Id { get; set; }

    public string? Guid { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsArchive { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual ICollection<Usercard> Usercards { get; set; } = new List<Usercard>();
}
