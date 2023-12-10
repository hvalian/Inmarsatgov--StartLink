// File Name: Products.cs
// Author: rameshvishnubhatla
// Date Created: 8/11/2023
//
//
using System;
using System.Collections;
using System.Security.Principal;
using IG.ENMS.Starlink.Models;

namespace IG.ENMS.Starlink.Data
{
	public class Products : IEnumerable<Product>
	{
		private readonly ILogger _logger;
		private Dictionary<string, Product> _productList = new Dictionary<string, Product>();

		public Products(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Clear()
		{
			try {
				_productList.Clear();
				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error clearing product list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(Product product)
		{
			_logger.LogDebug("Entering Add(Product) with argument {product}.", product);
			try {
				if (_productList.ContainsKey(product.ProductReferenceId)) {
					_logger.LogDebug("Product {productReferenceId} already exists.  Replacing it with new data.", product.ProductReferenceId);
					_productList[product.ProductReferenceId] = product;
					return true;
				}

				_productList.Add(product.ProductReferenceId, product);

				_logger.LogDebug("Product {product} added successfully.", product);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding product. {product}. Error: {errorMessage}", product, _Ex.Message);

				return false;
			}
		}

		public bool Add(string ProductReferenceId, string Name, double Price, string ISOCurrency)
		{
			_logger.LogDebug("Entering Add(Product) with arguments ProductReferenceId:{pddressReferenceId}, Name:{name}, Price:{price}, ISOCurrency:{iSOCurrency}.", ProductReferenceId, Name, Price, ISOCurrency);

			try {
				if (_productList.ContainsKey(ProductReferenceId)) {
					_logger.LogDebug("Product {productReferenceId} already exists.  Replacing it with new data.", ProductReferenceId);

					_productList[ProductReferenceId] = new Product() { ProductReferenceId = ProductReferenceId, Name = Name, Price = Price, ISOCurrencyCode = ISOCurrency };
					return true;
				}

				_productList.Add(ProductReferenceId, new Product() { ProductReferenceId = ProductReferenceId, Name = Name, Price = Price, ISOCurrencyCode = ISOCurrency });

				_logger.LogDebug("Product for ProductReferenceId:{pddressReferenceId}, Name:{name}, Price:{price}, ISOCurrency:{iSOCurrency} added successfully.", ProductReferenceId, Name, Price, ISOCurrency);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding product with arguments ProductReferenceId:{pddressReferenceId}, Name:{name}, Price:{price}, ISOCurrency:{iSOCurrency}.  Error: {errorMessage}", ProductReferenceId, Name, Price, ISOCurrency, _Ex.Message);

				return false;
			}
		}

		public bool Remove(Product product)
		{
			try {
				if (_productList.ContainsKey(product.ProductReferenceId))
					_productList.Remove(product.ProductReferenceId);
				else
					_logger.LogWarning("Product {productReferenceId} does not exist to be removed.", product.ProductReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing product. {product}. Error: {errorMessage}", product, _Ex.Message);

				return false;
			}
		}

		public bool Remove(string ProductReferenceId)
		{
			try {
				if (_productList.ContainsKey(ProductReferenceId))
					_productList.Remove(ProductReferenceId);
				else
					_logger.LogWarning("Product {productReferenceId} does not exist to be removed.", ProductReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing product. {productReferenceId}. Error: {errorMessage}", ProductReferenceId, _Ex.Message);

				return false;
			}
		}

		public Product Get (string ProductReferenceId)
		{
			if (_productList.ContainsKey(ProductReferenceId))
				return _productList[ProductReferenceId];
			else
				return null;
		}

		IEnumerator<Product> IEnumerable<Product>.GetEnumerator()
		{
			return _productList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _productList.GetEnumerator();
		}
	}
}
