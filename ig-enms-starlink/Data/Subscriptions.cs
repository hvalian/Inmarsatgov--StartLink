// File Name: Subscriptions.cs
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
	public class Subscriptions : IEnumerable<Subscription>
	{
		private readonly ILogger _logger;
		private Dictionary<string, Subscription> _subscriptionList = new Dictionary<string, Subscription>();

		public Subscriptions(ILogger Logger)
		{
			_logger = Logger;
		}

		public bool Clear()
		{
			try {
				_subscriptionList.Clear();
				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error clearing subscription list dictionary. Error: {errorMessage}", _Ex.Message);
				return false;
			}
		}

		public bool Add(Subscription subscription)
		{
			_logger.LogDebug("Entering Add(Subscription) with argument {subscription}.", subscription);
			try {
				if (_subscriptionList.ContainsKey(subscription.SubscriptionReferenceId)) {
					_logger.LogDebug("Subscription {subscriptionReferenceId} already exists.  Replacing it with new data.", subscription.SubscriptionReferenceId);
					_subscriptionList[subscription.SubscriptionReferenceId] = subscription;
					return true;
				}

				_subscriptionList.Add(subscription.SubscriptionReferenceId, subscription);

				_logger.LogDebug("Subscription {subscription} added successfully.", subscription);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding subscription. {subscription}. Error: {errorMessage}", subscription, _Ex.Message);

				return false;
			}
		}

		public bool Add(string SubscriptionReferenceId, string ServiceLineNumber, string Description, string ProductReferenceId, DateTime StartDate, DateTime NormalizedStartDate, DateTime EndDate, DateTime ServiceEndDate, double DelayedProductId, double OptInProductId)
		{
			_logger.LogDebug("Entering Add(Subscription) with arguments  SubscriptionReferenceId: {SubscriptionReferenceId}, ServiceLineNumber:{ServiceLineNumber}, Description: {Description}, ProductReferenceId: {ProductReferenceId}, StartDate: {StartDate}, NormalizedStartDate: {NormalizedStartDate},EndDate: {EndDate},ServiceEndDate: {ServiceEndDate}, DelayedProductId: {DelayedProductId}, OptInProductId: {OptInProductId}.", SubscriptionReferenceId, ServiceLineNumber, Description, ProductReferenceId, StartDate, NormalizedStartDate, EndDate, ServiceEndDate, DelayedProductId, OptInProductId);

			try {
				if (_subscriptionList.ContainsKey(SubscriptionReferenceId)) {
					_logger.LogDebug("Subscription {SubscriptionReferenceId} already exists.  Replacing it with new data.", SubscriptionReferenceId);

					_subscriptionList[SubscriptionReferenceId] = new Subscription() {
						SubscriptionReferenceId = SubscriptionReferenceId, ServiceLineNumber = ServiceLineNumber, Description = Description, ProductReferenceId = ProductReferenceId, StartDate = StartDate, NormalizedStartDate = NormalizedStartDate, EndDate = EndDate, ServiceEndDate = ServiceEndDate
					};
					return true;
				}

				_subscriptionList.Add(SubscriptionReferenceId, new Subscription() {
					SubscriptionReferenceId = SubscriptionReferenceId, ServiceLineNumber = ServiceLineNumber, Description = Description, ProductReferenceId = ProductReferenceId, StartDate = StartDate, NormalizedStartDate = NormalizedStartDate, EndDate = EndDate, ServiceEndDate = ServiceEndDate
				});

				_logger.LogDebug("Subscription for SubscriptionReferenceId: {SubscriptionReferenceId}, ServiceLineNumber:{ServiceLineNumber}, Description: {Description}, ProductReferenceId: {ProductReferenceId}, StartDate: {StartDate}, NormalizedStartDate: {NormalizedStartDate},EndDate: {EndDate},ServiceEndDate: {ServiceEndDate}, DelayedProductId: {DelayedProductId}, OptInProductId: {OptInProductId} added successfully.", SubscriptionReferenceId, ServiceLineNumber, Description, ProductReferenceId, StartDate, NormalizedStartDate, EndDate, ServiceEndDate, DelayedProductId, OptInProductId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error adding subscription with arguments SubscriptionReferenceId: {SubscriptionReferenceId}, ServiceLineNumber:{ServiceLineNumber}, Description: {Description}, ProductReferenceId: {ProductReferenceId}, StartDate: {StartDate}, NormalizedStartDate: {NormalizedStartDate},EndDate: {EndDate},ServiceEndDate: {ServiceEndDate}, DelayedProductId: {DelayedProductId}, OptInProductId: {OptInProductId}. Error {errorMessage}", SubscriptionReferenceId, ServiceLineNumber, Description, ProductReferenceId, StartDate, NormalizedStartDate, EndDate, ServiceEndDate, DelayedProductId, OptInProductId, _Ex.Message);

				return false;
			}
		}

		public bool Remove(Subscription subscription)
		{
			try {
				if (_subscriptionList.ContainsKey(subscription.SubscriptionReferenceId))
					_subscriptionList.Remove(subscription.SubscriptionReferenceId);
				else
					_logger.LogWarning("Subscription {subscriptionNumber} does not exist to be removed.", subscription.SubscriptionReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing subscription. {subscription}. Error: {errorMessage}", subscription, _Ex.Message);

				return false;
			}
		}

		public bool Remove(string SubscriptionReferenceId)
		{
			try {
				if (_subscriptionList.ContainsKey(SubscriptionReferenceId))
					_subscriptionList.Remove(SubscriptionReferenceId);
				else
					_logger.LogWarning("Subscription {subscriptionReferenceId} does not exist to be removed.", SubscriptionReferenceId);

				return true;
			} catch (Exception _Ex) {
				_logger.LogError("Error removing subscription. {subscriptionReferenceId}. Error: {errorMessage}", SubscriptionReferenceId, _Ex.Message);

				return false;
			}
		}

		public Subscription Get (string SubscriptionReferenceId)
		{
			if (_subscriptionList.ContainsKey(SubscriptionReferenceId))
				return _subscriptionList[SubscriptionReferenceId];
			else
				return null;
		}

		IEnumerator<Subscription> IEnumerable<Subscription>.GetEnumerator()
		{
			return _subscriptionList.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _subscriptionList.GetEnumerator();
		}
	}
}
