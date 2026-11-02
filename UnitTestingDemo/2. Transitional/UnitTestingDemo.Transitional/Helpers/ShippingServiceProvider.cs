using System;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Helpers.Interfaces;
using UnitTestingDemo.QueryContexts.Interfaces;

namespace UnitTestingDemo.Helpers
{
    /// <summary>
    /// Class responsible for providing shipping service details based on a specified shipping service code.
    /// </summary>
    internal sealed class ShippingServiceProvider : IShippingServiceProvider
    {
        private readonly IShippingQueryContext _queryContext;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingServiceSelector"/> with the supplied services.
        /// </summary>
        /// <param name="queryContext">
        /// An instance of <see cref="IShippingQueryContext"/> providing access to shipping queries.
        /// This must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="queryContext"/> argument is <c>null</c>.
        /// </exception>
        public ShippingServiceProvider(
            IShippingQueryContext queryContext)
        {
            // Validate argument(s).
            if (queryContext is null) throw new ArgumentNullException(nameof(queryContext));

            // Make these arguments available to the object.
            _queryContext = queryContext;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public async Task<ShippingService> GetShippingService(
            string shippingServiceCode)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(shippingServiceCode)) throw new ArgumentNullException(nameof(shippingServiceCode));

            // Retrieve the shipping service details from the database, and return them.
            var result = await _queryContext.GetShippingService(shippingServiceCode);
            return result;
        }

        #endregion // #region Public methods
    }
}
