using System;
using Microsoft.Data.SqlClient;

namespace UnitTestingDemo.Factories.Interfaces
{
    /// <summary>
    /// Interface implemented by classes responsible for providing SQL connections to the Fusion database
    /// to the application.
    /// </summary>
    public interface IFusionConnectionFactory
    {
        #region Methods

        /// <summary>
        /// Create a SQL connection to the Fusion database.
        /// </summary>
        /// <returns>
        /// A new instance of <see cref="SqlConnection"/> for the Fusion database.
        /// </returns>
        SqlConnection Create();

        #endregion // #region Methods
    }
}
