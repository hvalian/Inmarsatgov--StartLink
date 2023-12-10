using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Models.DB;

public partial class IgenmsContext : DbContext
{
    private readonly string _connectionString;

    public IgenmsContext()
    {
    }

    public IgenmsContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IgenmsContext(DbContextOptions<IgenmsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AssuranceMetricsMigrationLog> AssuranceMetricsMigrationLogs { get; set; }

    public virtual DbSet<Beams1> Beams1s { get; set; }

    public virtual DbSet<GxassuranceMetricsRunTimestamp> GxassuranceMetricsRunTimestamps { get; set; }

    public virtual DbSet<MuleprocessrunInitialrunStatus> MuleprocessrunInitialrunStatuses { get; set; }

    public virtual DbSet<TbAddress> TbAddresses { get; set; }

    public virtual DbSet<TbBgpconfig> TbBgpconfigs { get; set; }

    public virtual DbSet<TbBizService> TbBizServices { get; set; }

    public virtual DbSet<TbConnector> TbConnectors { get; set; }

    public virtual DbSet<TbCustomerBizMap> TbCustomerBizMaps { get; set; }

    public virtual DbSet<TbDhcpconfig> TbDhcpconfigs { get; set; }

    public virtual DbSet<TbDnsconfig> TbDnsconfigs { get; set; }

    public virtual DbSet<TbFisasvnSession> TbFisasvnSessions { get; set; }

    public virtual DbSet<TbGxBeamLookup> TbGxBeamLookups { get; set; }

    public virtual DbSet<TbGxLookup> TbGxLookups { get; set; }

    public virtual DbSet<TbGxSddlookup> TbGxSddlookups { get; set; }

    public virtual DbSet<TbGxassuranceLoadStatus> TbGxassuranceLoadStatuses { get; set; }

    public virtual DbSet<TbGxcirviolationAlertDaily> TbGxcirviolationAlertDailies { get; set; }

    public virtual DbSet<TbGxorderManagement> TbGxorderManagements { get; set; }

    public virtual DbSet<TbGxprovisionLog> TbGxprovisionLogs { get; set; }

    public virtual DbSet<TbLog> TbLogs { get; set; }

    public virtual DbSet<TbLookup> TbLookups { get; set; }

    public virtual DbSet<TbLookup1> TbLookups1 { get; set; }

    public virtual DbSet<TbModifyOrderDetail> TbModifyOrderDetails { get; set; }

    public virtual DbSet<TbNatpatconfig> TbNatpatconfigs { get; set; }

    public virtual DbSet<TbNode> TbNodes { get; set; }

    public virtual DbSet<TbNodeConfig> TbNodeConfigs { get; set; }

    public virtual DbSet<TbNodeConfigValue> TbNodeConfigValues { get; set; }

    public virtual DbSet<TbNodeDatum> TbNodeData { get; set; }

    public virtual DbSet<TbNodeMetric> TbNodeMetrics { get; set; }

    public virtual DbSet<TbNodeMetricValue> TbNodeMetricValues { get; set; }

    public virtual DbSet<TbNodeMetricValueLatest> TbNodeMetricValueLatests { get; set; }

    public virtual DbSet<TbNodeMetricValueLatestBySvn> TbNodeMetricValueLatestBySvns { get; set; }

    public virtual DbSet<TbNodeParent> TbNodeParents { get; set; }

    public virtual DbSet<TbNodeType> TbNodeTypes { get; set; }

    public virtual DbSet<TbNodeTypeConfig> TbNodeTypeConfigs { get; set; }

    public virtual DbSet<TbNodeTypeMetric> TbNodeTypeMetrics { get; set; }

    public virtual DbSet<TbNodeTypeTemplate> TbNodeTypeTemplates { get; set; }

    public virtual DbSet<TbNodeTypeTemplateMetric> TbNodeTypeTemplateMetrics { get; set; }

    public virtual DbSet<TbPlatform> TbPlatforms { get; set; }

    public virtual DbSet<TbPoller> TbPollers { get; set; }

    public virtual DbSet<TbPortSwitchConfig> TbPortSwitchConfigs { get; set; }

    public virtual DbSet<TbProcessLog> TbProcessLogs { get; set; }

    public virtual DbSet<TbProduct> TbProducts { get; set; }

    public virtual DbSet<TbProvisionStatus> TbProvisionStatuses { get; set; }

    public virtual DbSet<TbSddcle> TbSddcles { get; set; }

    public virtual DbSet<TbSddcontract> TbSddcontracts { get; set; }

    public virtual DbSet<TbSddproductOffering> TbSddproductOfferings { get; set; }

    public virtual DbSet<TbSddsite> TbSddsites { get; set; }

    public virtual DbSet<TbSddsiteFolder> TbSddsiteFolders { get; set; }

    public virtual DbSet<TbSddsubscriptionPlan> TbSddsubscriptionPlans { get; set; }

    public virtual DbSet<TbSddsvn> TbSddsvns { get; set; }

    public virtual DbSet<TbServiceLine> TbServiceLines { get; set; }

    public virtual DbSet<TbServiceLineSubscriptionMap> TbServiceLineSubscriptionMaps { get; set; }

    public virtual DbSet<TbServiceLineUsage> TbServiceLineUsages { get; set; }

    public virtual DbSet<TbSnterminal> TbSnterminals { get; set; }

    public virtual DbSet<TbSnterminalFailedRecordsDetail> TbSnterminalFailedRecordsDetails { get; set; }

    public virtual DbSet<TbSnterminalsPreviousDate> TbSnterminalsPreviousDates { get; set; }

    public virtual DbSet<TbStaticRouteConfig> TbStaticRouteConfigs { get; set; }

    public virtual DbSet<TbSubscriberServicePlan> TbSubscriberServicePlans { get; set; }

    public virtual DbSet<TbSubscriptionCustomerMap> TbSubscriptionCustomerMaps { get; set; }

    public virtual DbSet<TbSvnCustomerMap> TbSvnCustomerMaps { get; set; }

    public virtual DbSet<TbSvnUdr> TbSvnUdrs { get; set; }

    public virtual DbSet<TbSvnUdrNodeMetricValueMap> TbSvnUdrNodeMetricValueMaps { get; set; }

    public virtual DbSet<TbTerminalExternalCustomerMap> TbTerminalExternalCustomerMaps { get; set; }

    public virtual DbSet<TbTerminalFirmwareConfig> TbTerminalFirmwareConfigs { get; set; }

    public virtual DbSet<TbTerminalInfo> TbTerminalInfos { get; set; }

    public virtual DbSet<TbTerminalProvisionInfo> TbTerminalProvisionInfos { get; set; }

    public virtual DbSet<TbTerminalServiceLineMap> TbTerminalServiceLineMaps { get; set; }

    public virtual DbSet<TbTerminalServicePlan> TbTerminalServicePlans { get; set; }

    public virtual DbSet<TbTerminalSummary> TbTerminalSummaries { get; set; }

    public virtual DbSet<TbTerminalTypeConfig> TbTerminalTypeConfigs { get; set; }

    public virtual DbSet<TbTerminalUtilizationLookup> TbTerminalUtilizationLookups { get; set; }

    public virtual DbSet<TbTerminalsvnConfig> TbTerminalsvnConfigs { get; set; }

    public virtual DbSet<TbUsageBucket> TbUsageBuckets { get; set; }

    public virtual DbSet<TbVar> TbVars { get; set; }

    public virtual DbSet<TbVarCustomerBizMap> TbVarCustomerBizMaps { get; set; }

    public virtual DbSet<TbenmsGroup> TbenmsGroups { get; set; }

    public virtual DbSet<TbenmsGroupType> TbenmsGroupTypes { get; set; }

    public virtual DbSet<TbmodemInfo> TbmodemInfos { get; set; }

    public virtual DbSet<TbnodeExclusionList> TbnodeExclusionLists { get; set; }

    public virtual DbSet<Tbobject> Tbobjects { get; set; }

    public virtual DbSet<TbobjectConfig> TbobjectConfigs { get; set; }

    public virtual DbSet<TbobjectConfigValue> TbobjectConfigValues { get; set; }

    public virtual DbSet<TbobjectProcessConfig> TbobjectProcessConfigs { get; set; }

    public virtual DbSet<TbobjectProcessLog> TbobjectProcessLogs { get; set; }

    public virtual DbSet<TbobjectType> TbobjectTypes { get; set; }

    public virtual DbSet<TbobjectTypeConfig> TbobjectTypeConfigs { get; set; }

    public virtual DbSet<TbobjectTypeProcessConfig> TbobjectTypeProcessConfigs { get; set; }

    public virtual DbSet<TbterminalSvnSession> TbterminalSvnSessions { get; set; }

    public virtual DbSet<TbterminalenmsGroup> TbterminalenmsGroups { get; set; }

    public virtual DbSet<TmptbNodeMetricValueWithBackfill> TmptbNodeMetricValueWithBackfills { get; set; }

    public virtual DbSet<TmptbSvnCustomerMap> TmptbSvnCustomerMaps { get; set; }

    public virtual DbSet<ViewConfigValue> ViewConfigValues { get; set; }

    public virtual DbSet<ViewGxterminalDetail> ViewGxterminalDetails { get; set; }

    public virtual DbSet<ViewGxterminalSdddetail> ViewGxterminalSdddetails { get; set; }

    public virtual DbSet<ViewGxterminalStatus> ViewGxterminalStatuses { get; set; }

    public virtual DbSet<ViewGxterminalStatusBySvn> ViewGxterminalStatusBySvns { get; set; }

    public virtual DbSet<ViewNodeConfig> ViewNodeConfigs { get; set; }

    public virtual DbSet<ViewSpeedcastTerminalStatus> ViewSpeedcastTerminalStatuses { get; set; }

    public virtual DbSet<ViewTerminalSvnsession> ViewTerminalSvnsessions { get; set; }

    public virtual DbSet<VwActiveTerminalInfo> VwActiveTerminalInfos { get; set; }

    public virtual DbSet<VwGetAccountsByUserName> VwGetAccountsByUserNames { get; set; }

    public virtual DbSet<VwNode> VwNodes { get; set; }

    public virtual DbSet<VwNodeMetric> VwNodeMetrics { get; set; }

    public virtual DbSet<VwNodeStatus> VwNodeStatuses { get; set; }

    public virtual DbSet<VwNodeType> VwNodeTypes { get; set; }

    public virtual DbSet<VwNodeTypeMetric> VwNodeTypeMetrics { get; set; }

    public virtual DbSet<VwSubscription> VwSubscriptions { get; set; }

    public virtual DbSet<VwTerminal> VwTerminals { get; set; }

    public virtual DbSet<GetTelemetries_Result> GetTelemetries_Result { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AssuranceMetricsMigrationLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("assuranceMetricsMigrationLog", "GX");

            entity.Property(e => e.Dateadded)
                .HasColumnType("datetime")
                .HasColumnName("dateadded");
            entity.Property(e => e.Enddatetime)
                .HasColumnType("datetime")
                .HasColumnName("enddatetime");
            entity.Property(e => e.Log)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("log");
            entity.Property(e => e.Recordcount).HasColumnName("recordcount");
            entity.Property(e => e.Startdatetime)
                .HasColumnType("datetime")
                .HasColumnName("startdatetime");
        });

