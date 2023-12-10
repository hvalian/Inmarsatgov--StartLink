using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbNodeTypeTemplate
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool? Active { get; set; }

    public int NodeTypeId { get; set; }

    public string Description { get; set; } = null!;

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbNodeType NodeType { get; set; } = null!;

    public virtual ICollection<TbNodeTypeTemplateMetric> TbNodeTypeTemplateMetrics { get; set; } = new List<TbNodeTypeTemplateMetric>();

    public virtual ICollection<TbNode> TbNodes { get; set; } = new List<TbNode>();
}
