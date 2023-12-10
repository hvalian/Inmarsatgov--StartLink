using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Globalization;

namespace IG.ENMS.Starlink.Helper
{
    public class Utility
    {
        public static string createdBy = "ENMS CSSC Dataload";
        public static DateTime defaultDate = Convert.ToDateTime("1/1/1753 12:00:00 AM", new CultureInfo("en-US"));

        public static string GetCreatedBy()
        {
            return createdBy;
        }

        public static DateTime GetDateTime()
        {
            return (Debugger.IsAttached) ? DateTime.Now.ToUniversalTime() : System.DateTime.Now;
        }

        public static DateTime? GetDateTime(DateTime value)
        {
            return (value != null && value >= defaultDate) ? value : null;
        }

        public static IgcmsContext? GetIgCMSContext(IConfiguration configuration)
        {
            string? igcmsConnectionString = configuration.GetValue<string>("ConnectionStrings:IgcmsContext");
            if (string.IsNullOrEmpty(igcmsConnectionString))
            {
                return null;
            }

            try
            {
                return new IgcmsContext(igcmsConnectionString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static IgenmsContext? GetIgENMSContext(IConfiguration configuration)
        {
            string? igenmsConnectionString = configuration.GetValue<string>("ConnectionStrings:IgenmsContext");
            if (string.IsNullOrEmpty(igenmsConnectionString))
            {
                return null;
            }

            try
            {
                return new IgenmsContext(igenmsConnectionString);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static DateTime GetNextCleanupValue(DateTime value, double interval)
        {
            return (value > System.DateTime.Now) ? value : value.AddHours(interval);
        }

        public static void WriteToLogActivity(IgenmsContext dbContext, Category category, string methodName, int elapsedTime, int affectedRows)
        {
            TbLog logObj = new TbLog();
            logObj.AffectedRows = affectedRows;
            logObj.Category = category.ToString();
            logObj.ElapsedTime = elapsedTime;
            logObj.LogType = Log.Activity.ToString();
            logObj.MethodName = methodName;
            logObj.Timestamp = DateTime.Now;
            dbContext.Attach(logObj).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public static void WriteToLogActivity(IgenmsContext dbContext, Category category, string methodName, string message, int elapsedTime, int affectedRows)
        {
            TbLog logObj = new TbLog();
            logObj.AffectedRows = affectedRows;
            logObj.Category = category.ToString();
            logObj.ElapsedTime = elapsedTime;
            logObj.LogType = Log.Activity.ToString();
            logObj.MethodName = methodName;
            logObj.Message = message;
            logObj.Timestamp = DateTime.Now;
            dbContext.Attach(logObj).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public static void WriteToLogError(IgenmsContext dbContext, Category category, string methodName, Exception ex, string additionalInformation)
        {
            TbLog logObj = new TbLog();
            logObj.Category = category.ToString();
            logObj.LogType = Log.Error.ToString();
            logObj.MethodName = methodName;
            logObj.Timestamp = DateTime.Now;
            if (ex != null)
            {
                logObj.ExceptionMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    logObj.InnerException = ex.InnerException.ToString();
                }
                logObj.StackTrace = ex.StackTrace;
            }
            dbContext.Attach(logObj).State = EntityState.Added;
            dbContext.SaveChanges();
        }

        public static void WriteToLogWarning(IgenmsContext dbContext, Category category, string methodName, string additionalInformation)
        {
            TbLog logObj = new TbLog();
            logObj.Category = category.ToString();
            logObj.LogType = Log.Warning.ToString();
            logObj.Message = additionalInformation;
            logObj.MethodName = methodName;
            logObj.Timestamp = DateTime.Now;
            dbContext.Attach(logObj).State = EntityState.Added;
            dbContext.SaveChanges();
        }
    }
}
