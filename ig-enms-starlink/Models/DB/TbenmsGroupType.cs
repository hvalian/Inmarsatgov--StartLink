﻿using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbenmsGroupType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public int BizServiceId { get; set; }

    public virtual TbBizService BizService { get; set; } = null!;

    public virtual ICollection<TbenmsGroup> TbenmsGroups { get; set; } = new List<TbenmsGroup>();
}