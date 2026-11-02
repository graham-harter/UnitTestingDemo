using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestingDemo.Helpers
{
    /// <summary>
    /// Helper methods for the <see cref="string"/> type.
    /// </summary>
    public static class StringHelper
    {
        private const string NullRepresentation = "[null]";

        #region StringValueOrNull(...) methods

        /// <summary>
        /// Returns the supplied <see cref="string"/> value enclosed in quotation marks;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="string"/> value.
        /// </param>
        /// <returns>
        /// The supplied <see cref="string"/> value enclosed in quotation marks;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </returns>
        public static string StringValueOrNull(string value)
        {
            return StringValueOrNullPrivate(value);
        }

        /// <summary>
        /// Returns the supplied <see cref="object"/> value as a <see cref="string"/> enclosed in quotation marks;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="object"/> value.
        /// </param>
        /// <returns>
        /// The supplied <see cref="object"/> value as a <see cref="string"/> enclosed in quotation marks;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </returns>
        public static string StringValueOrNull(object value)
        {
            return StringValueOrNullPrivate(value?.ToString());
        }

        #endregion // #region StringValueOrNull(...) methods

        #region ValueOrNull(...) methods

        /// <summary>
        /// Returns the supplied <see cref="object"/> value as a <see cref="string"/>;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </summary>
        /// <param name="value">
        /// The <see cref="object"/> value.
        /// </param>
        /// <returns>
        /// The supplied <see cref="object"/> value as a <see cref="string"/>;
        /// or else &quot;[null]&quot; if the supplied value is <c>null</c>.
        /// </returns>
        public static string ValueOrNull(object value)
        {
            return ValueOrNullPrivate(value);
        }

        #endregion // #region ValueOrNull(...) methods

        #region Private helper methods

        private static string StringValueOrNullPrivate(string value)
        {
            if (value is null) return NullRepresentation;

            return string.Concat("\"", value, "\"");
        }

        private static string ValueOrNullPrivate(object value)
        {
            if (value is null) return NullRepresentation;

            return value.ToString()!;
        }

        #endregion // #region Private helper methods
    }
}
