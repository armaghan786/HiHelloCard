using System;
using System.Collections.Generic;

namespace HiHelloCard.Model.Domain;

public partial class Usercard
{
    public int Id { get; set; }

    public string? Guid { get; set; }

    public string? Name { get; set; }

    public string? Prefix { get; set; }

    public string? FirstName { get; set; }

    public string? MiddleName { get; set; }

    public string? LastName { get; set; }

    public string? Suffix { get; set; }

    public string? Accreditations { get; set; }

    public string? PreferredName { get; set; }

    public string? MaidenName { get; set; }

    public string? Pronouns { get; set; }

    public string? ProfilePhoto { get; set; }

    public int? DesignId { get; set; }

    public string? UserId { get; set; }

    public string? Color { get; set; }

    public string? Logo { get; set; }

    public string? AffiliateTitle { get; set; }

    public string? Department { get; set; }

    public string? Company { get; set; }

    public string? Headline { get; set; }

    public bool? IsArchive { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public DateTime? UpdatedDateTime { get; set; }

    public virtual Carddesign? Design { get; set; }

    public virtual Aspnetuser? User { get; set; }

    public virtual ICollection<Usercardbadge> Usercardbadges { get; set; } = new List<Usercardbadge>();

    public virtual ICollection<Usercardfield> Usercardfields { get; set; } = new List<Usercardfield>();
}
