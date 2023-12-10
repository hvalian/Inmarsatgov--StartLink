using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxassuranceLoadStatus
{
    public int Id { get; set; }

    public DateTime ProcessStartTs { get; set; }

    public DateTime? ProcessEndTs { get; set; }

    public DateTime? LastImportedTs { get; set; }

    public DateTime DateAdded { get; set; }
}
