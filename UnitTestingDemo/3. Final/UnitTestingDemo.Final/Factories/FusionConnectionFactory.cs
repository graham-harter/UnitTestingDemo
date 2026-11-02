using System;
using Microsoft.Data.SqlClient;
using UnitTestingDemo.Factories.Interfaces;

namespace UnitTestingDemo.Factories
{
    /// <summary>
    /// Class responsible for providing SQL connections to the Fusion database to the application.
    /// </summary>
    internal sealed class FusionConnectionFactory : IFusionConnectionFactory
    {
        #region Public methods

        public SqlConnection Create()
        {
            string connectionString = "Data Source=AREDEVSQL1\\AREDEVSQL1;Initial Catalog=NCSDatabase;Persist Security Info=True;User ID=ncs;Password=ncsdatabase;MultipleActiveResultSets=True;TrustServerCertificate=True;";

            return new SqlConnection(connectionString);
        }

        #endregion // #region Public methods
    }
}
