using System;
using UnitTestingDemo.Constants;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Helpers.Interfaces;
using UnitTestingDemo.Interfaces;

namespace UnitTestingDemo
{
    /// <summary>
    /// Class responsible for selecting a shipping service and providing its shipping charge.
    /// </summary>
    internal sealed class ShippingServiceSelector : IShippingServiceSelector
    {
        private readonly IShippingServiceProvider _shippingServiceProvider;
        private readonly ITimeProvider _timeProvider;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingServiceSelector"/> with the supplied services.
        /// </summary>
        /// <param name="shippingServiceProvider">
        /// An instance of <see cref="IShippingServiceProvider"/> providing access to shipping service details.
        /// This must not be <c>null</c>.
        /// </param>
        /// <param name="timeProvider">
        /// An instance of <see cref="ITimeProvider"/> providing access to the current time.
        /// This must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="shippingServiceProvider"/> argument is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="timeProvider"/> argument is <c>null</c>.
        /// </exception>
        public ShippingServiceSelector(
            IShippingServiceProvider shippingServiceProvider,
            ITimeProvider timeProvider)
        {
            // Validate argument(s).
            if (shippingServiceProvider is null) throw new ArgumentNullException(nameof(shippingServiceProvider));
            if (timeProvider is null) throw new ArgumentNullException(nameof(timeProvider));

            // Make these arguments available to the object.
            _shippingServiceProvider = shippingServiceProvider;
            _timeProvider = timeProvider;
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
            var shippingService = await _shippingServiceProvider.GetShippingService(shippingServiceCode);

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
            return _timeProvider.Now.DayOfWeek;
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
