ALTER TABLE [dbo].[tbNodeMetricValue]
DROP CONSTRAINT [PK_tbNodeMetricValue]
GO

ALTER TABLE [dbo].[tbNodeMetricValue] ADD  CONSTRAINT [PK_tbNodeMetricValue] PRIMARY KEY CLUSTERED 
(
	[nodeId] ASC,
	[metricKey] ASC,
	[timestamp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

ALTER TABLE [starlink].[tbServiceLineUsage] DROP CONSTRAINT [PK_tbServiceLineUsage] WITH ( ONLINE = OFF )
GO

ALTER TABLE [starlink].[tbServiceLineUsage] ADD  CONSTRAINT [PK_tbServiceLineUsage] PRIMARY KEY CLUSTERED 
(
	[ServiceLineNumber] ASC
	,[usageTS] ASC
	,[usageBucketId] ASC
	,[recordType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[starlink].[tbLog]') AND type in (N'U'))
DROP TABLE [starlink].[tbLog]
GO

CREATE TABLE [starlink].[tbLog](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[logType] [varchar](32) NOT NULL,
	[category] [varchar](32) NOT NULL,
	[methodName] [varchar](1024) NOT NULL,
	[timestamp] [datetime] NULL,
	[elapsedTime] [int] NOT NULL,
	[message] [varchar](max) NULL,
	[affectedRows] [int] NULL,
	[exceptionMessage] [varchar](max) NULL,
	[innerException] [varchar](max) NULL,
	[stackTrace] [varchar](max) NULL,
 CONSTRAINT [PK_tbLog] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[starlink].[GetTelemetries]') AND OBJECTPROPERTY(object_id, N'IsProcedure') = 1)
DROP PROCEDURE [starlink].[GetTelemetries]
GO

CREATE PROCEDURE [starlink].[GetTelemetries] 	
    @datetime datetime
AS
BEGIN
  SELECT n.[name] as 'TerminalId'
	  ,sum(CASE WHEN [metricKey] = 'DownlinkThroughput' THEN    CAST(value as float)  ELSE null END) AS 'DownlinkThroughput'
	  ,sum(CASE WHEN [metricKey] = 'ObstructionPercentTime' THEN    CAST(value as float)  ELSE null END) AS 'ObstructionPercentTime'
	  ,sum(CASE WHEN [metricKey] = 'PingDropRateAvg' THEN    CAST(value as float)  ELSE null END) AS 'PingDropRateAvg'
	  ,sum(CASE WHEN [metricKey] = 'PingLatencyMsAvg' THEN    CAST(value as INT)  ELSE null END) AS 'PingLatencyMsAvg'
	  ,sum(CASE WHEN [metricKey] = 'SignalQuality' THEN    CAST(value as float)  ELSE null END) AS 'SignalQuality'
	  ,sum(CASE WHEN [metricKey] = 'UplinkThroughput' THEN    CAST(value as float)  ELSE null END) AS 'UplinkThroughput'
	  ,sum(CASE WHEN [metricKey] = 'Uptime' THEN    CAST(value as INT)  ELSE null END) AS 'Uptime'
      ,[timestamp]
      ,[epochTimestamp] as 'UtcTimestampNs'
  FROM [dbo].[tbNodeMetricValue] NMV Inner Join
	[dbo].[tbNode] N on NMV.nodeId = n.ID
	WHERE NMV.timestamp >= @datetime
  group by n.[name], timestamp, [epochTimestamp]
END
GO

IF EXISTS ( SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[starlink].[GetTelemetriesByTerminalId]') AND OBJECTPROPERTY(object_id, N'IsProcedure') = 1)
DROP PROCEDURE [starlink].[GetTelemetriesByTerminalId]
GO

CREATE PROCEDURE [starlink].[GetTelemetriesByTerminalId] 	
    @terminalId varchar(50),
    @fromDate datetime,
    @toDate datetime
AS
BEGIN
  SELECT n.[name] as 'TerminalId'
	  ,sum(CASE WHEN [metricKey] = 'DownlinkThroughput' THEN    CAST(value as float)  ELSE null END) AS 'DownlinkThroughput'
	  ,sum(CASE WHEN [metricKey] = 'ObstructionPercentTime' THEN    CAST(value as float)  ELSE null END) AS 'ObstructionPercentTime'
	  ,sum(CASE WHEN [metricKey] = 'PingDropRateAvg' THEN    CAST(value as float)  ELSE null END) AS 'PingDropRateAvg'
	  ,sum(CASE WHEN [metricKey] = 'PingLatencyMsAvg' THEN    CAST(value as INT)  ELSE null END) AS 'PingLatencyMsAvg'
	  ,sum(CASE WHEN [metricKey] = 'SignalQuality' THEN    CAST(value as float)  ELSE null END) AS 'SignalQuality'
	  ,sum(CASE WHEN [metricKey] = 'UplinkThroughput' THEN    CAST(value as float)  ELSE null END) AS 'UplinkThroughput'
	  ,sum(CASE WHEN [metricKey] = 'Uptime' THEN    CAST(value as INT)  ELSE null END) AS 'Uptime'
      ,[timestamp]
      ,[epochTimestamp] as 'UtcTimestampNs'
  FROM [dbo].[tbNodeMetricValue] NMV Inner Join
	[dbo].[tbNode] N on NMV.nodeId = n.ID
	WHERE n.name = @terminalId and NMV.timestamp >= @fromDate and NMV.timestamp < @toDate
  group by n.[name], timestamp, [epochTimestamp]
END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id  = OBJECT_ID(N'[starlink].[vwGetAccountsByUserName]') AND type='v' )
DROP VIEW [starlink].[vwGetAccountsByUserName]
GO

create view [starlink].[vwGetAccountsByUserName]
as
SELECT distinct cm.userId, u.userName, Internal,customerId, c.name as customerName, c.externalID as accountNumber,s.serviceLineNumber, sm.nodeId, n.name as terminalID 
  FROM igcms.dbo.tbCustomerUserMap cm join 
	igcms.dbo.tbcustomer c on cm.customerid= c.id join 
	igcms.dbo.Users u on u.UserID=cm.userId left join 
	starlink.tbServiceLine s on s.accountNumber = c.externalID left join 
	starlink.tbTerminalServiceLineMap sm on sm.servicelinenumber = s.servicelinenumber  left join 
	dbo.tbnode n on n.id= sm.nodeid      
WHERE cm.isDeleted=0 and c.isdeleted=0 and u.Active =1
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id  = OBJECT_ID(N'[starlink].[vwNodeStatus]') AND type='v' )
DROP VIEW [starlink].[vwNodeStatus]
GO

create view [starlink].[vwNodeStatus]
as
SELECT [userTerminalId], CAST(MAX(CASE WHEN active=1 THEN 1 ELSE 0 END) AS bit) as [active]
  FROM [starlink].[vwTerminals]
Group by [userTerminalId]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id  = OBJECT_ID(N'[starlink].[vwSubscriptions]') AND type='v' )
DROP VIEW [starlink].[vwSubscriptions]
GO

CREATE view [starlink].[vwSubscriptions]
as
	SELECT sl.accountNumber, sm.[serviceLineNumber], sm.[subscriptionRefId], sm.[productRefId], sm.[serviceStart], sm.[serviceEnd]
	  FROM [starlink].[tbServiceLineSubscriptionMap] sm inner join 
		[starlink].[tbServiceLine] sl on sm.serviceLineNumber = sl.serviceLineNumber
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id  = OBJECT_ID(N'[starlink].[vwTerminals]') AND type='v' )
DROP VIEW [starlink].[vwTerminals]
GO

CREATE view [starlink].[vwTerminals]
as
SELECT  n.[name] as  'userTerminalId', kitSerialNumber,  dishSerialNumber, serviceLineNumber, tsl.deactivationDate,
	CAST(IIF(tsl.deactivationDate IS NULL, 1,0) As bit) As active
  FROM   [starlink].[tbTerminalServiceLineMap] TSL left join 
      [dbo].[tbNode] N on tsl.nodeId = n.id left Join 
  (
      select nc.[nodeId] as nodeid,  
            max(CASE WHEN nc.configkey = 'dishSerialNumber' THEN [value] ELSE null END) as 'dishSerialNumber',
            max(CASE WHEN nc.configkey = 'kitserialNumber' THEN [value] ELSE null END) as 'kitserialNumber'
      from tbnodeconfigvalue nc join (
            select nodeid, max(timestamP)maxTS, configkey 
            from tbnodeconfigvalue ncv inner join
                  dbo.[tbNode] n on ncv.nodeId = n.Id
            where ncv.nodetypeid=20 and configkey in (
            'dishSerialNumber', 'kitserialNumber') 
            group by nodeid, configkey) nc1 
      on nc.nodeid=nc1.nodeid and nc.timestamp=nc1.maxTS and nc.configkey=nc1.configkey   
      group by nc.nodeId
  ) q on tsl.nodeId = q.nodeid
GO
