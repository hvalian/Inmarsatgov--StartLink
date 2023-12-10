using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class ServiceLines
    {
        public static async Task<IG.ENMS.Starlink.Data.ServiceLines> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            IG.ENMS.Starlink.Data.ServiceLines serviceLines = new IG.ENMS.Starlink.Data.ServiceLines(_logger);

            if (dbContext == null)
            {
                logger.LogError("Error processing servicelines. Can't create instance of IgenmsContext.");
                return serviceLines;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbServiceLineSubscriptionMap> listOfServiceLineSubscriptionMap = await Task.Run(() => dbContext.TbServiceLineSubscriptionMaps.ToList());
            List<TbServiceLine> listOfServiceLines = await Task.Run(() => dbContext.TbServiceLines.ToList());

            foreach (TbServiceLine tbServiceLine in listOfServiceLines)
            {
                IG.ENMS.Starlink.Models.ServiceLine serviceLine = new IG.ENMS.Starlink.Models.ServiceLine(_logger);
                TbServiceLineSubscriptionMap? tbServiceLineSubscriptionMap = listOfServiceLineSubscriptionMap.FirstOrDefault(s => s.ServiceLineNumber == serviceLine.ServiceLineNumber);
                IG.ENMS.Starlink.Models.ServicePlan servicePlan = new IG.ENMS.Starlink.Models.ServicePlan();
                if (tbServiceLineSubscriptionMap != null)
                {
                    servicePlan.ActiveFrom = (DateTime)tbServiceLineSubscriptionMap.ActiveFrom;
                    servicePlan.OverageName = tbServiceLineSubscriptionMap.OverageName;
                    servicePlan.OverageDescription = tbServiceLineSubscriptionMap.OverageDescription;
                    servicePlan.IsOptedIntoOverage = (bool)tbServiceLineSubscriptionMap.IsOptedIntoOverage;
                    servicePlan.PricePerGB = (double)tbServiceLineSubscriptionMap.PricePerGb;
                    servicePlan.UsageLimitGB = (double)tbServiceLineSubscriptionMap.UsageLimitGb;
                    servicePlan.OverageAmountGB = (double)tbServiceLineSubscriptionMap.OverageAmountGb;
                    servicePlan.OveragePrice = (double)tbServiceLineSubscriptionMap.OveragePrice;
                }
                serviceLine.AccountNumber = tbServiceLine.AccountNumber ?? "";
                serviceLine.Active = tbServiceLine.Active;
                serviceLine.AddressReferenceId = tbServiceLine.AddressReferenceId ?? "";
                serviceLine.Nickname = tbServiceLine.Name ?? "";
                serviceLine.ServiceLineNumber = tbServiceLine.ServiceLineNumber;
                serviceLine.ServicePlan = servicePlan;
                serviceLines.Add(serviceLine);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.ServiceLine, "FetchData", (int)watch.ElapsedMilliseconds, serviceLines.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return serviceLines;
        }

        private static bool HasChanged(TbServiceLine currentServiceLine, ServiceLine newServiceLine)
        {
            return (
                currentServiceLine.AccountNumber != newServiceLine.AccountNumber ||
                currentServiceLine.Active != newServiceLine.Active ||
                currentServiceLine.AddressReferenceId != newServiceLine.AddressReferenceId ||
                currentServiceLine.Name != newServiceLine.Nickname
                );
        }

        public static async Task Save(IConfiguration configuration, ILogger<ServiceData> logger, Data.ServiceLines serviceLines)
        {
            if (serviceLines == null || (serviceLines != null && serviceLines.Count() == 0))
                return;

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing servicelines. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbServiceLine> objectsToAdd = new List<TbServiceLine>();
            List<TbServiceLine> objectsToUpdate = new List<TbServiceLine>();
            List<TbServiceLine> listOfServiceLines = await Task.Run(() => dbContext.TbServiceLines.ToList());
            List<TbAddress> listOfAddress = await Task.Run(() => dbContext.TbAddresses.ToList());

            foreach (ServiceLine serviceLine in serviceLines)
            {
                try
                {
                    bool isNew = false;
                    bool hasChanged = false;

                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbServiceLine? tbServiceLine = listOfServiceLines.FirstOrDefault(s => s.ServiceLineNumber == serviceLine.ServiceLineNumber);

                    if (tbServiceLine != null)
                    {
                        hasChanged = HasChanged(tbServiceLine, serviceLine);
                    }
                    else
                    {
                        isNew = true;
                        tbServiceLine = new TbServiceLine();
                        tbServiceLine.DateCreated = timestamp;
                    }

                    if (isNew || hasChanged)
                    {
                        tbServiceLine.AccountNumber = serviceLine.AccountNumber;
                        tbServiceLine.Active = serviceLine.Active;
                        tbServiceLine.AddressReferenceId = serviceLine.AddressReferenceId;
                        tbServiceLine.DateUpdated = timestamp;
                        tbServiceLine.Name = serviceLine.Nickname;
                        tbServiceLine.ServiceLineNumber = serviceLine.ServiceLineNumber;
                        tbServiceLine.IsTerminalRecord = false;

                        TbAddress? tbAddress = listOfAddress.FirstOrDefault(a => a.AddressReferenceId == serviceLine.AddressReferenceId);

                        if (tbAddress != null)
                        {
                            if (isNew)
                            {
                                objectsToAdd.Add(tbServiceLine);
                            }
                            else
                            {
                                objectsToUpdate.Add(tbServiceLine);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "Error processing servicelines.  ServiceLineNumber: " + serviceLine.ServiceLineNumber);
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
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "AddRange");
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
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.ServiceLine, "Save", ex, "UpdateRange");
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.ServiceLine, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task<IG.ENMS.Starlink.Data.ServiceLines> Sync(IConfiguration configuration, ILogger<ServiceData> logger, Data.ServiceLines newData, Data.ServiceLines existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing ServiceLines. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.ServiceLines returnData = newData;

            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (ServiceLine serviceLine in newData)
                {
                    string serviceLineNumber = serviceLine.ServiceLineNumber;
                    existingData.Remove(serviceLineNumber);
                }

                foreach (ServiceLine serviceLine in existingData)
                {
                    newData.Add(serviceLine);
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.ServiceLine, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return returnData;
        }
    }
}
