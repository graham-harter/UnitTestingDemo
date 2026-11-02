using System;
using UnitTestingDemo.DataClasses;

namespace UnitTestingDemo.Helpers.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for providing shipping service details based on a specified shipping service code.
    /// </summary>
    public interface IShippingServiceProvider
    {
        #region Methods

        /// <summary>
        /// Get the shipping service details for the specified shipping service code.
        /// </summary>
        /// <param name="shippingServiceCode">
        /// The shipping service code of the shipping service whose details to retrieve.
        /// This must not be <c>null</c>, empty or merely whitespace.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ShippingService"/> containing the details of the specified shipping service.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="shippingServiceCode"/> argument is <c>null</c>, empty or merely whitespace.
        /// </exception>
        Task<ShippingService> GetShippingService(
            string shippingServiceCode);

        #endregion // #region Methods
    }
}
