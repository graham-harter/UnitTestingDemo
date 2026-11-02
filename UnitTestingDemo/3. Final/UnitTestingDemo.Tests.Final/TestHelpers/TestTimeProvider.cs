using System;
using UnitTestingDemo.Helpers.Interfaces;

namespace UnitTestingDemo.Tests.TestHelpers
{
    /// <summary>
    /// Time provider implementation used to support unit tests.
    /// </summary>
    internal sealed class TestTimeProvider : ITimeProvider
    {
        private readonly DateTime _fixedDateTime;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="TestTimeProvider"/> with the supplied fixed date/time.
        /// </summary>
        /// <param name="fixedDateTime">
        /// The fixed date/time to be returned by the <see cref="Now"/> property.
        /// </param>
        public TestTimeProvider(
            DateTime fixedDateTime)
        {
            // Make these arguments available to the object.
            _fixedDateTime = fixedDateTime;
        }

        #endregion // #region Constructor(s)

        #region Public properties

        public DateTime Now => _fixedDateTime;

        #endregion // #region Public properties
    }
}
