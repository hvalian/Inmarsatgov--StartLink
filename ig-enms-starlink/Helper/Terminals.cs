using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace IG.ENMS.Starlink.Helper
{
    public class Terminals
    {
        public static async Task<IG.ENMS.Starlink.Data.UserTerminals> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            IG.ENMS.Starlink.Data.UserTerminals terminals = new IG.ENMS.Starlink.Data.UserTerminals(_logger);

            if (dbContext == null)
            {
                logger.LogError("Error processing terminals. Can't create instance of IgenmsContext.");
                return terminals;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<VwTerminal> listOfTerminals = await Task.Run(() => dbContext.VwTerminals.ToList());

            foreach (VwTerminal terminal in listOfTerminals)
            {
                IG.ENMS.Starlink.Models.UserTerminal userTerminal = new IG.ENMS.Starlink.Models.UserTerminal();

                userTerminal.UserTerminalId = terminal.UserTerminalId ?? "";
                userTerminal.KitSerialNumber = terminal.KitSerialNumber ?? "";
                userTerminal.DishSerialNumber = terminal.DishSerialNumber ?? "";
                userTerminal.ServiceLineNumber = terminal.ServiceLineNumber ?? "";
                userTerminal.Active = (bool)terminal.Active;
                userTerminal.DeactivationDate = terminal.DeactivationDate;
                terminals.Add(userTerminal);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Terminal, "FetchData", (int)watch.ElapsedMilliseconds, terminals.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return terminals;
        }

        public static async Task Save(IConfiguration configuration, ILogger<ServiceData> logger, UserTerminals terminals)
        {
            if (terminals == null || (terminals != null && terminals.Count() == 0))
                return;

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing terminals. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbNodeConfigValue> objectsToAdd = new List<TbNodeConfigValue>();
            List<TbNodeConfigValue> objectsToUpdate = new List<TbNodeConfigValue>();
            List<TbNode> nodes = await Task.Run(() => dbContext.TbNodes.ToList());

            foreach (UserTerminal t in terminals)
            {
                TbNode? node = nodes.FirstOrDefault(x => x.Name == t.UserTerminalId);

                if (node == null)
                {
                    var parameter = new List<object>();

                    var nodeNameParam = new SqlParameter("@nodeName", SqlDbType.VarChar);
                    nodeNameParam.Value = t.UserTerminalId;
                    nodeNameParam.Size = 50;
                    parameter.Add(nodeNameParam);

                    var nodeTypeIdParam = new SqlParameter("@nodeTypeId", SqlDbType.Int);
                    nodeTypeIdParam.Value = 20;
                    parameter.Add(nodeTypeIdParam);

                    var connectorIdParam = new SqlParameter("@connectorId", SqlDbType.Int);
                    connectorIdParam.Value = 21;
                    parameter.Add(connectorIdParam);

                    var connectorCrParam = new SqlParameter("@connectorCreds", SqlDbType.VarChar);
                    connectorCrParam.Value = DBNull.Value;
                    connectorCrParam.Size = 1024;
                    parameter.Add(connectorCrParam);

                    var sourceInfoParam = new SqlParameter("@sourceInfo", SqlDbType.VarChar);
                    sourceInfoParam.Value = DBNull.Value;
                    sourceInfoParam.Size = 128;
                    parameter.Add(sourceInfoParam);

                    var pollerIdParam = new SqlParameter("@pollerId", SqlDbType.Int);
                    pollerIdParam.Value = 7;
                    parameter.Add(pollerIdParam);

                    var useMetricTemplateParam = new SqlParameter("@useMetricTemplate", SqlDbType.Bit);
                    useMetricTemplateParam.Value = 1;
                    parameter.Add(useMetricTemplateParam);

                    var nodeTypeTemplateIdParam = new SqlParameter("@nodeTypeTemplateId", SqlDbType.Int);
                    nodeTypeTemplateIdParam.Value = DBNull.Value;
                    parameter.Add(nodeTypeTemplateIdParam);

                    var pollingIntervalParam = new SqlParameter("@pollingInterval", SqlDbType.Int);
                    pollingIntervalParam.Value = 1;
                    parameter.Add(pollingIntervalParam);

                    var pollPolicyParam = new SqlParameter("@pollPolicy", SqlDbType.VarChar);
                    pollPolicyParam.Value = DBNull.Value;
                    pollPolicyParam.Size = 20;
                    parameter.Add(pollPolicyParam);

                    var nodeDataParam = new SqlParameter("@nodeData", SqlDbType.VarChar);
                    nodeDataParam.Value = DBNull.Value;
                    nodeDataParam.Size = -1;
                    parameter.Add(nodeDataParam);

                    var resultParameter = new SqlParameter("@returnCode", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    resultParameter.Value = DBNull.Value;
                    parameter.Add(resultParameter);

                    var messageParameter = new SqlParameter("@returnMessage", SqlDbType.VarChar, -1) { Direction = ParameterDirection.Output };
                    messageParameter.Value = DBNull.Value;
                    parameter.Add(messageParameter);

                    try
                    {
                        var sql = "exec [dbo].[spAddNode] @nodeName, @nodeTypeId, @connectorId, @connectorCreds, @sourceInfo, @pollerId, " +
                             "@useMetricTemplate, @nodeTypeTemplateId, @pollingInterval, @pollPolicy, @nodeData, @returnCode out, @returnMessage out";

                        var data = dbContext.Database.ExecuteSqlRaw(sql, parameter.ToArray());
                    }
                    catch (Exception ex)
                    {
                        Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "SP: [spAddNode]", ex, "Error adding node.  NodeId: " + t.UserTerminalId);
                    }
                }
            }

            List<TbNode> listOfNodes = await Task.Run(() => dbContext.TbNodes
                .Include(x => x.TbNodeConfigs)
                .Include(x => x.TbNodeConfigValues)
                .ToList());

            foreach (UserTerminal ut in terminals)
            {
                try
                {
                    TbNode? node = listOfNodes.FirstOrDefault(n => n.Name == ut.UserTerminalId);

                    if (node != null)
                    {
                        foreach (TbNodeConfig config in node.TbNodeConfigs)
                        {
                            TbNodeConfigValue? configValue = node.TbNodeConfigValues
                                .Where(r => r.ConfigKey == config.ConfigKey)
                                .OrderByDescending(o => o.Timestamp).Take(1).FirstOrDefault();

                            string value = (config.ConfigKey == "KitSerialNumber") ? ut.KitSerialNumber : (config.ConfigKey == "DishSerialNumber") ? ut.DishSerialNumber : "";
                            value = string.IsNullOrEmpty(value) ? "" : value;
                            string currentvalue = string.IsNullOrEmpty(configValue.Value) ? "" : configValue.Value;

                            if (configValue != null && value != currentvalue)
                            {
                                DateTime ts = (configValue.Timestamp > Helper.Utility.GetDateTime()) ? configValue.Timestamp.AddMinutes(1) : Helper.Utility.GetDateTime();
                                TbNodeConfigValue tbNodeConfigValue = new TbNodeConfigValue();
                                tbNodeConfigValue.AddedBy = Helper.Utility.GetCreatedBy();
                                tbNodeConfigValue.ConfigKey = config.ConfigKey;
                                tbNodeConfigValue.ConfigValueType = "string";
                                tbNodeConfigValue.DateCreated = ts;
                                tbNodeConfigValue.Description = config.Description;
                                tbNodeConfigValue.Name = config.Name;
                                tbNodeConfigValue.NodeConfigId = config.Id;
                                tbNodeConfigValue.NodeId = node.Id;
                                tbNodeConfigValue.NodeTypeId = node.NodeTypeId;
                                tbNodeConfigValue.Timestamp = ts;
                                tbNodeConfigValue.Value = value;
                                tbNodeConfigValue.ValueUnit = "";
                                objectsToAdd.Add(tbNodeConfigValue);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "Error processing terminals.  UserTerminalId: " + ut.UserTerminalId);
                }
            }

            if (objectsToAdd.Count > 0)
            {
                try
                {
                    dbContext.AddRange(objectsToAdd);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "AddRange");
                }
            }

            if (objectsToUpdate.Count > 0)
            {
                try
                {
                    dbContext.UpdateRange(objectsToUpdate);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "UpdateRange");
                }
            }

            List<TbServiceLine> listOfServiceLines = await Task.Run(() => dbContext.TbServiceLines.ToList());
            List<TbTerminalServiceLineMap> listOfTerminalServiceLineMap = await Task.Run(() => dbContext.TbTerminalServiceLineMaps.ToList());
            List<TbTerminalServiceLineMap> tbTerminalServiceLineMapToAdd = new List<TbTerminalServiceLineMap>();
            List<TbTerminalServiceLineMap> tbTerminalServiceLineMapToUpdate = new List<TbTerminalServiceLineMap>();
            List<TbServiceLine> tbServiceLineToAdd = new List<TbServiceLine>();

            foreach (UserTerminal xt in terminals)
            {
                try
                {
                    TbNode? node = listOfNodes.FirstOrDefault(n => n.Name == xt.UserTerminalId);

                    if (node != null)
                    {
                        DateTime timestamp = Helper.Utility.GetDateTime();

                        TbTerminalServiceLineMap? tbTerminalServiceLineMap = listOfTerminalServiceLineMap.FirstOrDefault(x => x.NodeId == node.Id && x.ServiceLineNumber == xt.ServiceLineNumber);

                        if (tbTerminalServiceLineMap != null  && !xt.Active && tbTerminalServiceLineMap.DeactivationDate == null)
                        { 
                            tbTerminalServiceLineMap.DeactivationDate = timestamp;
                            tbTerminalServiceLineMap.DateUpdated = timestamp;
                            tbTerminalServiceLineMapToUpdate.Add(tbTerminalServiceLineMap);
                        }

                        if (tbTerminalServiceLineMap == null &&!string.IsNullOrEmpty(xt.ServiceLineNumber))
                        {
                            TbServiceLine? tbServiceLine = listOfServiceLines.FirstOrDefault(x => x.ServiceLineNumber == xt.ServiceLineNumber);
                            if (tbServiceLine == null)
                            {
                                foreach (var s in terminals.Where(kv => kv.UserTerminalId == xt.UserTerminalId))
                                {
                                    tbServiceLine = listOfServiceLines.FirstOrDefault(x => x.ServiceLineNumber == s.ServiceLineNumber);
                                    if (tbServiceLine != null)
                                    {
                                        TbServiceLine newTBServiceLine = new TbServiceLine();
                                        newTBServiceLine.DateCreated = timestamp;
                                        newTBServiceLine.AccountNumber = tbServiceLine.AccountNumber;
                                        newTBServiceLine.Active = false;
                                        newTBServiceLine.AddressReferenceId = tbServiceLine.AddressReferenceId ?? "";
                                        newTBServiceLine.Name = tbServiceLine.Name;
                                        newTBServiceLine.ServiceLineNumber = xt.ServiceLineNumber;
                                        newTBServiceLine.IsTerminalRecord = true;
                                        tbServiceLineToAdd.Add(newTBServiceLine);
                                        break;
                                    }
                                }
                            }

                            tbServiceLine = listOfServiceLines.FirstOrDefault(x => x.ServiceLineNumber == xt.ServiceLineNumber);
                            if (tbServiceLine != null)
                            {
                                tbTerminalServiceLineMap = new TbTerminalServiceLineMap();
                                tbTerminalServiceLineMap.ActivationDate = timestamp;
                                tbTerminalServiceLineMap.DateCreated = timestamp;
                                tbTerminalServiceLineMap.DeactivationDate = (xt.Active) ? null : timestamp;
                                tbTerminalServiceLineMap.NodeId = node.Id;
                                tbTerminalServiceLineMap.ServiceLineNumber = xt.ServiceLineNumber;
                                tbTerminalServiceLineMap.DateUpdated = timestamp;
                                listOfTerminalServiceLineMap.Add(tbTerminalServiceLineMap);
                                tbTerminalServiceLineMapToAdd.Add(tbTerminalServiceLineMap);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "Error processing terminals.  UserTerminalId: " + xt.UserTerminalId);
                }
            }

            if (tbServiceLineToAdd.Count > 0)
            {
                try
                {
                    dbContext.AddRange(tbServiceLineToAdd);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "AddRange");
                }
            }


            if (tbTerminalServiceLineMapToAdd.Count > 0)
            {
                try
                {
                    dbContext.AddRange(tbTerminalServiceLineMapToAdd);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "AddRange");
                }
            }

            if (tbTerminalServiceLineMapToUpdate.Count > 0)
            {
                try
                {
                    dbContext.UpdateRange(tbTerminalServiceLineMapToUpdate);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "Save", ex, "UpdateRange");
                }
            }

            List<TbNode> tbNodeToUpdate = new List<TbNode>();
            List<VwNodeStatus> vwStatuses = await Task.Run(() => dbContext.VwNodeStatuses.ToList());

            foreach (VwNodeStatus vwStatus in vwStatuses)
            {
                TbNode? node = listOfNodes.FirstOrDefault(n => n.Name == vwStatus.UserTerminalId);
                if (node != null)
                {
                    if (node.Active != vwStatus.Active)
                    {
                        node.Active = vwStatus.Active;
                        node.DeactivationDate = (vwStatus.Active == true) ? null : Utility.GetDateTime();
                        node.DateUpdated = Helper.Utility.GetDateTime();
                        tbNodeToUpdate.Add(node);
                    }
                }
            }

            if (tbNodeToUpdate.Count > 0)
            {
                try
                {
                    dbContext.UpdateRange(tbNodeToUpdate);
                    dbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "UpdateNodes", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Terminal, "UpdateNodes", ex, "UpdateRange");
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Terminal, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task<IG.ENMS.Starlink.Data.UserTerminals> Sync(IConfiguration configuration, ILogger<ServiceData> logger, Data.UserTerminals newData, Data.UserTerminals existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing terminals. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.UserTerminals returnData = newData;


            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (UserTerminal userTerminal in newData)
                {
                    existingData.Remove(userTerminal);
                }

                foreach (UserTerminal userTerminal in existingData)
                {
                    newData.Add(userTerminal);
                }
            }

            await Task.Delay(1);

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Terminal, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            return returnData;
        }

        public static string GetKey(UserTerminal terminal)
        {
            return terminal.UserTerminalId +":" + terminal.ServiceLineNumber;
        }

    }
}
