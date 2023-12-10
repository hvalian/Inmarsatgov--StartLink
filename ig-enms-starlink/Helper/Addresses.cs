using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Addresses
    {
        public static async Task<IG.ENMS.Starlink.Data.Addresses> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            IG.ENMS.Starlink.Data.Addresses addresses = new IG.ENMS.Starlink.Data.Addresses(_logger);

            if (dbContext == null)
            {
                logger.LogError("Error processing addresses. Can't create instance of IgenmsContext.");
                return addresses;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbAddress> listOfAddresses = await Task.Run(() => dbContext.TbAddresses.ToList());

            foreach (TbAddress tbAddress in listOfAddresses)
            {
                IG.ENMS.Starlink.Models.Address address = new IG.ENMS.Starlink.Models.Address();
                address.AdministrativeArea = tbAddress.AdministrativeArea ?? "";
                address.AdministrativeAreaCode = tbAddress.AdministrativeAreaCode ?? "";
                address.AddressLines = tbAddress.AddressLines.Split(',').ToList();
                address.AddressReferenceId = tbAddress.AddressReferenceId;
                address.FormattedAddress = tbAddress.FormattedAddress ?? "";
                address.Latitude = Convert.ToDouble(tbAddress.Latitude);
                address.Locality = tbAddress.Locality ?? "";
                address.Longitude = Convert.ToDouble(tbAddress.Longitude);
                address.PostalCode = tbAddress.PostalCode ?? "";
                address.Region = tbAddress.Region ?? "";
                address.RegionCode = tbAddress.RegionCode ?? "";
                addresses.Add(address);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Address, "FetchData", (int)watch.ElapsedMilliseconds, addresses.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return addresses;
        }

        private static bool HasChanged(TbAddress currentAddress, Address newAddress)
        {
            string address = string.Join(",", newAddress.AddressLines);
            address = string.IsNullOrEmpty(address) ? "No address in file" : address;
            return (
            currentAddress.AddressLines != address ||
            currentAddress.Locality != newAddress.Locality ||
            currentAddress.AdministrativeArea != newAddress.AdministrativeArea ||
            currentAddress.AdministrativeAreaCode != newAddress.AdministrativeAreaCode ||
            currentAddress.Region != newAddress.Region ||
            currentAddress.RegionCode != newAddress.RegionCode ||
            currentAddress.PostalCode != newAddress.PostalCode ||
            currentAddress.FormattedAddress != newAddress.FormattedAddress ||
            currentAddress.Latitude != newAddress.Latitude.ToString() ||
            currentAddress.Longitude != newAddress.Longitude.ToString()
            );
        }

        public static async Task Save(IConfiguration configuration, ILogger<ServiceData> logger, Data.Addresses addresses)
        {
            if (addresses == null || (addresses != null && addresses.Count() == 0))
                return;

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing addresses. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbAddress> objectsToAdd = new List<TbAddress>();
            List<TbAddress> objectsToUpdate = new List<TbAddress>();
            List<TbAddress> listOfAddresses = await Task.Run(() => dbContext.TbAddresses.ToList());

            foreach (Address address in addresses)
            {
                try
                {
                    bool isNew = false;
                    bool hasChanged = false;
                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbAddress? tbAddress = listOfAddresses.FirstOrDefault(a => a.AddressReferenceId == address.AddressReferenceId);
                    if (tbAddress != null)
                    {
                        hasChanged = HasChanged(tbAddress, address);
                    }
                    else
                    {
                        isNew = true;
                        tbAddress = new TbAddress();
                        tbAddress.DateCreated = timestamp;
                    }
                    if (isNew || hasChanged)
                    {
                        string addr = string.Join(",", address.AddressLines);
                        tbAddress.AdministrativeArea = address.AdministrativeArea;
                        tbAddress.AdministrativeAreaCode = address.AdministrativeAreaCode;
                        tbAddress.AddressLines = string.IsNullOrEmpty(addr) ? "No address in file" : addr;
                        tbAddress.AddressReferenceId = address.AddressReferenceId;
                        tbAddress.DateUpdated = timestamp;
                        tbAddress.FormattedAddress = address.FormattedAddress;
                        tbAddress.Latitude = address.Latitude.ToString();
                        tbAddress.Locality = address.Locality;
                        tbAddress.Longitude = address.Longitude.ToString();
                        tbAddress.PostalCode = address.PostalCode;
                        tbAddress.Region = address.Region;
                        tbAddress.RegionCode = address.RegionCode;
                        if (isNew)
                        {
                            objectsToAdd.Add(tbAddress);
                        }
                        else
                        {
                            objectsToUpdate.Add(tbAddress);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Address, "Save", ex, "Error processing addresses.  AddressReferenceId: " + address.AddressReferenceId);
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Address, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Address, "Save", ex, "AddRange");
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Address, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Address, "Save", ex, "UpdateRange");
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Address, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);

            dbContext.Database.CloseConnection();

            await Task.Delay(1);
        }

        public static async Task<IG.ENMS.Starlink.Data.Addresses> Sync(IConfiguration configuration, ILogger<ServiceData> logger, Data.Addresses newData, Data.Addresses existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing Addresses. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.Addresses returnData = newData;

            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (Address address in newData)
                {
                    string addressReferenceId = address.AddressReferenceId;
                    existingData.Remove(addressReferenceId);
                }

                foreach (Address address in existingData)
                {
                    newData.Add(address);
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Address, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return returnData;
        }
    }
}