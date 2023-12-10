using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddcle
{
    public int Id { get; set; }

    public int Cle { get; set; }

    public string Name { get; set; } = null!;

    public string BillingAccountId { get; set; } = null!;

    public string L1folderId { get; set; } = null!;

    public string ResourcePoolOwnerId { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public int? Varid { get; set; }

    public short? IsActive { get; set; }

    public virtual ICollection<TbSddcontract> TbSddcontracts { get; set; } = new List<TbSddcontract>();

    public virtual ICollection<TbSddsiteFolder> TbSddsiteFolders { get; set; } = new List<TbSddsiteFolder>();

    public virtual ICollection<TbSddsvn> TbSddsvns { get; set; } = new List<TbSddsvn>();

    public virtual ICollection<TbTerminalProvisionInfo> TbTerminalProvisionInfos { get; set; } = new List<TbTerminalProvisionInfo>();

    public virtual TbVar? Var { get; set; }
}
