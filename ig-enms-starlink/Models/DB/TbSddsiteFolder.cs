using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddsiteFolder
{
    public int Id { get; set; }

    public int Cleid { get; set; }

    public string L2folderId { get; set; } = null!;

    public string L2folderName { get; set; } = null!;

    public DateTime? DateAdded { get; set; }

    public DateTime? DateUpdated { get; set; }

    public short? IsActive { get; set; }

    public virtual TbSddcle Cle { get; set; } = null!;

    public virtual ICollection<TbSddsite> TbSddsites { get; set; } = new List<TbSddsite>();

    public virtual ICollection<TbTerminalProvisionInfo> TbTerminalProvisionInfos { get; set; } = new List<TbTerminalProvisionInfo>();
}
