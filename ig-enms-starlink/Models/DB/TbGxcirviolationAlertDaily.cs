using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbGxcirviolationAlertDaily
{
    public int Id { get; set; }

    public string? AlertSixty { get; set; }

    public string? AlertOne { get; set; }

    public string? AlertBoth { get; set; }

    public string? Time { get; set; }

    public DateTime DateCreated { get; set; }
}
