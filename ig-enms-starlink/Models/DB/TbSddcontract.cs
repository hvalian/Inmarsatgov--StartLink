using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddcontract
{
    public int Id { get; set; }

    public string ContractId { get; set; } = null!;

    public string ContractType { get; set; } = null!;

    public string ContractName { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int Cleid { get; set; }

    public short? IsActive { get; set; }

    public virtual TbSddcle Cle { get; set; } = null!;

    public virtual ICollection<TbSddproductOffering> TbSddproductOfferings { get; set; } = new List<TbSddproductOffering>();
}
