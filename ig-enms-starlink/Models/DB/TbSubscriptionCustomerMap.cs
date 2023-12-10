﻿using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbSubscriptionCustomerMap
{
    public int Id { get; set; }

    public int SubscriptionId { get; set; }

    public string CustomerId { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public DateTime DateCreated { get; set; }

    public DateTime? DateDeleted { get; set; }
}
