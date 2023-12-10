using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSddsvn
{
    public int Id { get; set; }

    public int Cleid { get; set; }

    public string Svn { get; set; } = null!;

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? Svnname { get; set; }

    public short? IsActive { get; set; }

    public virtual TbSddcle Cle { get; set; } = null!;

    public virtual ICollection<TbFisasvnSession> TbFisasvnSessions { get; set; } = new List<TbFisasvnSession>();

    public virtual ICollection<TbTerminalsvnConfig> TbTerminalsvnConfigs { get; set; } = new List<TbTerminalsvnConfig>();
}
