using IG.ENMS.Models.Starlink.NMS;
using IG.ENMS.Starlink.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace IG.ENMS.Starlink.Common
{
    public class UserAccountMapService
    {
        //protected readonly Config _cfg;
        readonly IConfiguration _configuration;
        //protected readonly IAppSettings _appSettings;
        protected readonly IMemoryCache _cache;
        //AppSettings _appSettings = new AppSettings();
        public UserAccountMapService(IConfiguration configuration, IMemoryCache cache)
        {

            _configuration = configuration;
            _cache = cache;
        }
 
        
        /// <summary>
        /// This method retrives user account mapping data in cache and if not availbale, then retrieve from database.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public  List<UserCustomerTerminalMap> GetAccountByUser()
        {

            var key = $"AccountService";
            int cacheExpirationMinutes = _configuration.GetValue<int>("AppSettings:CacheSettings:UserAccountRefreshMinutes");
            //var cacheExpirationMinutes = 5;
            List<UserCustomerTerminalMap> account = null;

            if (_cache != null && _cache.Get(key) != null)
            {
                account = _cache.Get(key) as List<UserCustomerTerminalMap>;
            }
            if (account == null)
            {
                account =  GetAccountsByUserNameFromDb();
               _cache.Set(key, account, TimeSpan.FromMinutes(cacheExpirationMinutes));
            }
            return account;
        }

        private  List<UserCustomerTerminalMap> GetAccountsByUserNameFromDb()
        {
            UserCustomerTerminalMap account = null;
            List<UserCustomerTerminalMap> accountMap = new List<UserCustomerTerminalMap>();

            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(_configuration);

            if (dbContext == null)
            {
                throw new Exception($"Can't create instance of IgenmsContext.");
            }

            List<VwGetAccountsByUserName> userAccounts = dbContext.VwGetAccountsByUserNames.ToList();

            foreach (VwGetAccountsByUserName userAccount in userAccounts)
            {
                account = new UserCustomerTerminalMap();

                account.userId = userAccount.UserId;
                account.userName = userAccount.UserName;
                account.Internal = userAccount.Internal;
                account.customerId = userAccount.CustomerId;
                account.accountName = userAccount.CustomerName ?? "";
                account.accountNumber = userAccount.AccountNumber ?? "";
                account.nodeId = (userAccount.NodeId == null) ? 0 : (int)userAccount.NodeId;
                account.terminalID = userAccount.TerminalId ?? "";
                accountMap.Add(account);
            }

            dbContext.Database.CloseConnection();

            if (accountMap == null)
            {
                throw new Exception($"No data found.");
            }

            return accountMap;
        }
    }
}
