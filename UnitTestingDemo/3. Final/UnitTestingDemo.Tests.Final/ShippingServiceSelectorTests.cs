using System;
using System.Text;
using UnitTestingDemo.Helpers;
using UnitTestingDemo.DataClasses;
using UnitTestingDemo.Helpers.Interfaces;
using UnitTestingDemo.Interfaces;
using UnitTestingDemo.Tests.Constants;
using UnitTestingDemo.Tests.TestHelpers;

namespace UnitTestingDemo.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="ShippingServiceSelector"/> class.
    /// </summary>
    internal sealed class ShippingServiceSelectorTests
    {
        private readonly ITimeProvider _sundayTimeProvider;
        private readonly ITimeProvider _mondayTimeProvider;
        private readonly ITimeProvider _tuesdayTimeProvider;
        private readonly ITimeProvider _wednesdayTimeProvider;
        private readonly ITimeProvider _thursdayTimeProvider;
        private readonly ITimeProvider _fridayTimeProvider;
        private readonly ITimeProvider _saturdayTimeProvider;
        private readonly IShippingServiceProvider _testShippingServiceProvider = new TestShippingServiceProvider();
        private readonly IShippingServiceSelector _testShippingServiceSelector;

        #region Constructor(s)

        /// <summary>
        /// Initializes a new instance of <see cref="ShippingServiceSelectorTests"/>.
        /// </summary>
        public ShippingServiceSelectorTests()
        {
            // Set up the time providers for each day of the week.
            _sundayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 11, 15, 33, 37));         // Sunday
            _mondayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 12, 5, 31, 16));          // Monday
            _tuesdayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 13, 10, 50, 17));        // Tuesday
            _wednesdayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 14, 20, 12, 45));      // Wednesday
            _thursdayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 15, 14, 22, 33));       // Thursday
            _fridayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 16, 9, 11, 27));          // Friday
            _saturdayTimeProvider = new TestTimeProvider(new DateTime(2026, 10, 17, 18, 44, 52));       // Saturday

            // Set up the services required by the tests.
            _testShippingServiceSelector = CreateShippingServiceSelector(_testShippingServiceProvider);
        }

        #endregion // #region Constructor(s)

        #region ctor tests

        [Test]
        public void Ctor_ShippingServiceProviderNull_ThrowsExpectedException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ShippingServiceSelector(null!, _sundayTimeProvider));

            Assert.That(exception.ParamName, Is.EqualTo("shippingServiceProvider"));
        }

        [Test]
        public void Ctor_TimeProviderNull_ThrowsExpectedException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ShippingServiceSelector(_testShippingServiceProvider, null!));

            Assert.That(exception.ParamName, Is.EqualTo("timeProvider"));
        }

        [Test]
        public void Ctor_ArgumentsSupplied_InstanceCreated()
        {
            var selector = new ShippingServiceSelector(_testShippingServiceProvider, _sundayTimeProvider);

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
        public async Task SelectShippingService_UnitedKingdomOnSunday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_sundayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 14.19M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnMonday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_mondayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 14.19M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnTuesday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_tuesdayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.RoyalMail2ndClass, "Royal Mail 2nd Class", 5.34M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnWednesday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_wednesdayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 14.19M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnThursday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_thursdayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.RoyalMail1stClass, "Royal Mail 1st Class", 8.90M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnFriday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_fridayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 14.19M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_UnitedKingdomOnSaturday_ReturnsExpectedValue()
        {
            var selector = CreateShippingServiceSelector(_saturdayTimeProvider);

            var selectedShippingService = await selector.SelectShippingService(CountryCode.UnitedKingdom);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.Mercury, "Mercury Careless Shipping", 14.19M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_France_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.France);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 23.99M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_Germany_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Germany);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 23.99M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_Italy_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Italy);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.DhlInternational, "DHL International", 23.99M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        [Test]
        public async Task SelectShippingService_OtherDestinationCountry_ReturnsExpectedValue()
        {
            var selectedShippingService = await _testShippingServiceSelector.SelectShippingService(CountryCode.Sweden);

            var expectedShippingService = new ExpectedShippingService(ShippingServiceCode.UpsInternational, "UPS International", 0.50M);

            Assert.That(MatchesExpectation(selectedShippingService, expectedShippingService));
        }

        #endregion // #region SelectShippingService(...) tests

        #region Private helper methods

        private IShippingServiceSelector CreateShippingServiceSelector(IShippingServiceProvider shippingServiceProvider)
        {
            IShippingServiceSelector shippingServiceSelector = new ShippingServiceSelector(shippingServiceProvider, _sundayTimeProvider);
            return shippingServiceSelector;
        }

        private IShippingServiceSelector CreateShippingServiceSelector(ITimeProvider timeProvider)
        {
            IShippingServiceSelector shippingServiceSelector = new ShippingServiceSelector(_testShippingServiceProvider, timeProvider);
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
