using System;
using System.Globalization;

namespace UnitTestingDemo.Helpers
{
    /// <summary>
    /// Helper methods for formatting currency values as <see cref="string"/> values.
    /// </summary>
    public static class CurrencyHelper
    {
        private const string SterlingCurrencySymbol = "£";

        private static readonly NumberFormatInfo _sterlingCurrencyFormat;

        #region Static constructor

        static CurrencyHelper()
        {
            // Set up the Sterling currency format info.
            var sterlingCurrencyFormat = (NumberFormatInfo)CultureInfo.InvariantCulture.NumberFormat.Clone();
            sterlingCurrencyFormat.CurrencySymbol = SterlingCurrencySymbol;
            sterlingCurrencyFormat.CurrencyNegativePattern = 1;     // e.g., -£1.00

            _sterlingCurrencyFormat = sterlingCurrencyFormat;
        }

        #endregion // #region Static constructor

        #region Public methods

        /// <summary>
        /// Format the supplied value as a Sterling currency value, with the specified number of decimal places.
        /// </summary>
        /// <param name="value">
        /// The value to be formatted as a Sterling currency value.
        /// </param>
        /// <param name="numberOfDecimalPlaces">
        /// (Optional) The number of decimal places to include in the formatted output. The default is 2.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> representation of the supplied value as a Sterling currency value, with the specified number of decimal places.
        /// </returns>
        public static string FormatSterlingValue(decimal value, int numberOfDecimalPlaces = 2)
        {
            var formatString = CreateFormatString(numberOfDecimalPlaces);
            var result = value.ToString(formatString, _sterlingCurrencyFormat);
            return result;
        }

        /// <summary>
        /// Format the supplied value as a Sterling currency value, with the specified number of decimal places.
        /// </summary>
        /// <param name="value">
        /// The value to be formatted as a Sterling currency value.
        /// </param>
        /// <param name="numberOfDecimalPlaces">
        /// (Optional) The number of decimal places to include in the formatted output. The default is 2.
        /// </param>
        /// <returns>
        /// A <see cref="string"/> representation of the supplied value as a Sterling currency value, with the specified number of decimal places.
        /// </returns>
        public static string FormatSterlingValue(double value, int numberOfDecimalPlaces = 2)
        {
            var formatString = CreateFormatString(numberOfDecimalPlaces);
            var result = value.ToString(formatString, _sterlingCurrencyFormat);
            return result;
        }

        #endregion // #region Public methods

        #region Private methods

        private static string CreateFormatString(int numberOfDecimalPlaces)
        {
            return string.Concat("C", numberOfDecimalPlaces.ToString(CultureInfo.InvariantCulture));
        }

        #endregion // #region Private methods
    }
}