        modelBuilder.Entity<Beams1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("beams_1");

            entity.Property(e => e.Beam)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BeamCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BeamId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ObjId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Obj_id");
            entity.Property(e => e.Satellite)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GxassuranceMetricsRunTimestamp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GXAssuranceMetrics_RunTimestamps");

            entity.HasIndex(e => new { e.NodeId, e.ProcessName }, "UC_processName_NodeId").IsUnique();

            entity.Property(e => e.Currentrunstart)
                .HasMaxLength(50)
                .HasColumnName("currentrunstart");
            entity.Property(e => e.CurrentrunstartEpoch)
                .HasMaxLength(50)
                .HasColumnName("currentrunstartEpoch");
            entity.Property(e => e.LastRunTime)
                .HasMaxLength(50)
                .HasColumnName("lastRunTime");
            entity.Property(e => e.LastRunTimeEpoch)
                .HasMaxLength(50)
                .HasColumnName("lastRunTimeEpoch");
            entity.Property(e => e.NodeId)
                .HasMaxLength(50)
                .HasColumnName("nodeId");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .HasColumnName("processName");
            entity.Property(e => e.RunId)
                .HasMaxLength(50)
                .HasColumnName("RunID");
            entity.Property(e => e.Running)
                .HasMaxLength(50)
                .HasColumnName("running");
        });

        modelBuilder.Entity<MuleprocessrunInitialrunStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("muleprocessrun_initialrun_status");

            entity.Property(e => e.InitialendTime).HasColumnName("initialend_time");
            entity.Property(e => e.InitialstartTime).HasColumnName("initialstart_time");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Processname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("processname");
            entity.Property(e => e.ProgressrunTime).HasColumnName("progressrun_time");
            entity.Property(e => e.Running)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("running");
        });

        modelBuilder.Entity<TbAddress>(entity =>
        {
            entity.HasKey(e => e.AddressReferenceId).HasName("PK_tbPAddress");

            entity.ToTable("tbAddress", "starlink");

            entity.Property(e => e.AddressReferenceId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("addressReferenceId");
            entity.Property(e => e.AddressLines)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("addressLines");
            entity.Property(e => e.AdministrativeArea)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("administrativeArea");
            entity.Property(e => e.AdministrativeAreaCode)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("administrativeAreaCode");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.FormattedAddress)
                .HasMaxLength(500)
                .HasColumnName("formattedAddress");
            entity.Property(e => e.Latitude)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("latitude");
            entity.Property(e => e.Locality)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("locality");
            entity.Property(e => e.Longitude)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("longitude");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("postalCode");
            entity.Property(e => e.Region)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("region");
            entity.Property(e => e.RegionCode)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("regionCode");
        });

        modelBuilder.Entity<TbBgpconfig>(entity =>
        {
            entity.ToTable("tbBGPConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConnectRetry)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("connectRetry");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Dropwarn)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("dropwarn");
            entity.Property(e => e.EnabledOrf)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("enabledORF");
            entity.Property(e => e.HoldTime)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("holdTime");
            entity.Property(e => e.HopSelf)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("hopSelf");
            entity.Property(e => e.IpAddressFamily)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("ipAddressFamily");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.KeepAlive)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("keepAlive");
            entity.Property(e => e.MaxRouterPeer)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("maxRouterPeer");
            entity.Property(e => e.Md5authPasswd)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("md5authPasswd");
            entity.Property(e => e.OtaBgppeer)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("otaBGPPeer");
            entity.Property(e => e.Passive)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("passive");
            entity.Property(e => e.PeerType)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("peerType");
            entity.Property(e => e.RedistributeConnectedRoutes)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("redistributeConnectedRoutes");
            entity.Property(e => e.RedistributeStaticRoutes)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("redistributeStaticRoutes");
            entity.Property(e => e.ReflectorClient)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("reflectorClient");
            entity.Property(e => e.RemoteAddress)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("remoteAddress");
            entity.Property(e => e.RemoteAs)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("remoteAS");
            entity.Property(e => e.RemotePort)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("remotePort");
            entity.Property(e => e.SddBgpconfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddBGPConfigID");
            entity.Property(e => e.SendRecv)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("sendRecv");
            entity.Property(e => e.TerminalsvnconfigId).HasColumnName("terminalsvnconfigID");

            entity.HasOne(d => d.Terminalsvnconfig).WithMany(p => p.TbBgpconfigs)
                .HasForeignKey(d => d.TerminalsvnconfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbBGPConfig_tbTerminalsvnconfigID");
        });

        modelBuilder.Entity<TbBizService>(entity =>
        {
            entity.ToTable("tbBizService");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbConnector>(entity =>
        {
            entity.ToTable("tbConnector");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ConnectorCreds)
                .HasColumnType("xml")
                .HasColumnName("connectorCreds");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.LastPollDate)
                .HasColumnType("datetime")
                .HasColumnName("lastPollDate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NextPollDate)
                .HasColumnType("datetime")
                .HasColumnName("nextPollDate");
            entity.Property(e => e.PlatformId).HasColumnName("platformId");
            entity.Property(e => e.PlatformLocation)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("platformLocation");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.ProcessorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("processorName");

            entity.HasOne(d => d.Platform).WithMany(p => p.TbConnectors)
                .HasForeignKey(d => d.PlatformId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbConnector_tbPlatform");
        });

        modelBuilder.Entity<TbCustomerBizMap>(entity =>
        {
            entity.ToTable("tbCustomerBizMap");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.BizId).HasColumnName("bizId");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");

            entity.HasOne(d => d.Biz).WithMany(p => p.TbCustomerBizMaps)
                .HasForeignKey(d => d.BizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbCustomerBizMap_tbBizService");
        });

        modelBuilder.Entity<TbDhcpconfig>(entity =>
        {
            entity.ToTable("tbDHCPConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Broadcastipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("broadcastipaddress");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Defaultgatewayipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("defaultgatewayipaddress");
            entity.Property(e => e.Dhcpmode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dhcpmode");
            entity.Property(e => e.Iprangeend)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("iprangeend");
            entity.Property(e => e.Iprangestart)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("iprangestart");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Leaseduration).HasColumnName("leaseduration");
            entity.Property(e => e.Leasedurationunit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("leasedurationunit");
            entity.Property(e => e.Maxleaselengthinsec)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("maxleaselengthinsec");
            entity.Property(e => e.Primaryserveripadddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primaryserveripadddress");
            entity.Property(e => e.Relaysubnetipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relaysubnetipaddress");
            entity.Property(e => e.Relaysubnetmask)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relaysubnetmask");
            entity.Property(e => e.Relaytoipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("relaytoipaddress");
            entity.Property(e => e.SddDhcpconfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddDHCPConfigID");
            entity.Property(e => e.SddDhcpparentConfigId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sddDHCPParentConfigID");
            entity.Property(e => e.Secserveripaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("secserveripaddress");
            entity.Property(e => e.Serversubnetipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serversubnetipaddress");
            entity.Property(e => e.Serversubnetmask)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serversubnetmask");
            entity.Property(e => e.TerminalsvnconfigId).HasColumnName("terminalsvnconfigID");

            entity.HasOne(d => d.Terminalsvnconfig).WithMany(p => p.TbDhcpconfigs)
                .HasForeignKey(d => d.TerminalsvnconfigId)
                .HasConstraintName("FK_tbDHCPConfig_tbTerminalsvnconfigID");
        });

        modelBuilder.Entity<TbDnsconfig>(entity =>
        {
            entity.ToTable("tbDNSConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.PrimaryIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primaryIP");
            entity.Property(e => e.PrimaryServerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primaryServerName");
            entity.Property(e => e.SddDnsconfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddDNSConfigID");
            entity.Property(e => e.SecondaryIp)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("secondaryIP");
            entity.Property(e => e.SecondaryServerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("secondaryServerName");
            entity.Property(e => e.TerminalsvnconfigId).HasColumnName("terminalsvnconfigID");

            entity.HasOne(d => d.Terminalsvnconfig).WithMany(p => p.TbDnsconfigs)
                .HasForeignKey(d => d.TerminalsvnconfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbDNSConfig_tbTerminalsvnconfigID");
        });

        modelBuilder.Entity<TbFisasvnSession>(entity =>
        {
            entity.HasKey(e => e.SvnSessionId);

            entity.ToTable("tbFISASvnSession", "GX");

            entity.Property(e => e.SvnSessionId).HasColumnName("svnSessionId");
            entity.Property(e => e.AddedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("addedBy");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateProcessed)
                .HasColumnType("datetime")
                .HasColumnName("dateProcessed");
            entity.Property(e => e.Dateupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateupdated");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.SessionEnd)
                .HasColumnType("datetime")
                .HasColumnName("sessionEnd");
            entity.Property(e => e.SessionStart)
                .HasColumnType("datetime")
                .HasColumnName("sessionStart");
            entity.Property(e => e.SvnId).HasColumnName("svnId");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("updatedBy");

            entity.HasOne(d => d.Node).WithMany(p => p.TbFisasvnSessions)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbFISASvnSession_tbNode");

            entity.HasOne(d => d.Svn).WithMany(p => p.TbFisasvnSessions)
                .HasForeignKey(d => d.SvnId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbFISASvnSession_tbSVN");
        });

        modelBuilder.Entity<TbGxBeamLookup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbGxBeamLookup", "GX");

            entity.Property(e => e.ObjAttributesAzimuth)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_attributes_azimuth");
            entity.Property(e => e.ObjAttributesBeamIndex)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_attributes_beam_index");
            entity.Property(e => e.ObjAttributesElevation)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_attributes_elevation");
            entity.Property(e => e.ObjAttributesRadius)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_attributes_radius");
            entity.Property(e => e.ObjId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_id");
            entity.Property(e => e.ObjName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_name");
        });

        modelBuilder.Entity<TbGxLookup>(entity =>
        {
            entity.ToTable("tbGxLookup", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MetricDesc)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricDesc");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TbGxSddlookup>(entity =>
        {
            entity.ToTable("tbGxSDDLookup", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Isactive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isactive");
            entity.Property(e => e.LookupField)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("lookupField");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TbGxassuranceLoadStatus>(entity =>
        {
            entity.ToTable("tbGXAssuranceLoadStatus", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.LastImportedTs)
                .HasColumnType("datetime")
                .HasColumnName("lastImportedTS");
            entity.Property(e => e.ProcessEndTs)
                .HasColumnType("datetime")
                .HasColumnName("processEndTS");
            entity.Property(e => e.ProcessStartTs)
                .HasColumnType("datetime")
                .HasColumnName("processStartTS");
        });

        modelBuilder.Entity<TbGxcirviolationAlertDaily>(entity =>
        {
            entity.ToTable("tbGXCIRViolationAlertDaily", "usgbu");

            entity.HasIndex(e => e.Time, "UC_time").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AlertBoth)
                .HasMaxLength(50)
                .HasColumnName("alertBoth");
            entity.Property(e => e.AlertOne)
                .HasMaxLength(50)
                .HasColumnName("alertOne");
            entity.Property(e => e.AlertSixty)
                .HasMaxLength(50)
                .HasColumnName("alertSixty");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Time)
                .HasMaxLength(50)
                .HasColumnName("time");
        });

        modelBuilder.Entity<TbGxorderManagement>(entity =>
        {
            entity.ToTable("tbGXOrderManagement", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("actionType");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnmsRefId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("enmsRefID");
            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusID");
            entity.Property(e => e.RetryFlag).HasColumnName("retryFlag");
            entity.Property(e => e.SddOrderId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sddOrderID");
            entity.Property(e => e.TerminalProvisionId).HasColumnName("terminalProvisionID");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UserName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.TbGxorderManagements)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK_tbGXOrderManagement_tbProvisionStatus");

            entity.HasOne(d => d.TerminalProvision).WithMany(p => p.TbGxorderManagements)
                .HasForeignKey(d => d.TerminalProvisionId)
                .HasConstraintName("FK_tbGXOrderManagement_tbTerminalProvisionInfo");
        });

        modelBuilder.Entity<TbGxprovisionLog>(entity =>
        {
            entity.ToTable("tbGXProvisionLog", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("actionType");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnmsRefId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("enmsRefID");
            entity.Property(e => e.LogType)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("logType");
            entity.Property(e => e.ModifyOrderDetailId).HasColumnName("modifyOrderDetailId");
            entity.Property(e => e.OrderMgmtId).HasColumnName("orderMgmtId");
            entity.Property(e => e.Payload).HasColumnName("payload");
            entity.Property(e => e.SddApimodule)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("sddAPIModule");
            entity.Property(e => e.TerminalProvisionId).HasColumnName("terminalProvisionID");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");

            entity.HasOne(d => d.ModifyOrderDetail).WithMany(p => p.TbGxprovisionLogs)
                .HasForeignKey(d => d.ModifyOrderDetailId)
                .HasConstraintName("FK_tbGXProvisionLog_tbModifyOrderDetail");

            entity.HasOne(d => d.TerminalProvision).WithMany(p => p.TbGxprovisionLogs)
                .HasForeignKey(d => d.TerminalProvisionId)
                .HasConstraintName("FK_tbGXProvisionLog_tbTerminalProvisionInfo");
        });

        modelBuilder.Entity<TbLog>(entity =>
        {
            entity.ToTable("tbLog", "starlink");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AffectedRows).HasColumnName("affectedRows");
            entity.Property(e => e.Category)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.ElapsedTime).HasColumnName("elapsedTime");
            entity.Property(e => e.ExceptionMessage)
                .IsUnicode(false)
                .HasColumnName("exceptionMessage");
            entity.Property(e => e.InnerException)
                .IsUnicode(false)
                .HasColumnName("innerException");
            entity.Property(e => e.LogType)
                .HasMaxLength(32)
                .IsUnicode(false)
                .HasColumnName("logType");
            entity.Property(e => e.Message)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.MethodName)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("methodName");
            entity.Property(e => e.StackTrace)
                .IsUnicode(false)
                .HasColumnName("stackTrace");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
        });

        modelBuilder.Entity<TbLookup>(entity =>
        {
            entity.ToTable("tbLookup");

            entity.HasIndex(e => new { e.Name, e.ReferenceValue, e.Active }, "IX_tbLookup_unique_name_refValue").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ReferenceValue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenceValue");
            entity.Property(e => e.ReturnValue)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("returnValue");
        });

        modelBuilder.Entity<TbLookup1>(entity =>
        {
            entity.ToTable("tbLookup", "provision");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Key)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("key");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TbModifyOrderDetail>(entity =>
        {
            entity.ToTable("tbModifyOrderDetail", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.ModifyOrderTypeId).HasColumnName("modifyOrderTypeID");
            entity.Property(e => e.OrderMgmtId).HasColumnName("orderMgmtID");
            entity.Property(e => e.OrderStatusId).HasColumnName("orderStatusID");
            entity.Property(e => e.SddOrderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddOrderId");

            entity.HasOne(d => d.ModifyOrderType).WithMany(p => p.TbModifyOrderDetails)
                .HasForeignKey(d => d.ModifyOrderTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbModifyOrderDetail_tbGXLookup");

            entity.HasOne(d => d.OrderMgmt).WithMany(p => p.TbModifyOrderDetails)
                .HasForeignKey(d => d.OrderMgmtId)
                .HasConstraintName("FK_tbModifyOrderDetail_tbGXOrderMgmt");

            entity.HasOne(d => d.OrderStatus).WithMany(p => p.TbModifyOrderDetails)
                .HasForeignKey(d => d.OrderStatusId)
                .HasConstraintName("FK_tbModifyOrderDetail_tbProvisionStatus");
        });

        modelBuilder.Entity<TbNatpatconfig>(entity =>
        {
            entity.ToTable("tbNATPATConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.FirewallEnable)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("firewallEnable");
            entity.Property(e => e.FirewallPortRange)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("firewallPortRange");
            entity.Property(e => e.FirewallProtocol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("firewallProtocol");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.SddNatpatconfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddNATPATConfigID");
            entity.Property(e => e.SessionLocalAddress)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("sessionLocalAddress");
            entity.Property(e => e.SessionLocalPortRange)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sessionLocalPortRange");
            entity.Property(e => e.SessionNatportRange)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sessionNATPortRange");
            entity.Property(e => e.SessionProtocol)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("sessionProtocol");
            entity.Property(e => e.SipalgtableEnable)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SIPALGTableEnable");
            entity.Property(e => e.SipalgtablePort)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SIPALGTablePort");
            entity.Property(e => e.TerminalsvnconfigId).HasColumnName("terminalsvnconfigID");
            entity.Property(e => e.Timeout)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("timeout");

            entity.HasOne(d => d.Terminalsvnconfig).WithMany(p => p.TbNatpatconfigs)
                .HasForeignKey(d => d.TerminalsvnconfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNATPATConfig_tbTerminalsvnconfigID");
        });

        modelBuilder.Entity<TbNode>(entity =>
        {
            entity.ToTable("tbNode");

            entity.HasIndex(e => new { e.Name, e.NodeTypeId }, "UC_Node_NodeType").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ConnectorCreds)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("connectorCreds");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.DeactivationDate)
                .HasColumnType("datetime")
                .HasColumnName("deactivationDate");
            entity.Property(e => e.LastPollDate)
                .HasColumnType("datetime")
                .HasColumnName("lastPollDate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NextPollDate)
                .HasColumnType("datetime")
                .HasColumnName("nextPollDate");
            entity.Property(e => e.NodeData)
                .HasColumnType("xml")
                .HasColumnName("nodeData");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.NodeTypeTemplateId).HasColumnName("nodeTypeTemplateId");
            entity.Property(e => e.PollPolicy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pollPolicy");
            entity.Property(e => e.PollerId).HasColumnName("pollerId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.ProcessorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("processorName");
            entity.Property(e => e.SourceInfo)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("sourceInfo");
            entity.Property(e => e.UseMetricTemplate)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("useMetricTemplate");

            entity.HasOne(d => d.Connector).WithMany(p => p.TbNodes)
                .HasForeignKey(d => d.ConnectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNode_tbConnector");

            entity.HasOne(d => d.NodeType).WithMany(p => p.TbNodes)
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNode_tbNodeType");

            entity.HasOne(d => d.NodeTypeTemplate).WithMany(p => p.TbNodes)
                .HasForeignKey(d => d.NodeTypeTemplateId)
                .HasConstraintName("FK_tbNode_tbNodeTypeTemplate");

            entity.HasOne(d => d.Poller).WithMany(p => p.TbNodes)
                .HasForeignKey(d => d.PollerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNode_tbPoller");
        });

        modelBuilder.Entity<TbNodeConfig>(entity =>
        {
            entity.ToTable("tbNodeConfig");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.Node).WithMany(p => p.TbNodeConfigs)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeConfig_tbNode");

            entity.HasOne(d => d.NodeType).WithMany(p => p.TbNodeConfigs)
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeConfig_tbNodeType");
        });

        modelBuilder.Entity<TbNodeConfigValue>(entity =>
        {
            entity.ToTable("tbNodeConfigValue");

            entity.HasIndex(e => new { e.NodeId, e.ConfigKey, e.Timestamp }, "idxNodeConfigValue_nodeID-ConfigKey-Timestamp");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("addedBy");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeConfigId).HasColumnName("nodeConfigId");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.NodeConfig).WithMany(p => p.TbNodeConfigValues)
                .HasForeignKey(d => d.NodeConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeConfigValue_tbNodeConfig");

            entity.HasOne(d => d.Node).WithMany(p => p.TbNodeConfigValues)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeConfigValue_tbNode");

            entity.HasOne(d => d.NodeType).WithMany(p => p.TbNodeConfigValues)
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeConfigValue_tbNodeType");
        });

        modelBuilder.Entity<TbNodeDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("tbNodeData");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.CollectStats)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("collectStats");
            entity.Property(e => e.Connectorid).HasColumnName("connectorid");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nodetypeid).HasColumnName("nodetypeid");
        });

        modelBuilder.Entity<TbNodeMetric>(entity =>
        {
            entity.ToTable("tbNodeMetric");

            entity.HasIndex(e => new { e.NodeId, e.NodeTypeId, e.MetricKey }, "UC_Node_NodeType_MetricKey").IsUnique();

            entity.HasIndex(e => new { e.NodeId, e.MetricKey, e.NodeTypeId }, "UQ_NodeID_MetricKey_NodeTypeID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricSourceInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricSourceInfo");
            entity.Property(e => e.MetricUnits)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricUnits");
            entity.Property(e => e.MetricValue1Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue1Label");
            entity.Property(e => e.MetricValue2Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue2Label");
            entity.Property(e => e.MetricValue3Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue3Label");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeMetricData)
                .HasColumnType("xml")
                .HasColumnName("nodeMetricData");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");

            entity.HasOne(d => d.Node).WithMany(p => p.TbNodeMetrics)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeMetric_tbNode");
        });

        modelBuilder.Entity<TbNodeMetricValue>(entity =>
        {
            entity.HasKey(e => new { e.NodeId, e.MetricKey, e.Timestamp });

            entity.ToTable("tbNodeMetricValue");

            entity.HasIndex(e => new { e.NodeId, e.MetricKey, e.NodeTypeId, e.Timestamp }, "IX_tbNodeMetricValue_nodeID_metricKey_nodetypeId_timestamp_value");

            entity.HasIndex(e => new { e.NodeId, e.MetricKey, e.NodeTypeId, e.Timestamp }, "IX_tbNodeMetricValue_nodeId_metrickey_nodetypeId_timestamp");

            entity.HasIndex(e => new { e.NodeId, e.Timestamp }, "IX_tbNodeMetricValue_nodeId_timestamp");

            entity.HasIndex(e => new { e.NodeTypeId, e.MetricKey }, "IX_tbNodeMetricValue_nodeTypeId_metricKey");

            entity.HasIndex(e => new { e.NodeMetricId, e.Id }, "IX_tbNodeMetricValue_nodemetricid_id");

            entity.HasIndex(e => e.Timestamp, "IX_tbNodeMetricValue_timestamp");

            entity.HasIndex(e => new { e.MetricKey, e.NodeId, e.Timestamp, e.Id, e.NodeMetricId }, "UX_tbNodeMetricValue_metrickey_nodeid_timestamp_id_nodemetricid");

            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.BackFillMetricValueId).HasColumnName("backFillMetricValueID");
            entity.Property(e => e.EpochTimestamp).HasColumnName("epochTimestamp");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsbackFilled).HasColumnName("isbackFilled");
            entity.Property(e => e.LastPolledTimestamp).HasColumnName("lastPolledTimestamp");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Metricvalue1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue1");
            entity.Property(e => e.Metricvalue2)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue2");
            entity.Property(e => e.Metricvalue3)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue3");
            entity.Property(e => e.NodeMetricId).HasColumnName("nodeMetricId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");

            entity.HasOne(d => d.Node).WithMany(p => p.TbNodeMetricValues)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeMetricValue_tbNode");

            entity.HasOne(d => d.NodeMetric).WithMany(p => p.TbNodeMetricValues)
                .HasForeignKey(d => d.NodeMetricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeMetricValue_tbNodeMetric");
        });

        modelBuilder.Entity<TbNodeMetricValueLatest>(entity =>
        {
            entity.ToTable("tbNodeMetricValueLatest");

            entity.HasIndex(e => new { e.NodeId, e.NodeTypeId, e.MetricKey, e.Timestamp }, "UC1_Node_NodeType_MetricKey_Timestamp").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Epochtimestamp).HasColumnName("epochtimestamp");
            entity.Property(e => e.LastpolledTimestamp).HasColumnName("lastpolledTimestamp");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Metricvalue1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue1");
            entity.Property(e => e.Metricvalue2)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue2");
            entity.Property(e => e.Metricvalue3)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue3");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeMetricId).HasColumnName("nodeMetricId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TbNodeMetricValueLatestBySvn>(entity =>
        {
            entity.ToTable("tbNodeMetricValueLatestBySVN");

            entity.HasIndex(e => new { e.NodeId, e.NodeTypeId, e.MetricKey, e.Timestamp, e.Svn }, "UC2_Node_NodeType_MetricKey_Timestamp").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Metricvalue1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue1");
            entity.Property(e => e.Metricvalue2)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue2");
            entity.Property(e => e.Metricvalue3)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue3");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeMetricId).HasColumnName("nodeMetricId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.Svn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("SVN");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<TbNodeParent>(entity =>
        {
            entity.ToTable("tbNodeParent");

            entity.HasIndex(e => new { e.NodeId, e.ParentnodeId }, "UC_Node_ParentNode").IsUnique();

            entity.HasIndex(e => new { e.NodeId, e.ParentnodeId }, "UQ_NodeID_parentNodeID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.ParentnodeId).HasColumnName("parentnodeId");

            entity.HasOne(d => d.Node).WithMany(p => p.TbNodeParentNodes)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeParent_tbNode");

            entity.HasOne(d => d.Parentnode).WithMany(p => p.TbNodeParentParentnodes)
                .HasForeignKey(d => d.ParentnodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_bNodeParent_tbNode");
        });

        modelBuilder.Entity<TbNodeType>(entity =>
        {
            entity.ToTable("tbNodeType");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.Config)
                .HasColumnType("xml")
                .HasColumnName("config");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbNodeTypeConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbNodeTypeConfig");

            entity.Property(e => e.Config)
                .HasColumnType("xml")
                .HasColumnName("config");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.NodeType).WithMany()
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeTypeConfig_tbNodeType");
        });

        modelBuilder.Entity<TbNodeTypeMetric>(entity =>
        {
            entity.ToTable("tbNodeTypeMetric");

            entity.HasIndex(e => new { e.NodeTypeId, e.MetricKey }, "UC_NodeType_MetricKey").IsUnique();

            entity.HasIndex(e => new { e.MetricKey, e.NodeTypeId }, "UQ_MetricKey_NodeTypeID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Config)
                .HasColumnType("xml")
                .HasColumnName("config");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.IsCalculatedMetric)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isCalculatedMetric");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricSourceInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricSourceInfo");
            entity.Property(e => e.MetricUnits)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricUnits");
            entity.Property(e => e.MetricValue1Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue1Label");
            entity.Property(e => e.MetricValue2Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue2Label");
            entity.Property(e => e.MetricValue3Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue3Label");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.PreFillValueConfig)
                .HasColumnType("xml")
                .HasColumnName("preFillValueConfig");
            entity.Property(e => e.RollUpAggregateConfig)
                .HasColumnType("xml")
                .HasColumnName("rollUpAggregateConfig");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");

            entity.HasOne(d => d.NodeType).WithMany(p => p.TbNodeTypeMetrics)
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeTypeMetric_tbNodeType");
        });

        modelBuilder.Entity<TbNodeTypeTemplate>(entity =>
        {
            entity.ToTable("tbNodeTypeTemplate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");

            entity.HasOne(d => d.NodeType).WithMany(p => p.TbNodeTypeTemplates)
                .HasForeignKey(d => d.NodeTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeTypeTemplate_tbNodeType");
        });

        modelBuilder.Entity<TbNodeTypeTemplateMetric>(entity =>
        {
            entity.ToTable("tbNodeTypeTemplateMetric");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.NodeTypeMetricId).HasColumnName("nodeTypeMetricId");
            entity.Property(e => e.NodeTypeTemplateId).HasColumnName("nodeTypeTemplateId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");

            entity.HasOne(d => d.NodeTypeMetric).WithMany(p => p.TbNodeTypeTemplateMetrics)
                .HasForeignKey(d => d.NodeTypeMetricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeTypeTemplateMetric_tbNodeTypeMetric");

            entity.HasOne(d => d.NodeTypeTemplate).WithMany(p => p.TbNodeTypeTemplateMetrics)
                .HasForeignKey(d => d.NodeTypeTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbNodeTypeTemplateMetric_tbNodeTypeTemplate");
        });

        modelBuilder.Entity<TbPlatform>(entity =>
        {
            entity.ToTable("tbPlatform");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbPoller>(entity =>
        {
            entity.ToTable("tbPoller");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbPortSwitchConfig>(entity =>
        {
            entity.ToTable("tbPortSwitchConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AutoNegotiation)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("autoNegotiation");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnableTrunk)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("enableTrunk");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Mode)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("mode");
            entity.Property(e => e.PortMode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("portMode");
            entity.Property(e => e.PortSwitchNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("portSwitchNumber");
            entity.Property(e => e.SddPortConfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddPortConfigID");
            entity.Property(e => e.Speed)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("speed");
            entity.Property(e => e.Svnlist)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SVNList");
            entity.Property(e => e.Tagenable)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("tagenable");
            entity.Property(e => e.TerminalProvisionInfoId).HasColumnName("terminalProvisionInfoID");
            entity.Property(e => e.TrunkSvnlist)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("trunkSVNList");

            entity.HasOne(d => d.TerminalProvisionInfo).WithMany(p => p.TbPortSwitchConfigs)
                .HasForeignKey(d => d.TerminalProvisionInfoId)
                .HasConstraintName("FK_tbPortSwitchConfig_tbTerminalProvisionInfo");
        });

        modelBuilder.Entity<TbProcessLog>(entity =>
        {
            entity.HasKey(e => e.ProcessId).HasName("PK_tbprocessLog");

            entity.ToTable("tbProcessLog");

            entity.Property(e => e.ProcessId).HasColumnName("processId");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.ErrorType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("errorType");
            entity.Property(e => e.LogDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("logDate");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("processName");
            entity.Property(e => e.Request)
                .IsUnicode(false)
                .HasColumnName("request");
            entity.Property(e => e.RequestDate)
                .HasColumnType("datetime")
                .HasColumnName("requestDate");
            entity.Property(e => e.Response)
                .IsUnicode(false)
                .HasColumnName("response");
            entity.Property(e => e.ResponseCode)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("responseCode");
        });

        modelBuilder.Entity<TbProduct>(entity =>
        {
            entity.HasKey(e => e.ProductReferenceId);

            entity.ToTable("tbProduct", "starlink");

            entity.Property(e => e.ProductReferenceId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("productReferenceId");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PriceCurrency)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("priceCurrency");
        });

        modelBuilder.Entity<TbProvisionStatus>(entity =>
        {
            entity.HasKey(e => e.Statusid);

            entity.ToTable("tbProvisionStatus", "GX");

            entity.Property(e => e.Statusid).HasColumnName("statusid");
            entity.Property(e => e.Sddstatusdesc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddstatusdesc");
            entity.Property(e => e.StatusDesc)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("statusDesc");
        });

        modelBuilder.Entity<TbSddcle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbCLE");

            entity.ToTable("tbSDDCLE", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BillingAccountId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("billingAccountId");
            entity.Property(e => e.Cle).HasColumnName("CLE");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.L1folderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("L1FolderID");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ResourcePoolOwnerId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("resourcePoolOwnerID");
            entity.Property(e => e.Varid).HasColumnName("VARID");

            entity.HasOne(d => d.Var).WithMany(p => p.TbSddcles)
                .HasPrincipalKey(p => p.VarId)
                .HasForeignKey(d => d.Varid)
                .HasConstraintName("FK_tbCLE_tbvar");
        });

        modelBuilder.Entity<TbSddcontract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbcontract");

            entity.ToTable("tbSDDContract", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cleid).HasColumnName("CLEID");
            entity.Property(e => e.ContractId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contractID");
            entity.Property(e => e.ContractName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contractName");
            entity.Property(e => e.ContractType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contractType");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");

            entity.HasOne(d => d.Cle).WithMany(p => p.TbSddcontracts)
                .HasForeignKey(d => d.Cleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbContract_tbCLE");
        });

        modelBuilder.Entity<TbSddproductOffering>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbproductOffering");

            entity.ToTable("tbSDDProductOffering", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contractid).HasColumnName("contractid");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PdtOfferingId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pdtOfferingID");

            entity.HasOne(d => d.Contract).WithMany(p => p.TbSddproductOfferings)
                .HasForeignKey(d => d.Contractid)
                .HasConstraintName("FK_tbproductOffering_tbcontract");
        });

        modelBuilder.Entity<TbSddsite>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbsite");

            entity.ToTable("tbSDDSite", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Class)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("class");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.InstallationCountry)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("installationCountry");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.SiteFolderId).HasColumnName("siteFolderID");
            entity.Property(e => e.SiteId).HasColumnName("siteID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TailNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tailNumber");

            entity.HasOne(d => d.SiteFolder).WithMany(p => p.TbSddsites)
                .HasForeignKey(d => d.SiteFolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbsite_tbsiteFolder");
        });

        modelBuilder.Entity<TbSddsiteFolder>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbsiteFolder");

            entity.ToTable("tbSDDSiteFolder", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cleid).HasColumnName("CLEID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.L2folderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("L2FolderID");
            entity.Property(e => e.L2folderName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("L2FolderName");

            entity.HasOne(d => d.Cle).WithMany(p => p.TbSddsiteFolders)
                .HasForeignKey(d => d.Cleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbsiteFolder_tbCLE");
        });

        modelBuilder.Entity<TbSddsubscriptionPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbsubscriptionPlan");

            entity.ToTable("tbSDDSubscriptionPlan", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Characteristicname)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("characteristicname");
            entity.Property(e => e.Characteristicvalue)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("characteristicvalue");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.ProductOfferingId).HasColumnName("productOfferingID");
            entity.Property(e => e.ProductSpecificationId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("productSpecificationID");
            entity.Property(e => e.ProductSpecificationName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("productSpecificationName");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionID");
            entity.Property(e => e.SubscriptionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("subscriptionName");

            entity.HasOne(d => d.ProductOffering).WithMany(p => p.TbSddsubscriptionPlans)
                .HasForeignKey(d => d.ProductOfferingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbsubscriptionPlan_tbproductOffering");
        });

        modelBuilder.Entity<TbSddsvn>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbsvn");

            entity.ToTable("tbSDDSvn", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cleid).HasColumnName("CLEID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Svn)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("svn");
            entity.Property(e => e.Svnname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SVNName");

            entity.HasOne(d => d.Cle).WithMany(p => p.TbSddsvns)
                .HasForeignKey(d => d.Cleid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbsvn_tbcle");
        });

        modelBuilder.Entity<TbServiceLine>(entity =>
        {
            entity.HasKey(e => e.ServiceLineNumber);

            entity.ToTable("tbServiceLine", "starlink");

            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.AddressReferenceId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("addressReferenceId");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsTerminalRecord).HasColumnName("isTerminalRecord");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.AddressReference).WithMany(p => p.TbServiceLines)
                .HasForeignKey(d => d.AddressReferenceId)
                .HasConstraintName("FK_tbServiceLine_tbAddress");
        });

        modelBuilder.Entity<TbServiceLineSubscriptionMap>(entity =>
        {
            entity.HasKey(e => e.SlsubscriptionMapId);

            entity.ToTable("tbServiceLineSubscriptionMap", "starlink");

            entity.Property(e => e.SlsubscriptionMapId).HasColumnName("SLSubscriptionMapID");
            entity.Property(e => e.ActiveFrom)
                .HasColumnType("datetime")
                .HasColumnName("activeFrom");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsOptedIntoOverage).HasColumnName("isOptedIntoOverage");
            entity.Property(e => e.OverageAmountGb).HasColumnName("overageAmountGB");
            entity.Property(e => e.OverageDescription)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("overageDescription");
            entity.Property(e => e.OverageName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("overageName");
            entity.Property(e => e.OveragePrice).HasColumnName("overagePrice");
            entity.Property(e => e.PricePerGb).HasColumnName("pricePerGB");
            entity.Property(e => e.ProductRefId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("productRefId");
            entity.Property(e => e.ServiceEnd)
                .HasColumnType("datetime")
                .HasColumnName("serviceEnd");
            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");
            entity.Property(e => e.ServiceStart)
                .HasColumnType("datetime")
                .HasColumnName("serviceStart");
            entity.Property(e => e.SubscriptionRefId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("subscriptionRefId");
            entity.Property(e => e.UsageLimitGb).HasColumnName("usageLimitGB");

            entity.HasOne(d => d.ProductRef).WithMany(p => p.TbServiceLineSubscriptionMaps)
                .HasForeignKey(d => d.ProductRefId)
                .HasConstraintName("FK_tbServiceLineSubMap_tbProduct");

            entity.HasOne(d => d.ServiceLineNumberNavigation).WithMany(p => p.TbServiceLineSubscriptionMaps)
                .HasForeignKey(d => d.ServiceLineNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbServiceLineSubMap_tbServiceLine");
        });

        modelBuilder.Entity<TbServiceLineUsage>(entity =>
        {
            entity.HasKey(e => new { e.ServiceLineNumber, e.UsageTs, e.UsageBucketId, e.RecordType });

            entity.ToTable("tbServiceLineUsage", "starlink");

            entity.HasIndex(e => new { e.ServiceLineNumber, e.UsageTs, e.RecordType, e.UsageBucketId }, "UC_tbServiceLineUsage").IsUnique();

            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.UsageTs)
                .HasColumnType("datetime")
                .HasColumnName("usageTS");
            entity.Property(e => e.UsageBucketId).HasColumnName("usageBucketId");
            entity.Property(e => e.RecordType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("recordType");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.TotalGb).HasColumnName("totalGB");

            entity.HasOne(d => d.ServiceLineNumberNavigation).WithMany(p => p.TbServiceLineUsages)
                .HasForeignKey(d => d.ServiceLineNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbServieLineUsage_tbServiceLine");

            entity.HasOne(d => d.UsageBucket).WithMany(p => p.TbServiceLineUsages)
                .HasForeignKey(d => d.UsageBucketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbServieLineUsage_tbUsageBucket");
        });

        modelBuilder.Entity<TbSnterminal>(entity =>
        {
            entity.HasKey(e => e.SysId);

            entity.ToTable("tbSNTerminals", "GX");

            entity.Property(e => e.SysId)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("sysId");
            entity.Property(e => e.Company)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("company");
            entity.Property(e => e.CompanyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("companyID");
            entity.Property(e => e.CoreModuleId)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("coreModuleID");
            entity.Property(e => e.DateImported)
                .HasColumnType("datetime")
                .HasColumnName("dateImported");
            entity.Property(e => e.GetStats).HasColumnName("getStats");
            entity.Property(e => e.GxTerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gxTerminalID");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("manufacturer");
            entity.Property(e => e.ProvisionedDate)
                .HasColumnType("datetime")
                .HasColumnName("provisionedDate");
            entity.Property(e => e.TerminalName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("terminalName");
            entity.Property(e => e.TerminalRmp)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("terminalRMP");
            entity.Property(e => e.TerminalStatus).HasColumnName("terminalStatus");
            entity.Property(e => e.TerminalType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("terminalType");
            entity.Property(e => e.Tpk)
                .HasMaxLength(80)
                .IsUnicode(false)
                .HasColumnName("tpk");
        });

        modelBuilder.Entity<TbSnterminalFailedRecordsDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbSNTerminalFailedRecordsDetails", "GX");

            entity.Property(e => e.CreatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("createdDateTime");
            entity.Property(e => e.FailedReason)
                .HasMaxLength(5000)
                .IsUnicode(false)
                .HasColumnName("failed_reason");
            entity.Property(e => e.Id)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("id");
            entity.Property(e => e.Operation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("operation");
            entity.Property(e => e.ReferenceKey)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("referenceKey");
            entity.Property(e => e.UpdatedDateTime)
                .HasColumnType("datetime")
                .HasColumnName("updatedDateTime");
        });

        modelBuilder.Entity<TbSnterminalsPreviousDate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbSNTerminalsPreviousDate", "GX");

            entity.Property(e => e.Active)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("active");
            entity.Property(e => e.Operation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("operation");
            entity.Property(e => e.PreviousDateTime)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("previousDateTime");
        });

        modelBuilder.Entity<TbStaticRouteConfig>(entity =>
        {
            entity.ToTable("tbStaticRouteConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Interface)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("interface");
            entity.Property(e => e.Ipv4Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ipv4Address");
            entity.Property(e => e.Ipv4Gateway)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ipv4Gateway");
            entity.Property(e => e.Ipv4Netmask)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ipv4Netmask");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.SddStaticRouteConfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddStaticRouteConfigID");
            entity.Property(e => e.TerminalsvnconfigId).HasColumnName("terminalsvnconfigID");

            entity.HasOne(d => d.Terminalsvnconfig).WithMany(p => p.TbStaticRouteConfigs)
                .HasForeignKey(d => d.TerminalsvnconfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbStaticRouteConfig_tbTerminalsvnconfigID");
        });

        modelBuilder.Entity<TbSubscriberServicePlan>(entity =>
        {
            entity.ToTable("tbSubscriberServicePlan", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateImported)
                .HasColumnType("datetime")
                .HasColumnName("dateImported");
            entity.Property(e => e.SspObjId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sspObjID");
            entity.Property(e => e.SspObjName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sspObjName");
            entity.Property(e => e.SspnodeId).HasColumnName("SSPNodeID");
            entity.Property(e => e.TspId).HasColumnName("tspId");

            entity.HasOne(d => d.Sspnode).WithMany(p => p.TbSubscriberServicePlans)
                .HasForeignKey(d => d.SspnodeId)
                .HasConstraintName("FK_tbSubscriberServicePlan_tbTerminalServicePlan");
        });

        modelBuilder.Entity<TbSubscriptionCustomerMap>(entity =>
        {
            entity.ToTable("tbSubscriptionCustomerMap", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("datetime")
                .HasColumnName("dateDeleted");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionId");
        });

        modelBuilder.Entity<TbSvnCustomerMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK1_tbSvnCustomerMap");

            entity.ToTable("tbSvnCustomerMap", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("datetime")
                .HasColumnName("dateDeleted");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Svn).HasColumnName("svn");
        });

        modelBuilder.Entity<TbSvnUdr>(entity =>
        {
            entity.ToTable("tbSvnUdr", "usgbu");

            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .HasColumnName("id");
            entity.Property(e => e.BeamId).HasColumnName("beamId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateProcessed)
                .HasColumnType("datetime")
                .HasColumnName("dateProcessed");
            entity.Property(e => e.Duplicate)
                .HasMaxLength(50)
                .HasColumnName("duplicate");
            entity.Property(e => e.EndTime)
                .HasColumnType("datetime")
                .HasColumnName("endTime");
            entity.Property(e => e.FramedIp)
                .HasMaxLength(15)
                .HasColumnName("framedIP");
            entity.Property(e => e.Latitude)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("latitude");
            entity.Property(e => e.Location)
                .HasMaxLength(50)
                .HasColumnName("location");
            entity.Property(e => e.Longitude)
                .HasColumnType("numeric(9, 3)")
                .HasColumnName("longitude");
            entity.Property(e => e.OriginHost)
                .HasMaxLength(50)
                .HasColumnName("originHost");
            entity.Property(e => e.OriginRealm)
                .HasMaxLength(50)
                .HasColumnName("originRealm");
            entity.Property(e => e.RecordNumber).HasColumnName("recordNumber");
            entity.Property(e => e.RecordType)
                .HasMaxLength(50)
                .HasColumnName("recordType");
            entity.Property(e => e.RxBytes).HasColumnName("rxBytes");
            entity.Property(e => e.Sac)
                .HasMaxLength(50)
                .HasColumnName("sac");
            entity.Property(e => e.SatelliteId)
                .HasMaxLength(50)
                .HasColumnName("satelliteId");
            entity.Property(e => e.ServiceId).HasColumnName("serviceId");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(50)
                .HasColumnName("serviceName");
            entity.Property(e => e.SessionId)
                .HasMaxLength(50)
                .HasColumnName("sessionId");
            entity.Property(e => e.StartTime)
                .HasColumnType("datetime")
                .HasColumnName("startTime");
            entity.Property(e => e.Subscriber)
                .HasMaxLength(50)
                .HasColumnName("subscriber");
            entity.Property(e => e.Svn).HasColumnName("svn");
            entity.Property(e => e.Termination).HasColumnName("termination");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.TotalBytes).HasColumnName("totalBytes");
            entity.Property(e => e.TotalTime).HasColumnName("totalTime");
            entity.Property(e => e.TxBytes).HasColumnName("txBytes");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
        });

        modelBuilder.Entity<TbSvnUdrNodeMetricValueMap>(entity =>
        {
            entity.ToTable("tbSvnUdrNodeMetricValueMap", "usgbu");

            entity.HasIndex(e => e.NodeMetricValueId, "idx_tbSvnUdrNodeMetricValueMap_nmvid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.NodeMetricValueId).HasColumnName("nodeMetricValueId");
            entity.Property(e => e.Svn).HasColumnName("svn");
            entity.Property(e => e.SvnUdrId)
                .HasMaxLength(50)
                .HasColumnName("svnUdrId");
        });

        modelBuilder.Entity<TbTerminalExternalCustomerMap>(entity =>
        {
            entity.ToTable("tbTerminalExternalCustomerMap", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateImported)
                .HasColumnType("datetime")
                .HasColumnName("dateImported");
            entity.Property(e => e.ExternalCustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("externalCustomerId");
            entity.Property(e => e.ExternalCustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("externalCustomerName");
            entity.Property(e => e.ExternalCustomerType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("externalCustomerType");
            entity.Property(e => e.NodeId).HasColumnName("nodeID");

            entity.HasOne(d => d.Node).WithMany(p => p.TbTerminalExternalCustomerMaps)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalExternalCustomerMap_tbNode");
        });

        modelBuilder.Entity<TbTerminalFirmwareConfig>(entity =>
        {
            entity.ToTable("tbTerminalFirmwareConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActiveFirmware)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("activeFirmware");
            entity.Property(e => e.BackupFirmware)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("backupFirmware");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.ModifyOrderType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("modifyOrderType");
            entity.Property(e => e.TerminalTypeId).HasColumnName("terminalTypeId");

            entity.HasOne(d => d.TerminalType).WithMany(p => p.TbTerminalFirmwareConfigs)
                .HasForeignKey(d => d.TerminalTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalFirmwareConfig_tbTerminalTypeConfig");
        });

        modelBuilder.Entity<TbTerminalInfo>(entity =>
        {
            entity.ToTable("tbTerminalInfo", "speedcast");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.NetworkName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("networkName");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.TerminalDid)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("terminalDID");
            entity.Property(e => e.TerminalId)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("terminalId");
            entity.Property(e => e.TerminalName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terminalName");
            entity.Property(e => e.TerminalSerialNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasDefaultValueSql("('')")
                .HasColumnName("terminalSerialNumber");
        });

        modelBuilder.Entity<TbTerminalProvisionInfo>(entity =>
        {
            entity.ToTable("tbTerminalProvisionInfo", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.AddedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("addedBy");
            entity.Property(e => e.BillingAccountId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("billingAccountId");
            entity.Property(e => e.CleId).HasColumnName("cleId");
            entity.Property(e => e.CustomTerminalName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("customTerminalName");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnmsRefId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("enmsRefID");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.MeetmePoint)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("meetmePoint");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PingConfig)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("pingConfig");
            entity.Property(e => e.SiteFolderId).HasColumnName("siteFolderID");
            entity.Property(e => e.SiteId).HasColumnName("siteID");
            entity.Property(e => e.StatsMgmtProfile)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("statsMgmtProfile");
            entity.Property(e => e.SubscriptionId).HasColumnName("subscriptionID");
            entity.Property(e => e.TerminaltypeConfigId).HasColumnName("terminaltypeConfigID");
            entity.Property(e => e.Tpk)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("tpk");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("updatedBy");
            entity.Property(e => e.UtilizationTypeId).HasColumnName("utilizationTypeId");

            entity.HasOne(d => d.Cle).WithMany(p => p.TbTerminalProvisionInfos)
                .HasForeignKey(d => d.CleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalProvisionInfo_CLEID");

            entity.HasOne(d => d.SiteFolder).WithMany(p => p.TbTerminalProvisionInfos)
                .HasForeignKey(d => d.SiteFolderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalProvisionInfo_siteFolderID");

            entity.HasOne(d => d.Site).WithMany(p => p.TbTerminalProvisionInfos)
                .HasForeignKey(d => d.SiteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbterminalProvisionInfo_tbSite");

            entity.HasOne(d => d.TerminaltypeConfig).WithMany(p => p.TbTerminalProvisionInfos)
                .HasForeignKey(d => d.TerminaltypeConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalProvisionInfo_terminaltypeConfigID");
        });

        modelBuilder.Entity<TbTerminalServiceLineMap>(entity =>
        {
            entity.HasKey(e => e.TerminalServiceLineMapId);

            entity.ToTable("tbTerminalServiceLineMap", "starlink");

            entity.Property(e => e.TerminalServiceLineMapId).HasColumnName("TerminalServiceLineMapID");
            entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.DeactivationDate)
                .HasColumnType("datetime")
                .HasColumnName("deactivationDate");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");

            entity.HasOne(d => d.Node).WithMany(p => p.TbTerminalServiceLineMaps)
                .HasForeignKey(d => d.NodeId)
                .HasConstraintName("FK_tbTerminalSLMap_tbNode");

            entity.HasOne(d => d.ServiceLineNumberNavigation).WithMany(p => p.TbTerminalServiceLineMaps)
                .HasForeignKey(d => d.ServiceLineNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalSLMap_tbServiceLine");
        });

        modelBuilder.Entity<TbTerminalServicePlan>(entity =>
        {
            entity.ToTable("tbTerminalServicePlan", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateImported)
                .HasColumnType("datetime")
                .HasColumnName("dateImported");
            entity.Property(e => e.NodeId).HasColumnName("nodeID");
            entity.Property(e => e.TspObjId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tspObjID");
            entity.Property(e => e.TspObjName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tspObjName");

            entity.HasOne(d => d.Node).WithMany(p => p.TbTerminalServicePlans)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalServicePlan_tbNode");
        });

        modelBuilder.Entity<TbTerminalSummary>(entity =>
        {
            entity.ToTable("tbTerminalSummary", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.ActiveFirmware)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("activeFirmware");
            entity.Property(e => e.BackupFirmware)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("backupFirmware");
            entity.Property(e => e.CoreModuleId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("coreModuleID");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnmsRefId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("enmsRefID");
            entity.Property(e => e.GxTerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gxTerminalID");
            entity.Property(e => e.NodeId).HasColumnName("nodeID");
            entity.Property(e => e.SddGxconfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddGXConfigID");
            entity.Property(e => e.SddGxid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sddGXID");
            entity.Property(e => e.SddGxsatelliteTerminalId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sddGXSatelliteTerminalID");
            entity.Property(e => e.SddGxtpsid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("sddGXTPSId");
            entity.Property(e => e.SddInstanceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddInstanceID");
            entity.Property(e => e.SddcustomerProvidedTerminalId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SDDCustomerProvidedTerminalID");
            entity.Property(e => e.SddgxsatelliteTerminalTypeId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("SDDGXsatelliteTerminalTypeID");
            entity.Property(e => e.SddtempdeactivationConfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddtempdeactivationConfigID");
            entity.Property(e => e.SdtServiceDeliveryPointId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sdtServiceDeliveryPointId");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serialNumber");
            entity.Property(e => e.ServiceDeliveryPointId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("serviceDeliveryPointID");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TerminalDid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("terminalDID");
            entity.Property(e => e.TerminalName)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("terminalName");
            entity.Property(e => e.TerminalProvisionId).HasColumnName("terminalProvisionID");
            entity.Property(e => e.TerminalType)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("terminalType");

            entity.HasOne(d => d.Node).WithMany(p => p.TbTerminalSummaries)
                .HasForeignKey(d => d.NodeId)
                .HasConstraintName("FK_tbTerminalSummary_tbNode");

            entity.HasOne(d => d.TerminalProvision).WithMany(p => p.TbTerminalSummaries)
                .HasForeignKey(d => d.TerminalProvisionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbTerminalSummary_tbTerminalProvisionInfo");
        });

        modelBuilder.Entity<TbTerminalTypeConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbterminalTypeConfig");

            entity.ToTable("tbTerminalTypeConfig", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Portcount).HasColumnName("portcount");
            entity.Property(e => e.SddproductItemconfig)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddproductItemconfig");
            entity.Property(e => e.TerminalType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("terminalType");
        });

        modelBuilder.Entity<TbTerminalUtilizationLookup>(entity =>
        {
            entity.ToTable("tbTerminalUtilizationLookup", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.UtilizationType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TbTerminalsvnConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbterminalsvnConfig");

            entity.ToTable("tbTerminalsvnConfig", "GX");

            entity.HasIndex(e => e.Svnid, "NonClusteredIndex-tbTerminalsvnConfig-snvid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ipaddress");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("((1))")
                .HasColumnName("isActive");
            entity.Property(e => e.Sat0Address)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sat0Address");
            entity.Property(e => e.SddsvnConfigId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddsvnConfigID");
            entity.Property(e => e.Subnet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subnet");
            entity.Property(e => e.Svnid).HasColumnName("svnid");
            entity.Property(e => e.TerminalProvisionInfoId).HasColumnName("terminalProvisionInfoID");

            entity.HasOne(d => d.Svn).WithMany(p => p.TbTerminalsvnConfigs)
                .HasForeignKey(d => d.Svnid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbterminalsvnConfig_tbsvn");

            entity.HasOne(d => d.TerminalProvisionInfo).WithMany(p => p.TbTerminalsvnConfigs)
                .HasForeignKey(d => d.TerminalProvisionInfoId)
                .HasConstraintName("FK_tbterminalsvnConfig_tbTerminalProvisionInfo");
        });

        modelBuilder.Entity<TbUsageBucket>(entity =>
        {
            entity.HasKey(e => e.UsageBucketid);

            entity.ToTable("tbUsageBucket", "starlink");

            entity.Property(e => e.UsageBucketid)
                .ValueGeneratedNever()
                .HasColumnName("usageBucketid");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbVar>(entity =>
        {
            entity.ToTable("tbVAR", "GX");

            entity.HasIndex(e => e.VarId, "UC_varID").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.VarId).HasColumnName("varID");

            entity.HasOne(d => d.Connector).WithMany(p => p.TbVars)
                .HasForeignKey(d => d.ConnectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbvar_tbconnector");
        });

        modelBuilder.Entity<TbVarCustomerBizMap>(entity =>
        {
            entity.ToTable("tbVarCustomerBizMap", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.CustbizId).HasColumnName("custbizId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.VarId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("varId");

            entity.HasOne(d => d.Custbiz).WithMany(p => p.TbVarCustomerBizMaps)
                .HasForeignKey(d => d.CustbizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbVarCustomerBizMap_tbcustomerBizMap");
        });

        modelBuilder.Entity<TbenmsGroup>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK_tbcustomerGrouping");

            entity.ToTable("tbenmsGroup");

            entity.Property(e => e.GroupId).HasColumnName("groupId");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("groupName");
            entity.Property(e => e.GroupTypeId).HasColumnName("groupTypeID");
            entity.Property(e => e.ParentGroupId).HasColumnName("parentGroupId");
            entity.Property(e => e.Udmapping)
                .HasColumnType("xml")
                .HasColumnName("udmapping");

            entity.HasOne(d => d.GroupType).WithMany(p => p.TbenmsGroups)
                .HasForeignKey(d => d.GroupTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbenmsGroup_tbenmsGroupType");
        });

        modelBuilder.Entity<TbenmsGroupType>(entity =>
        {
            entity.ToTable("tbenmsGroupType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.BizServiceId).HasColumnName("bizServiceId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.BizService).WithMany(p => p.TbenmsGroupTypes)
                .HasForeignKey(d => d.BizServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbenmsGroupType_tbBizService");
        });

        modelBuilder.Entity<TbmodemInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbmodemCustomerMap");

            entity.ToTable("tbmodemInfo", "iDirect");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.ModemType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("modemType");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
        });

        modelBuilder.Entity<TbnodeExclusionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbnodeExclusionList", "GX");

            entity.Property(e => e.ConnectorId).HasColumnName("connectorID");
            entity.Property(e => e.Dateadded)
                .HasColumnType("datetime")
                .HasColumnName("dateadded");
            entity.Property(e => e.GxTerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gxTerminalID");
        });

        modelBuilder.Entity<Tbobject>(entity =>
        {
            entity.HasKey(e => e.ObjectId);

            entity.ToTable("tbobject", "provision");

            entity.HasIndex(e => new { e.Name, e.ObjectTypeId }, "UC_object_objectType").IsUnique();

            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("createdBy");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ObjectData)
                .HasColumnType("xml")
                .HasColumnName("objectData");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");

            entity.HasOne(d => d.Connector).WithMany(p => p.Tbobjects)
                .HasForeignKey(d => d.ConnectorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobject_tbConnector");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.Tbobjects)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobject_tbobjectType");
        });

        modelBuilder.Entity<TbobjectConfig>(entity =>
        {
            entity.ToTable("tbobjectConfig", "provision");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.Object).WithMany(p => p.TbobjectConfigs)
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectConfig_tbobject");

            entity.HasOne(d => d.ObjectType).WithMany(p => p.TbobjectConfigs)
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectConfig_tbobjectType");
        });

        modelBuilder.Entity<TbobjectConfigValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbobjectConfigValue", "provision");

            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ObjectConfigId).HasColumnName("objectConfigId");
            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.ObjectConfig).WithMany()
                .HasForeignKey(d => d.ObjectConfigId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectConfigValue_tbobjectConfig");

            entity.HasOne(d => d.Object).WithMany()
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectConfigValue_tbobject");

            entity.HasOne(d => d.ObjectType).WithMany()
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectConfigValue_tbobjectType");
        });

        modelBuilder.Entity<TbobjectProcessConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbobjectProcessConfig", "provision");

            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsParentProcess)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("isParentProcess");
            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.ParentProcessId).HasColumnName("parentProcessId");
            entity.Property(e => e.ProcessInfo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("processInfo");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("processName");
            entity.Property(e => e.ProcessStepNumber).HasColumnName("processStepNumber");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Timestamp)
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("userName");

            entity.HasOne(d => d.Object).WithMany()
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nodeProcessConfig_tbObject");

            entity.HasOne(d => d.ObjectType).WithMany()
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_nodeProcessConfig_tbobjectType");
        });

        modelBuilder.Entity<TbobjectProcessLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbobjectProcessLog", "provision");

            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.LogType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ObjectId).HasColumnName("objectId");
            entity.Property(e => e.ObjectProcessId).HasColumnName("objectProcessId");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.Payload)
                .IsUnicode(false)
                .HasColumnName("payload");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("processName");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.Object).WithMany()
                .HasForeignKey(d => d.ObjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_objectProcessLog_object");

            entity.HasOne(d => d.ObjectType).WithMany()
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbobjectProcessLog_tbobjectType");
        });

        modelBuilder.Entity<TbobjectType>(entity =>
        {
            entity.ToTable("tbobjectType", "provision");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TbobjectTypeConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbobjectTypeConfig", "provision");

            entity.Property(e => e.ConfigKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configKey");
            entity.Property(e => e.ConfigValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("configValueType");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
            entity.Property(e => e.ValueUnit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("valueUnit");

            entity.HasOne(d => d.ObjectType).WithMany()
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbbjectTypeConfig_tbNodeType");
        });

        modelBuilder.Entity<TbobjectTypeProcessConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbobjectTypeProcessConfig", "provision");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.IsParentProcess)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("isParentProcess");
            entity.Property(e => e.ObjectTypeId).HasColumnName("objectTypeId");
            entity.Property(e => e.ParentProcessId).HasColumnName("parentProcessId");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("processName");
            entity.Property(e => e.ProcessStepNumber).HasColumnName("processStepNumber");

            entity.HasOne(d => d.ObjectType).WithMany()
                .HasForeignKey(d => d.ObjectTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_objectTypeProcessConfig_tbobjectType");
        });

        modelBuilder.Entity<TbterminalSvnSession>(entity =>
        {
            entity.ToTable("tbterminalSvnSession", "usgbu");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DateAdded)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateAdded");
            entity.Property(e => e.Dateupdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateupdated");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.SessionEnd)
                .HasColumnType("datetime")
                .HasColumnName("sessionEnd");
            entity.Property(e => e.SessionEndFlag).HasColumnName("sessionEndFlag");
            entity.Property(e => e.SessionStart)
                .HasColumnType("datetime")
                .HasColumnName("sessionStart");
            entity.Property(e => e.Subscriber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("subscriber");
            entity.Property(e => e.Svn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("svn");
        });

        modelBuilder.Entity<TbterminalenmsGroup>(entity =>
        {
            entity.HasKey(e => e.TerminalgroupId).HasName("PK_tbcustomerGroup");

            entity.ToTable("tbterminalenmsGroup", "GX");

            entity.Property(e => e.TerminalgroupId).HasColumnName("terminalgroupId");
            entity.Property(e => e.Active)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.EnmsGroupId).HasColumnName("enmsGroupID");
            entity.Property(e => e.TerminalprovisionId).HasColumnName("terminalprovisionID");

            entity.HasOne(d => d.EnmsGroup).WithMany(p => p.TbterminalenmsGroups)
                .HasForeignKey(d => d.EnmsGroupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbterminalCustomergroup_tbcustomerGroup");
        });

        modelBuilder.Entity<TmptbNodeMetricValueWithBackfill>(entity =>
        {
            entity.ToTable("tmptbNodeMetricValueWithBackfill");

            entity.HasIndex(e => new { e.NodeId, e.NodeTypeId, e.MetricKey, e.Timestamp }, "UC_Node_NodeType_MetricKey_TimestampTS").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BackFillMetricValueId).HasColumnName("backFillMetricValueID");
            entity.Property(e => e.EpochTimestamp).HasColumnName("epochTimestamp");
            entity.Property(e => e.IsbackFilled).HasColumnName("isbackFilled");
            entity.Property(e => e.LastPolledTimestamp).HasColumnName("lastPolledTimestamp");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Metricvalue1)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue1");
            entity.Property(e => e.Metricvalue2)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue2");
            entity.Property(e => e.Metricvalue3)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("metricvalue3");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeMetricId).HasColumnName("nodeMetricId");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.Timestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");

            entity.HasOne(d => d.Node).WithMany(p => p.TmptbNodeMetricValueWithBackfills)
                .HasForeignKey(d => d.NodeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmptbNodeMetricValueWithBackfill_tbNode");

            entity.HasOne(d => d.NodeMetric).WithMany(p => p.TmptbNodeMetricValueWithBackfills)
                .HasForeignKey(d => d.NodeMetricId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tmptbNodeMetricValueWithBackfill_tbNodeMetric");
        });

        modelBuilder.Entity<TmptbSvnCustomerMap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbSvnCustomerMap");

            entity.ToTable("tmptbSvnCustomerMap", "GX");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("customerId");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateDeleted)
                .HasColumnType("datetime")
                .HasColumnName("dateDeleted");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Svn).HasColumnName("svn");
        });

        modelBuilder.Entity<ViewConfigValue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_ConfigValue", "GX");

            entity.Property(e => e.Configkey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configkey");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<ViewGxterminalDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_GXTerminalDetails", "GX");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Connector)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("connector");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.GxterminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GXTerminalID");
            entity.Property(e => e.LastStatusTimestamp).HasColumnType("datetime");
            entity.Property(e => e.NodeId).HasColumnName("NodeID");
            entity.Property(e => e.NodeType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TerminalName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TerminalServicePlan)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TerminalStatus)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.TerminalStatusdesc)
                .HasMaxLength(1024)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewGxterminalSdddetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_GXTerminalSDDDetails", "GX");

            entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.Cleid).HasColumnName("cleid");
            entity.Property(e => e.CoreModuleId)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("coreModuleID");
            entity.Property(e => e.Customer)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customer");
            entity.Property(e => e.Customerid)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("customerid");
            entity.Property(e => e.Enmsnodeid).HasColumnName("enmsnodeid");
            entity.Property(e => e.Gxterminalid)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gxterminalid");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MeetmePoint)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("meetmePoint");
            entity.Property(e => e.ProvStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prov_status");
            entity.Property(e => e.ProvStatusId).HasColumnName("prov_statusId");
            entity.Property(e => e.SddInstanceId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sddInstanceID");
            entity.Property(e => e.SdtServiceDeliveryPointId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sdtServiceDeliveryPointId");
            entity.Property(e => e.SerialNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("serialNumber");
            entity.Property(e => e.SiteId).HasColumnName("siteID");
            entity.Property(e => e.TerminalDid)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("terminalDID");
            entity.Property(e => e.TerminalProvisionId).HasColumnName("terminalProvisionId");
            entity.Property(e => e.TerminalRmp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("terminalRMP");
            entity.Property(e => e.TerminalType)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.Terminalname)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("terminalname");
            entity.Property(e => e.Tpk)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("tpk");
            entity.Property(e => e.UtilizationTypeId).HasColumnName("utilizationTypeID");
            entity.Property(e => e.Var)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("VAR");
            entity.Property(e => e.VarId).HasColumnName("varId");
        });

        modelBuilder.Entity<ViewGxterminalStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_GXTerminalStatus", "GX");

            entity.Property(e => e.GxterminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GXTerminalID");
            entity.Property(e => e.LastStatusTimestamp).HasColumnType("datetime");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Nodestatus)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.Nodestatusdesc)
                .HasMaxLength(1024)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewGxterminalStatusBySvn>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_GXTerminalStatusBySVN", "GX");

            entity.Property(e => e.GxterminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("GXTerminalID");
            entity.Property(e => e.LastStatusTimestamp).HasColumnType("datetime");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Nodestatus)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.Nodestatusdesc)
                .HasMaxLength(1024)
                .IsUnicode(false);
            entity.Property(e => e.Svn)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("svn");
        });

        modelBuilder.Entity<ViewNodeConfig>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_NodeConfig", "iDirect");

            entity.Property(e => e.Configkey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("configkey");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Value)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("value");
        });

        modelBuilder.Entity<ViewSpeedcastTerminalStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_SpeedcastTerminalStatus", "speedcast");

            entity.Property(e => e.LastStatusTimestamp).HasColumnType("datetime");
            entity.Property(e => e.NodeName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nodeName");
            entity.Property(e => e.Nodeid).HasColumnName("nodeid");
            entity.Property(e => e.Nodestatus)
                .HasMaxLength(1024)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewTerminalSvnsession>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_terminalSVNSession", "GX");

            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.SessionEnd)
                .HasColumnType("datetime")
                .HasColumnName("sessionEnd");
            entity.Property(e => e.SessionStart)
                .HasColumnType("datetime")
                .HasColumnName("sessionStart");
            entity.Property(e => e.Source)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("source");
            entity.Property(e => e.Svn)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("svn");
        });

        modelBuilder.Entity<VwActiveTerminalInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwActiveTerminalInfo", "speedcast");

            entity.Property(e => e.NetworkName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("networkName");
            entity.Property(e => e.NodeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("nodeId");
            entity.Property(e => e.TerminalDid)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terminalDID");
            entity.Property(e => e.TerminalId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terminalId");
            entity.Property(e => e.TerminalName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terminalName");
            entity.Property(e => e.TerminalSerialNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("terminalSerialNumber");
        });

        modelBuilder.Entity<VwGetAccountsByUserName>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwGetAccountsByUserName", "starlink");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("customerName");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");
            entity.Property(e => e.TerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("terminalID");
            entity.Property(e => e.UserId).HasColumnName("userId");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");
        });

        modelBuilder.Entity<VwNode>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwNode");

            entity.Property(e => e.ActivationDate)
                .HasColumnType("datetime")
                .HasColumnName("activationDate");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.ConnectorCreds)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("connectorCreds");
            entity.Property(e => e.ConnectorId).HasColumnName("connectorId");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.DeactivationDate)
                .HasColumnType("datetime")
                .HasColumnName("deactivationDate");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.LastPollDate)
                .HasColumnType("datetime")
                .HasColumnName("lastPollDate");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NextPollDate)
                .HasColumnType("datetime")
                .HasColumnName("nextPollDate");
            entity.Property(e => e.NodeData).HasColumnName("nodeData");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.NodeTypeTemplateId).HasColumnName("nodeTypeTemplateId");
            entity.Property(e => e.PollPolicy)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pollPolicy");
            entity.Property(e => e.PollerId).HasColumnName("pollerId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.ProcessorName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("processorName");
            entity.Property(e => e.SourceInfo)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("sourceInfo");
            entity.Property(e => e.UseMetricTemplate).HasColumnName("useMetricTemplate");
        });

        modelBuilder.Entity<VwNodeMetric>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwNodeMetric");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricSourceInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricSourceInfo");
            entity.Property(e => e.MetricUnits)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricUnits");
            entity.Property(e => e.MetricValue1Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue1Label");
            entity.Property(e => e.MetricValue2Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue2Label");
            entity.Property(e => e.MetricValue3Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue3Label");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeId).HasColumnName("nodeId");
            entity.Property(e => e.NodeMetricData).HasColumnName("nodeMetricData");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
        });

        modelBuilder.Entity<VwNodeStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwNodeStatus", "starlink");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.UserTerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userTerminalId");
        });

        modelBuilder.Entity<VwNodeType>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwNodeType");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Config).HasColumnName("config");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<VwNodeTypeMetric>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwNodeTypeMetric");

            entity.Property(e => e.Config).HasColumnName("config");
            entity.Property(e => e.DateCreated)
                .HasColumnType("datetime")
                .HasColumnName("dateCreated");
            entity.Property(e => e.DateUpdated)
                .HasColumnType("datetime")
                .HasColumnName("dateUpdated");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("displayName");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsCalculatedMetric)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("isCalculatedMetric");
            entity.Property(e => e.MetricKey)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("metricKey");
            entity.Property(e => e.MetricSourceInfo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricSourceInfo");
            entity.Property(e => e.MetricUnits)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricUnits");
            entity.Property(e => e.MetricValue1Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue1Label");
            entity.Property(e => e.MetricValue2Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue2Label");
            entity.Property(e => e.MetricValue3Label)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("metricValue3Label");
            entity.Property(e => e.MetricValueType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("metricValueType");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NodeTypeId).HasColumnName("nodeTypeId");
            entity.Property(e => e.PollingInterval).HasColumnName("pollingInterval");
            entity.Property(e => e.PreFillValueConfig).HasColumnName("preFillValueConfig");
            entity.Property(e => e.RollUpAggregateConfig).HasColumnName("rollUpAggregateConfig");
            entity.Property(e => e.SampleValue)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("sampleValue");
        });

        modelBuilder.Entity<VwSubscription>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwSubscriptions", "starlink");

            entity.Property(e => e.AccountNumber)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("accountNumber");
            entity.Property(e => e.ProductRefId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("productRefId");
            entity.Property(e => e.ServiceEnd)
                .HasColumnType("datetime")
                .HasColumnName("serviceEnd");
            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");
            entity.Property(e => e.ServiceStart)
                .HasColumnType("datetime")
                .HasColumnName("serviceStart");
            entity.Property(e => e.SubscriptionRefId)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("subscriptionRefId");
        });

        modelBuilder.Entity<VwTerminal>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwTerminals", "starlink");

            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.DeactivationDate)
                .HasColumnType("datetime")
                .HasColumnName("deactivationDate");
            entity.Property(e => e.DishSerialNumber)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("dishSerialNumber");
            entity.Property(e => e.KitSerialNumber)
                .HasMaxLength(1024)
                .IsUnicode(false)
                .HasColumnName("kitSerialNumber");
            entity.Property(e => e.ServiceLineNumber)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("serviceLineNumber");
            entity.Property(e => e.UserTerminalId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("userTerminalId");
        });

        modelBuilder.Entity<GetTelemetries_Result>(entity =>
        {
            entity.HasNoKey();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
