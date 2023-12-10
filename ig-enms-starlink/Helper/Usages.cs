using EFCore.BulkExtensions;
using IG.ENMS.Starlink.Data;
using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Usages
    {
        private static int Count(DataUsages usages)
        {
            int count = 0;

            foreach (var dataUsage in usages)
            {
                foreach (var usage in dataUsage)
                {
                    count += 1;
                }
            }

            return count;
        }

        public static async Task<List<IG.ENMS.Starlink.Data.DataUsages>> Get(IConfiguration configuration, ILogger<UsageData> logger, Settings settings)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            List<IG.ENMS.Starlink.Data.DataUsages> listOfUsages = new List<DataUsages>();

            if (dbContext == null)
            {
                logger.LogError("Error processing servieLineUsage. Can't create instance of IgenmsContext.");
                return listOfUsages;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            int count = 0;

            IG.ENMS.Starlink.Data.DataUsages billingUsages = new IG.ENMS.Starlink.Data.DataUsages(logger);
            IG.ENMS.Starlink.Data.DataUsages dailyUsages = new IG.ENMS.Starlink.Data.DataUsages(logger);

            List<TbServiceLine> listOfServiceLines = await Task.Run(() => dbContext.TbServiceLines.ToList());

            List<TbServiceLineUsage> listOfServiceLineUsages = await Task.Run(() => dbContext.TbServiceLineUsages
                .Where(x => x.UsageTs > System.DateTime.Today.AddDays(-settings.RetentionPolicy_UsageDataBilling) && x.RecordType == "billingCycle")
                .ToList());

            foreach (TbServiceLine tbServiceLine in listOfServiceLines)
            {
                List<TbServiceLineUsage> matchingObjects = listOfServiceLineUsages.FindAll(x => x.ServiceLineNumber == tbServiceLine.ServiceLineNumber);
                foreach (TbServiceLineUsage matchingObject in matchingObjects)
                {
                    IG.ENMS.Starlink.Models.DataUsage dataUsage = new IG.ENMS.Starlink.Models.DataUsage();
                    dataUsage.DataBucketId = matchingObject.UsageBucketId;
                    dataUsage.StartDate = matchingObject.UsageTs;
                    dataUsage.EndDate = matchingObject.UsageTs.AddMonths(+1);
                    if (matchingObject.TotalGb.HasValue)
                    {
                        dataUsage.TotalGB = (double)matchingObject.TotalGb;
                    }
                    dataUsage.ServiceLineNumber = matchingObject.ServiceLineNumber;

                    billingUsages.Add(dataUsage);
                }
            }
            listOfUsages.Add(billingUsages);

            listOfServiceLineUsages = await Task.Run(() => dbContext.TbServiceLineUsages
                .Where(x => x.UsageTs > System.DateTime.Today.AddDays(-settings.RetentionPolicy_UsageDataDaily) && x.RecordType == "daily")
                .ToList());

            foreach (TbServiceLine tbServiceLine in listOfServiceLines)
            {
                List<TbServiceLineUsage> matchingObjects = listOfServiceLineUsages.FindAll(x => x.ServiceLineNumber == tbServiceLine.ServiceLineNumber);
                foreach (TbServiceLineUsage matchingObject in matchingObjects)
                {
                    IG.ENMS.Starlink.Models.DataUsage dataUsage = new IG.ENMS.Starlink.Models.DataUsage();
                    dataUsage.DataBucketId = matchingObject.UsageBucketId;
                    dataUsage.EndDate = matchingObject.UsageTs;
                    dataUsage.StartDate = matchingObject.UsageTs;
                    if (matchingObject.TotalGb.HasValue)
                    {
                        dataUsage.TotalGB = (double)matchingObject.TotalGb;
                    }
                    dataUsage.ServiceLineNumber = matchingObject.ServiceLineNumber;

                    dailyUsages.Add(dataUsage);
                }
            }
            listOfUsages.Add(dailyUsages);

            count += Count(billingUsages);
            count += Count(dailyUsages);

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Usage, "FetchData", (int)watch.ElapsedMilliseconds, count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return listOfUsages;
        }

        public static async Task Save(IConfiguration configuration, ILogger<UsageData> logger, DataUsages billingCycles, DataUsages dailyDataUsages)
        {
            if ((dailyDataUsages == null || (dailyDataUsages != null && dailyDataUsages.Count() == 0)) ||
                (billingCycles == null || (billingCycles != null && billingCycles.Count() == 0)))
                return;

            IConfiguration _configuration = configuration;
            ILogger<UsageData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing servieLineUsage. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbServiceLineUsage> objectsToAdd = new List<TbServiceLineUsage>();
            List<TbServiceLineUsage> objectsToUpdate = new List<TbServiceLineUsage>();
            List<TbServiceLineUsage> listOfUsages = await Task.Run(() => dbContext.TbServiceLineUsages.ToList());

            if (dailyDataUsages != null && dailyDataUsages.Count() > 0)
            {
                foreach (var dailyDataUsage in dailyDataUsages)
                {
                    foreach (var dailyUsage in dailyDataUsage)
                    {
                        DataUsage dataUsage = dailyUsage.Value;
                        try
                        {
                            bool isNew = false;
                            bool hasChanged = false;

                            DateTime timestamp = Helper.Utility.GetDateTime();
                            TbServiceLineUsage? tbUsage = listOfUsages.FirstOrDefault(x => x.ServiceLineNumber == dataUsage.ServiceLineNumber && x.UsageTs == dataUsage.StartDate && x.RecordType == "daily" && x.UsageBucketId == dataUsage.DataBucketId);

                            if (tbUsage != null)
                            {
                                hasChanged = ((tbUsage.TotalGb != dataUsage.TotalGB));
                            }
                            else
                            {
                                isNew = true;
                                tbUsage = new TbServiceLineUsage();
                                tbUsage.DateCreated = timestamp;
                                tbUsage.RecordType = "daily";
                                tbUsage.ServiceLineNumber = dataUsage.ServiceLineNumber;
                                tbUsage.UsageBucketId = dataUsage.DataBucketId;
                                tbUsage.UsageTs = dataUsage.StartDate;
                            }

                            if (isNew || hasChanged)
                            {
                                tbUsage.DateUpdated = timestamp;
                                tbUsage.TotalGb = dataUsage.TotalGB;
                                if (isNew)
                                {
                                    objectsToAdd.Add(tbUsage);
                                }
                                else
                                {
                                    objectsToUpdate.Add(tbUsage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "Error processing servieLineUsage.  ServiceLineNumber: " + dataUsage.ServiceLineNumber);
                        }
                    }
                }
            }

            if (billingCycles != null && billingCycles.Count() > 0)
            {
                foreach (var billingDataUsage in billingCycles)
                {
                    foreach (var billingUsage in billingDataUsage)
                    {
                        DataUsage dataUsage = billingUsage.Value;
                        try
                        {
                            bool isNew = false;
                            bool hasChanged = false;

                            DateTime timestamp = Helper.Utility.GetDateTime();
                            TbServiceLineUsage? tbUsage = listOfUsages.FirstOrDefault(x => x.ServiceLineNumber == dataUsage.ServiceLineNumber && x.UsageTs == dataUsage.StartDate && x.RecordType == "billingCycle" && x.UsageBucketId == dataUsage.DataBucketId);

                            if (tbUsage != null)
                            {
                                hasChanged = ((tbUsage.TotalGb != dataUsage.TotalGB));
                            }
                            else
                            {
                                isNew = true;
                                tbUsage = new TbServiceLineUsage();
                                tbUsage.DateCreated = timestamp;
                                tbUsage.RecordType = "billingCycle";
                                tbUsage.ServiceLineNumber = dataUsage.ServiceLineNumber;
                                tbUsage.UsageBucketId = dataUsage.DataBucketId;
                                tbUsage.UsageTs = dataUsage.StartDate;
                            }

                            if (isNew || hasChanged)
                            {
                                tbUsage.DateUpdated = timestamp;
                                tbUsage.TotalGb = dataUsage.TotalGB;
                                if (isNew)
                                {
                                    objectsToAdd.Add(tbUsage);
                                }
                                else
                                {
                                    objectsToUpdate.Add(tbUsage);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "Error processing servieLineUsage.  ServiceLineNumber: " + dataUsage.ServiceLineNumber);
                        }
                    }
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "AddRange");
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "UpdateRange");
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Usage, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task Save(IConfiguration configuration, string fileName, List<DataUsage> dataUsages, Usage usageType)
        {
            IConfiguration _configuration = configuration;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbServiceLineUsage> datas = new List<TbServiceLineUsage>();

            foreach (DataUsage dataUsage in dataUsages)
            {
                try
                {
                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbServiceLineUsage tbUsage = new TbServiceLineUsage();
                    tbUsage.DateCreated = timestamp;
                    tbUsage.RecordType = usageType.ToString();
                    tbUsage.ServiceLineNumber = dataUsage.ServiceLineNumber;
                    tbUsage.UsageBucketId = dataUsage.DataBucketId;
                    tbUsage.UsageTs = dataUsage.StartDate;
                    tbUsage.DateUpdated = timestamp;
                    tbUsage.TotalGb = dataUsage.TotalGB;
                    datas.Add(tbUsage);
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Usage, "Save", ex, "Error processing servieLineUsage.  ServiceLineNumber: " + dataUsage.ServiceLineNumber);
                }
            }

            try
            {
                dbContext.BulkInsertOrUpdate(datas);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "JsaonFile", ex, "BulkInsertOrUpdate");
            }
            catch (Exception ex)
            {
                Helper.Utility.WriteToLogError(dbContext, Category.Telemetry, "JsaonFile", ex, "BulkInsertOrUpdate");
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Usage, "JsaonFile", fileName, (int)watch.ElapsedMilliseconds, datas.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return;
        }

        public static async Task<List<IG.ENMS.Starlink.Data.DataUsages>> Sync(IConfiguration configuration, ILogger<UsageData> logger, Settings settings,
            Data.DataUsages newBillingUsages, Data.DataUsages existingBillingUsages,
            Data.DataUsages newDailyUsages, Data.DataUsages existingDailyUsages)
        {
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            List<IG.ENMS.Starlink.Data.DataUsages> listOfUsages = new List<DataUsages>();

            if (dbContext == null)
            {
                logger.LogError("Error processing servieLineUsage. Can't create instance of IgenmsContext.");
                return listOfUsages;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            int count = 0;

            IG.ENMS.Starlink.Data.DataUsages billingUsages = SyncUsages(newBillingUsages, existingBillingUsages);
            count += Count(billingUsages);

            IG.ENMS.Starlink.Data.DataUsages dailyUsages = SyncUsages(newDailyUsages, existingDailyUsages);
            count += Count(dailyUsages);

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Usage, "Sync", (int)watch.ElapsedMilliseconds, count);

            if (settings.NextCleanup_UsageData <= System.DateTime.Now)
            {
                watch = System.Diagnostics.Stopwatch.StartNew();

                count = 0;

                foreach (var item in billingUsages)
                {
                    int xcount = item.Count();
                    foreach (var s in item.Where(kv => (System.DateTime.Now - kv.Key).Days > settings.RetentionPolicy_UsageDataBilling).ToList())
                    {
                        item.Remove(s.Key);
                    }
                    xcount -= item.Count();
                    count += xcount;
                }

                foreach (var item in dailyUsages)
                {
                    int xcount = item.Count();
                    foreach (var s in item.Where(kv => (System.DateTime.Now - kv.Key).Days > settings.RetentionPolicy_UsageDataDaily).ToList())
                    {
                        item.Remove(s.Key);
                    }
                    xcount -= item.Count();
                    count += xcount;
                }

                watch.Stop();

                Helper.Utility.WriteToLogActivity(dbContext, Category.Usage, "CleanUP", (int)watch.ElapsedMilliseconds, count);
            }

            listOfUsages.Add(billingUsages);
            listOfUsages.Add(dailyUsages);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return listOfUsages;
        }

        private static IG.ENMS.Starlink.Data.DataUsages SyncUsages(Data.DataUsages newUsages, Data.DataUsages existingUsages)
        {
            List<string> keys = new List<string>(newUsages.GetDataUsageList().Keys);

            foreach (string key in keys)
            {
                if (existingUsages.GetDataUsageList().ContainsKey(key))
                {
                    var existingUsage = existingUsages.GetDataUsageList()[key];
                    if (newUsages.GetDataUsageList().ContainsKey(key))
                    {
                        var newUsage = newUsages.GetDataUsageList()[key];
                        foreach (var billingUsage in newUsage)
                        {
                            DataUsage dataUsage = billingUsage.Value;
                            DateTime dictionaryKey = Helper.Usages.GetKey(dataUsage);
                            if (existingUsage.ContainsKey(dictionaryKey))
                            {
                                existingUsage[dictionaryKey] = newUsage[dictionaryKey];
                            }
                            else
                            {
                                existingUsage.Add(dictionaryKey, dataUsage);
                            }
                        }
                    }
                }
                else
                {
                    var newUsage = newUsages.GetDataUsageList()[key];
                    existingUsages.GetDataUsageList().Add(key, newUsage);
                }
            }

            return existingUsages;
        }

        public static DateTime GetKey(DataUsage dataUsage)
        {
            return dataUsage.StartDate.AddSeconds(dataUsage.DataBucketId);
        }
    }
}
