using System;
using System.Collections.Generic;
using System.Text;
using UnitTestingDemo.Helpers;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Factories;
using UnitTestingDemo.Factories.Interfaces;
using UnitTestingDemo.Interfaces;
using UnitTestingDemo.QueryContexts;
using UnitTestingDemo.QueryContexts.Interfaces;
using UnitTestingDemo.Tests.Constants;

namespace UnitTestingDemo.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="ShippingServiceSelector"/> class.
    /// </summary>
    internal sealed class ShippingServiceSelectorTests
    {
        private readonly IShippingQueryContext _shippingQueryContext;
        private readonly IShippingServiceSelector _testShippingServiceSelector;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingServiceSelectorTests"/>.
        /// </summary>
        public ShippingServiceSelectorTests()
        {
            // Set up the services required by the tests.
            _shippingQueryContext = CreateShippingQueryContext();
            _testShippingServiceSelector = CreateShippingServiceSelector(_shippingQueryContext);
        }

        #endregion // #region Constructor(s)

        #region ctor tests

        [Test]
        public void Ctor_ShippingQueryContextNull_ThrowsExpectedException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ShippingServiceSelector(null!));

            Assert.That(exception.ParamName, Is.EqualTo("shippingQueryContext"));
        }

        [Test]
        public void Ctor_ShippingQueryContextSupplied_InstanceCreated()
        {
            var selector = new ShippingServiceSelector(_shippingQueryContext);

            Assert.That(selector, Is.Not.Null);
            Assert.That(selector, Is.InstanceOf<IShippingServiceSelector>());
        }

        #endregion // #region ctor tests

        #region SelectShippingService(...) tests

        [Test]
        public async Task SelectShippingService_DestinationCountryCodeNull_ThrowsExpectedException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _testShippingServiceSelector.SelectShippingService(null!));

            Assert.That(exception.ParamName, Is.EqualTo("destinationCountryCode"));
        }

        [Test]
        public async Task SelectShippingService_DestinationCountryCodeEmpty_ThrowsExpectedException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _testShippingServiceSelector.SelectShippingService(string.Empty));

            Assert.That(exception.ParamName, Is.EqualTo("destinationCountryCode"));
        }

        [Test]
        public async Task SelectShippingService_DestinationCountryCodeMerelyWhitespace_ThrowsExpectedException()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await _testShippingServiceSelector.SelectShippingService(" \t\t  \n  "));

            Assert.That(exception.ParamName, Is.EqualTo("destinationCountryCode"));
        }

        [Test]
        public async Task SelectShippingService_DestinationCountryCodeProvided_ReturnsResult()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Italy);

            Assert.That(selectedShippingService, Is.Not.Null);
            Assert.That(selectedShippingService, Is.InstanceOf<ShippingService>());
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdom_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 3.67M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_France_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.France);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 15.64M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_Germany_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Germany);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 15.64M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_Italy_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Italy);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 15.64M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_OtherDestinationCountry_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Sweden);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.UpsInternational, "UPS International", 29.11M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        #endregion // #region SelectShippingService(...) tests

        #region Private helper methods

        private static IShippingQueryContext CreateShippingQueryContext()
        {
            IFusionConnectionFactory connectionFactory = new FusionConnectionFactory();

            IShippingQueryContext shippingQueryContext = new ShippingQueryContext(connectionFactory);

            return shippingQueryContext;
        }

        private static IShippingServiceSelector CreateShippingServiceSelector(IShippingQueryContext shippingQueryContext)
        {
            IShippingServiceSelector shippingServiceSelector = new ShippingServiceSelector(shippingQueryContext);
            return shippingServiceSelector;
        }

        private static bool MatchesExpectation(
            ShippingService actual,
            ExpectedShippingService expected)
        {
            // Return false if the shipping service codes don't match.
            if (!string.Equals(actual.ShippingServiceCode, expected.ShippingSeviceCode, StringComparison.InvariantCulture)) return false;

            // Return false if the shipping charges don't match.
            if (actual.ShippingChargePoundsSterling != expected.ShippingChargePoundsSterling) return false;

            // Otherwise, the actual shipping service matches the expected shipping service.
            return true;
        }

        #endregion // #region Private helper methods

        #region Private class ExpectedShippingService

        private sealed class ExpectedShippingService
        {
            public ExpectedShippingService(
                string shippingSeviceCode,
                string shippingServiceName,
                decimal shippingChargePoundsSterling)
            {
                // Validate arguments.
                if (string.IsNullOrWhiteSpace(shippingSeviceCode)) throw new ArgumentNullException(nameof(shippingSeviceCode));
                if (string.IsNullOrWhiteSpace(shippingServiceName)) throw new ArgumentNullException(nameof(shippingServiceName));

                // Make these arguments available to the object.
                ShippingSeviceCode = shippingSeviceCode;
                ShippingServiceName = shippingServiceName;
                ShippingChargePoundsSterling = shippingChargePoundsSterling;
            }

            public string ShippingSeviceCode { get; }

            public string ShippingServiceName { get; }

            public decimal ShippingChargePoundsSterling { get; }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(GetType().Name);
                sb.Append(" {");

                sb.Append($"{nameof(ShippingSeviceCode)} = {StringHelper.StringValueOrNull(ShippingSeviceCode)}");

                sb.Append(", ");
                sb.Append($"{nameof(ShippingServiceName)} = {StringHelper.StringValueOrNull(ShippingServiceName)}");

                sb.Append(", ");
                sb.Append($"{nameof(ShippingChargePoundsSterling)} = {CurrencyHelper.FormatSterlingValue(ShippingChargePoundsSterling)}");

                sb.Append("}");
                return sb.ToString();
            }
        }

        #endregion // #region Private class ExpectedShippingService
    }
}
