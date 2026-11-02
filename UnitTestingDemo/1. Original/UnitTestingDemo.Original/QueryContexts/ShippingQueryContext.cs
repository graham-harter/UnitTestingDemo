using System;
using System.Data;
using System.Text;
using UnitTestingDemo.Helpers;
using Dapper;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Factories.Interfaces;
using UnitTestingDemo.QueryContexts.Interfaces;

namespace UnitTestingDemo.QueryContexts
{
    /// <summary>
    /// Class responsible for executing database queries relating to shipping.
    /// </summary>
    internal sealed class ShippingQueryContext : IShippingQueryContext
    {
        private readonly IFusionConnectionFactory _connectionFactory;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingQueryContext"/> with the supplied services.
        /// </summary>
        /// <param name="connectionFactory">
        /// An instance of <see cref="IFusionConnectionFactory"/> providing connections to the Fusion database.
        /// This must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="connectionFactory"/> argument is <c>null</c>.
        /// </exception>
        public ShippingQueryContext(
            IFusionConnectionFactory connectionFactory)
        {
            // Validate argument(s).
            if (connectionFactory is null) throw new ArgumentNullException(nameof(connectionFactory));

            // Make these arguments available to the object.
            _connectionFactory = connectionFactory;
        }

        #endregion // #region Constructor(s)

        #region Public methods

        public async Task<ShippingService> GetShippingService(
            string shippingServiceCode)
        {
            // Validate argument(s).
            if (string.IsNullOrWhiteSpace(shippingServiceCode)) throw new ArgumentNullException(nameof(shippingServiceCode));

            using (var connection = _connectionFactory.Create())
            {
                connection.Open();

                // Create stored procedure parameters.
                var parameters = new DynamicParameters();
                parameters.Add("@ShippingServiceCode", shippingServiceCode, DbType.String, size: 8);

                // Execute query to retrieve the shipping service details.
                ShippingServiceCollector collectedShippingService = await connection.QueryFirstAsync<ShippingServiceCollector>(
@"SELECT
		sp.ShippingServiceCode
	,	sp.ShippingServiceName
	,	sp.ShippingPricePoundsSterling AS [ShippingChargePoundsSterling]
FROM [UnitTestDemoShippingPricing] AS sp
WHERE
	sp.ShippingServiceCode = @ShippingServiceCode
;",
                    parameters,
                    commandType: CommandType.Text);

                // Convert the retrieved shipping service to the return type, and return it.
                var result = new ShippingService(
                    collectedShippingService.ShippingServiceCode,
                    collectedShippingService.ShippingServiceName,
                    collectedShippingService.ShippingChargePoundsSterling);

                return result;
            }
        }

        #endregion // #region Public methods

        #region Private class ShippingServiceCollector

        private sealed class ShippingServiceCollector
        {
            public required string ShippingServiceCode { get; set; }

            public required string ShippingServiceName { get; set; }

            public decimal ShippingChargePoundsSterling { get; set; }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetType().Name);
                sb.Append(" {");

                sb.Append($"{nameof(ShippingServiceCode)} = {StringHelper.StringValueOrNull(ShippingServiceCode)}");

                sb.Append(", ");
                sb.Append($"{nameof(ShippingServiceName)} = {StringHelper.StringValueOrNull(ShippingServiceName)}");

                sb.Append(", ");
                sb.Append($"{nameof(ShippingChargePoundsSterling)} = {CurrencyHelper.FormatSterlingValue(ShippingChargePoundsSterling)}");

                sb.Append("}");
                return sb.ToString();
            }
        }

        #endregion // #region Private class ShippingServiceCollector
    }
}
