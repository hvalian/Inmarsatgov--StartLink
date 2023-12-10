using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbVarCustomerBizMap
{
    public int Id { get; set; }

    public string VarId { get; set; } = null!;

    public int CustbizId { get; set; }

    public bool? Active { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime DateUpdated { get; set; }

    public virtual TbCustomerBizMap Custbiz { get; set; } = null!;
}
