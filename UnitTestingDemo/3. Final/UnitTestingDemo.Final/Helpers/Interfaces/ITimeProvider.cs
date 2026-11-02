using System;

namespace UnitTestingDemo.Helpers.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for providing the current time to the application.
    /// </summary>
    public interface ITimeProvider
    {
        #region Properties

        /// <summary>
        /// Gets the current date/time.
        /// </summary>
        DateTime Now { get; }

        #endregion // #region Properties
    }
}
