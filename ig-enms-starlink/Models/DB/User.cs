using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public bool? Internal { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public DateTime CreatedOn { get; set; }

    public int CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public int? UpdatedBy { get; set; }

    public bool? Active { get; set; }
}
