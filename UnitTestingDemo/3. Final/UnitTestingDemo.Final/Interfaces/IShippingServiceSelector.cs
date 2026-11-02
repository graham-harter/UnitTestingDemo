using System;
using UnitTestingDemo.DataClasses;

namespace UnitTestingDemo.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for selecting a shipping service and providing its shipping charge.
    /// </summary>
    public interface IShippingServiceSelector
    {
        #region Methods

        /// <summary>
        /// Select the shipping service to use based on the supplied values.
        /// </summary>
        /// <param name="destinationCountryCode">
        /// The destination country code. This must not be <c>null</c>, empty or merely whitespace.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ShippingService"/> containing the details of the selected shipping service, including its shipping charge.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="destinationCountryCode"/> argument is <c>null</c>, empty or merely whitespace.
        /// </exception>
        Task<ShippingService> SelectShippingService(
            string destinationCountryCode);

        #endregion // #region Methods
    }
}
