using System;
using System.Collections.ObjectModel;
using UnitTestingDemo.Constants;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Helpers.Interfaces;

namespace UnitTestingDemo.Tests.TestHelpers
{
    /// <summary>
    /// Shipping service provider implementation used to support unit tests.
    /// </summary>
    internal sealed class TestShippingServiceProvider : IShippingServiceProvider
    {
        private readonly IReadOnlyDictionary<string, ShippingService> _shippingServiceLookupByShippingServiceCode;

        #region Constructor(s)

        public TestShippingServiceProvider()
        {
            // Create a lookup table of test shipping services by shipping service code.
            _shippingServiceLookupByShippingServiceCode = CreateShippingServiceLookupByShippingServiceCode();
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public async Task<ShippingService> GetShippingService(
            string shippingServiceCode)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(shippingServiceCode)) throw new ArgumentNullException(nameof(shippingServiceCode));

            // Look up the shipping service by the supplied shipping service code.
            if (!_shippingServiceLookupByShippingServiceCode.TryGetValue(shippingServiceCode, out ShippingService shippingService))
            {
                throw new KeyNotFoundException($"Shipping service with code '{shippingServiceCode}' not found.");
            }

            // Return the looked-up shipping service.
            return shippingService!;
        }

        #endregion // #region Public methods

        #region Private methods

        private static IReadOnlyDictionary<string, ShippingService> CreateShippingServiceLookupByShippingServiceCode()
        {
            IDictionary<string, ShippingService> lookup = new Dictionary<string, ShippingService>(StringComparer.InvariantCulture);
            AddShippingService(lookup, ShippingServiceCode.RoyalMail1stClass, "RepMail1st", "Republican Mail 1st Class", 8.90M);
            AddShippingService(lookup, ShippingServiceCode.RoyalMail2ndClass, "RepMail2nd", "Republican Mail 2nd Class", 5.34M);
            AddShippingService(lookup, ShippingServiceCode.Mercury, "Hermes", "The Winged Messenger", 14.19M);
            AddShippingService(lookup, ShippingServiceCode.DhlInternational, "Securicor", "Secure International Delivery", 23.99M);
            AddShippingService(lookup, ShippingServiceCode.UpsInternational, "Global", "Global Delivery Worldwide!", 0.50M);

            IReadOnlyDictionary<string, ShippingService> result = new ReadOnlyDictionary<string, ShippingService>(lookup);
            return result;
        }

        private static void AddShippingService(
            IDictionary<string, ShippingService> shippingServiceLookupByShippingServiceCode,
            string shippingServiceCodeKey,
            string shippingServiceCodeValue,
            string shippingServiceName,
            decimal shippingChargePoundsSterling)
        {
            // Construct a new shipping service instance based on these values.
            ShippingService shippingService = new ShippingService(
                shippingServiceCodeValue,
                shippingServiceName,
                shippingChargePoundsSterling);

            // Add this to the lookup table using the shipping service code key.
            shippingServiceLookupByShippingServiceCode.Add(
                shippingServiceCodeKey,
                shippingService);
        }

        #endregion // #region Private methods
    }
}
