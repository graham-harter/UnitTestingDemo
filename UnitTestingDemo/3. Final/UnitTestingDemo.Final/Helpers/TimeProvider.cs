using System;
using UnitTestingDemo.Helpers.Interfaces;

namespace UnitTestingDemo.Helpers
{
    /// <summary>
    /// Class responsible for providing the current time to the application.
    /// </summary>
    internal sealed class TimeProvider : ITimeProvider
    {
        #region Public properties

        public DateTime Now => DateTime.Now;

        #endregion // #region Public properties
    }
}
