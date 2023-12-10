using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbCustomerBizMap
{
    public int Id { get; set; }

    public string CustomerId { get; set; } = null!;

    public int BizId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbBizService Biz { get; set; } = null!;

    public virtual ICollection<TbVarCustomerBizMap> TbVarCustomerBizMaps { get; set; } = new List<TbVarCustomerBizMap>();
}
