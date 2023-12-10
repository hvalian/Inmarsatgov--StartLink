using IG.ENMS.Starlink.Models;
using IG.ENMS.Starlink.Models.DB;
using IG.ENMS.Starlink.Models.Enums;
using IG.ENMS.Starlink.Services.Pollers;
using Microsoft.EntityFrameworkCore;

namespace IG.ENMS.Starlink.Helper
{
    public class Products
    {
        public static async Task<IG.ENMS.Starlink.Data.Products> Get(IConfiguration configuration, ILogger<ServiceData> logger)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            IG.ENMS.Starlink.Data.Products products = new IG.ENMS.Starlink.Data.Products(_logger);

            if (dbContext == null)
            {
                logger.LogError("Error processing products. Can't create instance of IgenmsContext.");
                return products;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbProduct> listOfProducts = await Task.Run(() => dbContext.TbProducts.ToList());

            foreach (TbProduct tbProduct in listOfProducts)
            {
                Product product = new Product();
                product.Name = tbProduct.Name ?? "";
                product.Price = Convert.ToDouble(tbProduct.Price);
                product.ISOCurrencyCode = tbProduct.PriceCurrency ?? "";
                product.ProductReferenceId = tbProduct.ProductReferenceId;
                products.Add(product);
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Product, "FetchData", (int)watch.ElapsedMilliseconds, products.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return products;
        }

        private static bool HasChanged(TbProduct currentProduct, Product newProduct)
        {
            return (
                currentProduct.Name != newProduct.Name ||
                currentProduct.Price != newProduct.Price ||
                currentProduct.PriceCurrency != newProduct.ISOCurrencyCode
            );
        }

        public static async Task Save(IConfiguration configuration, ILogger<ServiceData> logger, Data.Products products)
        {
            if (products == null || (products != null && products.Count() == 0))
                return;

            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing products. Can't create instance of IgenmsContext.");
                return;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            List<TbProduct> objectsToAdd = new List<TbProduct>();
            List<TbProduct> objectsToUpdate = new List<TbProduct>();
            List<TbProduct> listOfProducts = await Task.Run(() => dbContext.TbProducts.ToList());

            foreach (Product product in products)
            {
                try
                {
                    bool isNew = false;
                    bool hasChanged = false;

                    DateTime timestamp = Helper.Utility.GetDateTime();
                    TbProduct? tbProduct = listOfProducts.FirstOrDefault(p => p.ProductReferenceId == product.ProductReferenceId);

                    if (tbProduct != null)
                    {
                        hasChanged = HasChanged(tbProduct, product);
                    }
                    else
                    {
                        isNew = true;
                        tbProduct = new TbProduct();
                        tbProduct.DateCreated = timestamp;
                    }

                    if (isNew || hasChanged)
                    {
                        tbProduct.DateUpdated = timestamp;
                        tbProduct.Name = product.Name;
                        tbProduct.Price = product.Price;
                        tbProduct.PriceCurrency = product.ISOCurrencyCode;
                        tbProduct.ProductReferenceId = product.ProductReferenceId;
                        if (isNew)
                        {
                            objectsToAdd.Add(tbProduct);
                        }
                        else
                        {
                            objectsToUpdate.Add(tbProduct);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Product, "Save", ex, "Error processing products.  ProductReferenceId: " + product.ProductReferenceId);
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Product, "Save", ex, "AddRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Product, "Save", ex, "AddRange");
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
                    Helper.Utility.WriteToLogError(dbContext, Category.Product, "Save", ex, "UpdateRange");
                }
                catch (Exception ex)
                {
                    Helper.Utility.WriteToLogError(dbContext, Category.Product, "Save", ex, "UpdateRange");
                }
            }

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Product, "Save", (int)watch.ElapsedMilliseconds, objectsToAdd.Count + objectsToUpdate.Count);
        }

        public static async Task<IG.ENMS.Starlink.Data.Products> Sync(IConfiguration configuration, ILogger<ServiceData> logger, Data.Products newData, Data.Products existingData)
        {
            IConfiguration _configuration = configuration;
            ILogger<ServiceData> _logger = logger;
            IgenmsContext? dbContext = Helper.Utility.GetIgENMSContext(configuration);

            if (dbContext == null)
            {
                logger.LogError("Error processing Products. Can't create instance of IgenmsContext.");
                return newData;
            }

            var watch = System.Diagnostics.Stopwatch.StartNew();

            IG.ENMS.Starlink.Data.Products returnData = newData;

            if (newData.Count() == 0 && existingData.Count() > 0)
            {
                returnData = existingData;
            }
            else if (newData.Count() > 0 && existingData.Count() > 0)
            {
                foreach (Product product in newData)
                {
                    string productReferenceId = product.ProductReferenceId;
                    existingData.Remove(productReferenceId);
                }

                foreach (Product product in existingData)
                {
                    newData.Add(product);
                }
            }

            watch.Stop();

            Helper.Utility.WriteToLogActivity(dbContext, Category.Product, "Sync", (int)watch.ElapsedMilliseconds, newData.Count());

            dbContext.Database.CloseConnection();

            await Task.Delay(1);

            return returnData;
        }
    }
}