using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Services.Pollers;
using IG.ENMS.Starlink.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Accounts
    {
        public static async Task<IG.ENMS.Starlink.Data.Accounts> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgcmsContext? customerDbContext = Helper.Utility.GetIgCMSContext(configuration);
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            IG.ENMS.Starlink.Data.Accounts accounts = new IG.ENMS.Starlink.Data.Accounts(_logger);

            if (customerDbContext == null || dbContext == null)
            {
                logger.LogError("Error processing accounts. Can't create instance of IgenmsContext.");
                return accounts;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbCustomer> listOfCustomers = await Task.Run(() => customerDbContext.TbCustomers
                .Where(c => c.ExternalId != null)
                .ToList());

            var result = dbContext.TbServiceLines.ToList()
                .Join(dbContext.TbAddresses.ToList(),
                serviceLine => serviceLine.AddressReferenceId,
                address => address.AddressReferenceId,
                (serviceLine, address) => new
                {
                    AccountNumber = serviceLine.AccountNumber ?? "",
                    RegionCode = address.RegionCode ?? ""
                })
                .GroupBy(x => x.AccountNumber)
                .Select(grp => grp.First())
                .ToList();

            var customers = listOfCustomers
                .Join(result,
                customer => customer.ExternalId,
                result => result.AccountNumber,
                (customer, result) => new
                {
                    AccountNumber = customer.ExternalId ?? "",
                    AccountName = customer.Name ?? "",
                    RegionCode = result.RegionCode ?? ""
                }).ToList();


            foreach (var item in customers)
            {
                Account account = new Account();
                account.AccountNumber = item.AccountNumber;
                account.AccountName = item.AccountName;
                account.RegionCode = item.RegionCode;
                accounts.Add(account);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Account, "FetchData", (int)watch.ElapsedMilliseconds, accounts.Count());

            customerDbContext.Database.CloseConnection();
            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return accounts;
        }

        private static bool HasChanged(TbCustomer currentCustomer, Account newCustomer)
        {
            return (currentCustomer.Name != newCustomer.AccountName);
        }

        public static async Task Save(IConfiguration configuration, ILogger<ServiceData> logger, Data.Accounts accounts)
        {
            if (accounts == null || (accounts != null && accounts.Count() == 0))
                return;

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgcmsContext? customerDbContext = Helper.Utility.GetIgCMSContext(configuration);
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (customerDbContext == null || dbContext == null)
            {
                logger.LogError("Error processing accounts. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbCustomer> objectsToAdd = new List<TbCustomer>();
            List<TbCustomer> objectsToUpdate = new List<TbCustomer>();
            List<TbCustomer> listOfAccounts = await Task.Run(() => customerDbContext.TbCustomers.ToList());

            foreach (Account account in accounts)
            {
                try
                {
                    bool isNew = false;
                    bool hasChanged = false;
                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbCustomer? tbCustomer = listOfAccounts.FirstOrDefault(x => x.ExternalId == account.AccountNumber);

                    if (tbCustomer != null)
                    {
                        hasChanged = HasChanged(tbCustomer, account);
                    }
                    else
                    {
                        isNew = true;
                        tbCustomer = new TbCustomer();
                        tbCustomer.CreatedBy = Helper.Utility.GetCreatedBy();
                        tbCustomer.DateCreated = timestamp;
                        tbCustomer.ExternalId = account.AccountNumber;
                        tbCustomer.IsDeleted = 0;
                        tbCustomer.SegAid = "TBD";
                        tbCustomer.ServiceNowId = "TBD";
                    }

                    if (isNew || hasChanged)
                    {
                        tbCustomer.DateUpdated = timestamp;
                        tbCustomer.Name = account.AccountName;
                        tbCustomer.UpdatedBy = Helper.Utility.GetCreatedBy();
                    }

                    if (isNew)
                    {
                        objectsToAdd.Add(tbCustomer);
                    }
                    if (hasChanged)
                    {
                        objectsToUpdate.Add(tbCustomer);
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Account, "Save", ex, "Error processing accounts.  ExternalId: " + account.AccountNumber);
                }
            }

            if (objectsToAdd.Count > 0)
            {
                try
                {
                    customerDbContext.AddRange(objectsToAdd);
                    customerDbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Account, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Account, "Save", ex, "AddRange");
                }
            }

            if (objectsToUpdate.Count > 0)
            {
                try
                {
                    customerDbContext.UpdateRange(objectsToUpdate);
                    customerDbContext.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Account, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Account, "Save", ex, "UpdateRange");
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Account, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            customerDbContext.Database.CloseConnection();
            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task<IG.ENMS.Starlink.Data.Accounts> Sync(IConfiguration configuration, ILogger<ServiceData> logger, IG.ENMS.Starlink.Data.Accounts newData, IG.ENMS.Starlink.Data.Accounts existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing accounts. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.Accounts returnData = newData;

            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (Account account in newData)
                {
                    string accountNumber = account.AccountNumber;
                    existingData.Remove(accountNumber);
                }

                foreach (Account account in existingData)
                {
                    newData.Add(account);
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Account, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return returnData;
        }
    }
}
