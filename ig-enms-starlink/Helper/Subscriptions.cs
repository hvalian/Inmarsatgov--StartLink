using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Subscriptions
    {
        public static async Task<IG.ENMS.Starlink.Data.Subscriptions> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext dbContext;

            IG.ENMS.Starlink.Data.Subscriptions subscriptions = new IG.ENMS.Starlink.Data.Subscriptions(_logger);

            string? igenmsConnectionString = _configuration.GetValue<string>("ConnectionStrings:IgenmsContext");
            if (string.IsNullOrEmpty(igenmsConnectionString))
            {
                _logger.LogError("Error processing servicelines. ConnectionStrings is null");
                return subscriptions;
            }

            try
            {
                dbContext = new IgenmsContext(igenmsConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error processing servicelines. Can't create instance of IgenmsContext. Error: {error} InnerException: {InnerException}", ex.Message, ex.InnerException);
                return subscriptions;
            }
            List<VwSubscription> listOfSubscriptions = await Task.Run(() => dbContext.VwSubscriptions.ToList());

            foreach (VwSubscription subscriptionMap in listOfSubscriptions)
            {
                IG.ENMS.Starlink.Models.Subscription subscription = new IG.ENMS.Starlink.Models.Subscription();
                subscription.AccountNumber = subscriptionMap.AccountNumber ?? "";
                subscription.ProductReferenceId = subscriptionMap.ProductRefId ?? "";
                subscription.ServiceEndDate = (subscriptionMap.ServiceEnd == null) ? System.DateTime.MinValue : (DateTime)subscriptionMap.ServiceEnd;
                subscription.ServiceLineNumber = subscriptionMap.ServiceLineNumber;
                subscription.StartDate = (subscriptionMap.ServiceStart == null) ? System.DateTime.MinValue : (DateTime)subscriptionMap.ServiceStart;
                subscription.SubscriptionReferenceId = subscriptionMap.SubscriptionRefId ?? "";
                subscriptions.Add(subscription);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Subscription, "FetchData", (int)watch.ElapsedMilliseconds, subscriptions.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return subscriptions;
        }

        private static bool HasChanged(TbServiceLineSubscriptionMap currentServicePlan, ServiceLine newServiceLine, Subscription subscription)
        {
            return (
                currentServicePlan.ActiveFrom != newServiceLine.ServicePlan.ActiveFrom ||
                currentServicePlan.IsOptedIntoOverage != newServiceLine.ServicePlan.IsOptedIntoOverage ||
                currentServicePlan.OverageDescription != newServiceLine.ServicePlan.OverageDescription ||
                currentServicePlan.OverageName != newServiceLine.ServicePlan.OverageName ||
                currentServicePlan.OverageAmountGb != newServiceLine.ServicePlan.OverageAmountGB ||
                currentServicePlan.OveragePrice != newServiceLine.ServicePlan.OveragePrice ||
                currentServicePlan.PricePerGb != newServiceLine.ServicePlan.PricePerGB ||
                currentServicePlan.UsageLimitGb != (int)newServiceLine.ServicePlan.UsageLimitGB
            );
        }

        public static async Task Save(IConfiguration configuration, ILogger<UsageData> logger, Data.ServiceLines serviceLines, Data.Subscriptions subscriptions)
        {
            if (subscriptions == null || (subscriptions != null && subscriptions.Count() == 0))
                return;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IConfiguration _configuration = configuration;
            ILogger<UsageData> _logger = logger;
            IgenmsContext dbContext;

            string? igenmsConnectionString = _configuration.GetValue<string>("ConnectionStrings:IgenmsContext");
            if (string.IsNullOrEmpty(igenmsConnectionString))
            {
                _logger.LogError("Error processing subscriptions. ConnectionStrings is null");
                return;
            }

            try
            {
                dbContext = new IgenmsContext(igenmsConnectionString);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error processing subscriptions. Can't create instance of IgenmsContext. Error: {error} InnerException: {InnerException}", ex.Message, ex.InnerException);
                return;
            }

            List<TbServiceLineSubscriptionMap> objectsToAdd = new List<TbServiceLineSubscriptionMap>();
            List<TbServiceLineSubscriptionMap> objectsToUpdate = new List<TbServiceLineSubscriptionMap>();
            List<TbServiceLineSubscriptionMap> listOfSubscriptions = await Task.Run(() => dbContext.TbServiceLineSubscriptionMaps.ToList());

            foreach (Subscription subscription in subscriptions)
            {
                try
                {
                    ServiceLine? serviceLine = serviceLines.FirstOrDefault(x => x.ServiceLineNumber == subscription.ServiceLineNumber);
                    if (serviceLine == null)
                    {
                        Helper.Utility.WriteToLogWarning(dbContext, Category.Subscription, "Save", "Error writing to subscriptions. ServiceLineNumber is null. SubscriptionReferenceId: " + subscription.SubscriptionReferenceId);
                        continue;
                    }

                    bool isNew = false;
                    bool hasChanged = false;

                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbServiceLineSubscriptionMap? tbServiceLineSubscriptionMap = listOfSubscriptions.FirstOrDefault(s => s.ServiceLineNumber == subscription.ServiceLineNumber);

                    if (tbServiceLineSubscriptionMap != null)
                    {
                        hasChanged = HasChanged(tbServiceLineSubscriptionMap, serviceLine, subscription);
                    }
                    else
                    {
                        isNew = true;
                        tbServiceLineSubscriptionMap = new TbServiceLineSubscriptionMap();
                        tbServiceLineSubscriptionMap.DateCreated = timestamp;
                    }

                    if (isNew || hasChanged)
                    {
                        tbServiceLineSubscriptionMap.DateUpdated = timestamp;
                        tbServiceLineSubscriptionMap.ProductRefId = subscription.ProductReferenceId;
                        tbServiceLineSubscriptionMap.ServiceEnd = Helper.Utility.GetDateTime(subscription.EndDate);
                        tbServiceLineSubscriptionMap.ServiceLineNumber = subscription.ServiceLineNumber;
                        tbServiceLineSubscriptionMap.ServiceStart = Helper.Utility.GetDateTime(subscription.StartDate);
                        tbServiceLineSubscriptionMap.SubscriptionRefId = subscription.SubscriptionReferenceId;
                        if (serviceLine != null)
                        {
                            tbServiceLineSubscriptionMap.ActiveFrom = Helper.Utility.GetDateTime(serviceLine.ServicePlan.ActiveFrom);
                            tbServiceLineSubscriptionMap.IsOptedIntoOverage = serviceLine.ServicePlan.IsOptedIntoOverage;
                            tbServiceLineSubscriptionMap.OverageDescription = serviceLine.ServicePlan.OverageDescription;
                            tbServiceLineSubscriptionMap.OverageName = serviceLine.ServicePlan.OverageName;
                            tbServiceLineSubscriptionMap.OverageAmountGb = serviceLine.ServicePlan.OverageAmountGB;
                            tbServiceLineSubscriptionMap.OveragePrice = serviceLine.ServicePlan.OveragePrice;
                            tbServiceLineSubscriptionMap.PricePerGb = serviceLine.ServicePlan.PricePerGB;
                            tbServiceLineSubscriptionMap.UsageLimitGb = (int)serviceLine.ServicePlan.UsageLimitGB;
                        }
                        if (isNew)
                        {
                            objectsToAdd.Add(tbServiceLineSubscriptionMap);
                        }
                        else
                        {
                            objectsToUpdate.Add(tbServiceLineSubscriptionMap);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Subscription, "Save", ex, "Error processing subscriptions.  ServiceLineNumber: " + subscription.ServiceLineNumber + " SubscriptionReferenceId: " + subscription.SubscriptionReferenceId);
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Subscription, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Subscription, "Save", ex, "AddRange");
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Subscription, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Subscription, "Save", ex, "UpdateRange");
                }
            }

            watch.Stop(); 
            
            Helper.Utility.WriteToLogActivity(dbContext, Category.Subscription, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task<IG.ENMS.Starlink.Data.Subscriptions> Sync(IConfiguration configuration, ILogger<ServiceData> logger, Data.Subscriptions newData, Data.Subscriptions existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing Subscriptions. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.Subscriptions returnData = newData;

            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (Subscription subscription in newData)
                {
                    string subscriptionReferenceId = subscription.SubscriptionReferenceId;
                    existingData.Remove(subscriptionReferenceId);
                }

                foreach (Subscription subscription in existingData)
                {
                    newData.Add(subscription);
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Subscription, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return returnData;
        }
    }
}
