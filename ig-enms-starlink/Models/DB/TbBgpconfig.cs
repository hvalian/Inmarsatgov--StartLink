using System;
using System.Collections.Generic;

namespace IG.ENMS.Starlink.Models.DB;

public partial class TbBgpconfig
{
    public int Id { get; set; }

    public string? PeerType { get; set; }

    public string? ConnectRetry { get; set; }

    public string? Dropwarn { get; set; }

    public string? HoldTime { get; set; }

    public string? HopSelf { get; set; }

    public string? KeepAlive { get; set; }

    public string? MaxRouterPeer { get; set; }

    public string? Md5authPasswd { get; set; }

    public string? Passive { get; set; }

    public string? ReflectorClient { get; set; }

    public string? RemoteAddress { get; set; }

    public string? RemoteAs { get; set; }

    public string? RemotePort { get; set; }

    public string? SendRecv { get; set; }

    public string? EnabledOrf { get; set; }

    public string? OtaBgppeer { get; set; }

    public string? IpAddressFamily { get; set; }

    public string? RedistributeConnectedRoutes { get; set; }

    public string? RedistributeStaticRoutes { get; set; }

    public int TerminalsvnconfigId { get; set; }

    public DateTime DateAdded { get; set; }

    public DateTime DateUpdated { get; set; }

    public string? SddBgpconfigId { get; set; }

    public short? IsActive { get; set; }

    public virtual TbTerminalsvnConfig Terminalsvnconfig { get; set; } = null!;
}
