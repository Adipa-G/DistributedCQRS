using System.Linq;
using System.Reflection;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Test.TestData;
using NUnit.Framework;

namespace DistCqrs.Core.Test.Resolve
{
    [TestFixture]
    public class ResolveUtilsTest
    {
        [Test]
        public void GivenEntityCommandAndHandler_WhenGetCommandToEntityMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetCommandToEntityMappings(
                new[] {typeof(ResolveUtilsTest).GetTypeInfo().Assembly});

            Assert.AreEqual(typeof(Account),mappings[typeof(CreateAccountCommand)]);
            Assert.AreEqual(typeof(Account),mappings[typeof(UpdateAccountBalanceCommand)]);
        }

        [Test]
        public void GivenEntityCommandAndHandler_WhenGetCommandHandlerMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetCommandHandlerMappings(
                new[] { typeof(ResolveUtilsTest).GetTypeInfo().Assembly });

            Assert.IsTrue(mappings.Any(
                m => m.Item1 == typeof(Account) &&
                     m.Item2 == typeof(CreateAccountCommand) &&
                     m.Item3 == typeof(CreateAccountCommandHandler)));

            Assert.IsTrue(mappings.Any(
                m => m.Item1 == typeof(Account) &&
                     m.Item2 == typeof(UpdateAccountBalanceCommand) &&
                     m.Item3 == typeof(UpdateAccountBalanceCommandHandler)));
        }

        [Test]
        public void GivenEntityCommandAndHandler_WhenGetEventHandlerMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetEventHandlerMappings(
                new[] { typeof(ResolveUtilsTest).GetTypeInfo().Assembly });

            Assert.IsTrue(mappings.Any(
                m => m.Item1 == typeof(Account) &&
                     m.Item2 == typeof(AccountCreatedEvent) &&
                     m.Item3 == typeof(AccountCreatedEventHandler)));

            Assert.IsTrue(mappings.Any(
                m => m.Item1 == typeof(Account) &&
                     m.Item2 == typeof(AccountBalanceUpdatedEvent) &&
                     m.Item3 == typeof(AccountBalanceUpdatedEventHandler)));
        }
    }
}
