# UnitTestingDemo
A demo. of how to construct unit test suites that are robust and useful, without dependencies on external conditions

The purpose of this solution is to demonstrate how unit test suites can be written in a way that is robust and useful.

The key class (the one being subjected to unit tests) is the 'ShippingServiceSelector' class,
which is responsible for providing the correct shipping service (with its shipping charge) based on a set of rules.

The rules are as follow:—

1. If the order is destined for the UK, then select the "Mercury" shipping service. (Just don't expect your package to arrive in one piece!)
2. However, because Royal Mail have recently stopped doing daily collections, we want to ship by Royal Mail only on certain days of the week.
     So, if today is a Tuesday, then select the "Royal Mail 2nd Class" shipping service instead.
3. And if today is a Thursday, select the "Royal Mail 1st Class" shipping service.
4. For all orders destined for France, Germany or Italy, use the "DHL International" shipping service.
5. And for all other countries, use the "UPS International" shipping service.

The unit test suite, in UnitTestingDemo.Tests\ShippingServiceSelectorTests, verifies that the ShippingServiceSelector class behaves correctly for all of the above rules.
