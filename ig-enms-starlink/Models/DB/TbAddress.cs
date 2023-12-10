using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbAddress
{
    public string AddressReferenceId { get; set; } = null!;

    public string AddressLines { get; set; } = null!;

    public string? Locality { get; set; }

    public string? AdministrativeArea { get; set; }

    public string? AdministrativeAreaCode { get; set; }

    public string? Region { get; set; }

    public string? RegionCode { get; set; }

    public string? PostalCode { get; set; }

    public string? FormattedAddress { get; set; }

    public string? Latitude { get; set; }

    public string? Longitude { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateUpdated { get; set; }

    public virtual ICollection<TbServiceLine> TbServiceLines { get; set; } = new List<TbServiceLine>();
}
