using System;
using UnitTestingDemo.Constants;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Interfaces;
using UnitTestingDemo.QueryContexts.Interfaces;

namespace UnitTestingDemo
{
    /// <summary>
    /// Class responsible for selecting a shipping service and providing its shipping charge.
    /// </summary>
    internal sealed class ShippingServiceSelector : IShippingServiceSelector
    {
        private readonly IShippingQueryContext _shippingQueryContext;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingServiceSelector"/> with the supplied services.
        /// </summary>
        /// <param name="shippingQueryContext">
        /// An instance of <see cref="IShippingQueryContext"/> providing access to shipping queries.
        /// This must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="shippingQueryContext"/> argument is <c>null</c>.
        /// </exception>
        public ShippingServiceSelector(
            IShippingQueryContext shippingQueryContext)
        {
            // Validate argument(s).
            if (shippingQueryContext is null) throw new ArgumentNullException(nameof(shippingQueryContext));

            // Make these arguments available to the object.
            _shippingQueryContext = shippingQueryContext;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public async Task<ShippingService> SelectShippingService(
            string destinationCountryCode)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(destinationCountryCode)) throw new ArgumentNullException(nameof(destinationCountryCode));

            // Select the shipping service code based on our rules.
            var shippingServiceCode = SelectShippingServiceCode(destinationCountryCode);

            // Retrieve the shipping service details for the selected shipping service.
            var shippingService = await _shippingQueryContext.GetShippingService(shippingServiceCode);

            return shippingService;
        }

        #endregion // #region Public methods

        #region Private methods

        private string SelectShippingServiceCode(
            string destinationCountryCode)
        {
            // 1. Rules for domestic services.
            if (IsDomestic(destinationCountryCode))
            {
                // Check the current day of the week.
                var dayOfWeek = GetDayOfWeek();
                switch (dayOfWeek)
                {
                    case DayOfWeek.Thursday:
                        return ShippingServiceCode.RoyalMail1stClass;

                    case DayOfWeek.Tuesday:
                        return ShippingServiceCode.RoyalMail2ndClass;

                    default:
                        return ShippingServiceCode.Mercury;
                }
            }

            // 2. Rules for international services.
            if (IsDhlTerritory(destinationCountryCode))
            {
                return ShippingServiceCode.DhlInternational;
            }

            return ShippingServiceCode.UpsInternational;
        }

        private DayOfWeek GetDayOfWeek()
        {
            return DateTime.Now.DayOfWeek;
        }

        private static bool IsDomestic(
            string destinationCountryCode)
        {
            return destinationCountryCode == CountryCode.UnitedKingdom;
        }

        private static bool IsDhlTerritory(
            string destinationCountryCode)
        {
            bool result;
            switch (destinationCountryCode)
            {
                case CountryCode.France:
                case CountryCode.Germany:
                case CountryCode.Italy:
                    result = true;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }

        #endregion // #region Private methods
    }
}
