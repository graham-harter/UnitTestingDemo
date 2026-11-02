using System;
using System.Text;
using UnitTestingDemo.Helpers;

namespace UnitTestingDemo.DataClasses
{
    /// <summary>
    /// Provides shipping-related operations.
    /// </summary>
    public sealed class ShippingService
    {
        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingService"/> with the supplied values.
        /// </summary>
        /// <param name="shippingServiceCode">
        /// The shipping service code. This must not be <c>null</c>, empty or merely whitespace.
        /// </param>
        /// <param name="shippingServiceName">
        /// The shipping service name. This must not be <c>null</c>, empty or merely whitespace.
        /// </param>
        /// <param name="shippingChargePoundsSterling">
        /// The shipping charge, in pounds Sterling, for this shipping service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="shippingServiceCode"/> argument is <c>null</c>, empty or merely whitespace.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// If the <paramref name="shippingServiceName"/> argument is <c>null</c>, empty or merely whitespace.
        /// </exception>
        public ShippingService(
            string shippingServiceCode,
            string shippingServiceName,
            decimal shippingChargePoundsSterling)
        {
            // Validate arguments.
            if (string.IsNullOrWhiteSpace(shippingServiceCode)) throw new ArgumentNullException(nameof(shippingServiceCode));
            if (string.IsNullOrWhiteSpace(shippingServiceName)) throw new ArgumentNullException(nameof(shippingServiceName));

            // Make these arguments available to the object.
            ShippingServiceCode = shippingServiceCode.Trim();
            ShippingServiceName = shippingServiceName.Trim();
            ShippingChargePoundsSterling = shippingChargePoundsSterling;
        }

        #endregion // #region Constructor(s)

        #region Public properties

        /// <summary>
        /// Gets the shipping service code.
        /// </summary>
        public string ShippingServiceCode { get; }

        /// <summary>
        /// Gets the shipping service name.
        /// </summary>
        public string ShippingServiceName { get; }

        /// <summary>
        /// Gets the shipping charge, in pounds Sterling, for this shipping service.
        /// </summary>
        public decimal ShippingChargePoundsSterling { get; }

        #endregion // #region Public properties

        #region ToString() override

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

        #endregion // #region ToString() override
    }
}
