using System.Linq;
using System.Reflection;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.Resolve.Helpers;
using DistCqrs.Core.Test.TestData;
using NUnit.Framework;

namespace DistCqrs.Core.Test.Resolve
{
    [TestFixture]
    public class ResolveUtilsTest
    {
        [Test]
        public void
            GivenEntityCommandAndHandler_WhenGetCommandToEntityMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetCommandToEntityMappings(
                new[] {typeof(ResolveUtilsTest).GetTypeInfo().Assembly});

            Assert.AreEqual(typeof(Account),
                mappings
                    .Single(m => m.CommandType == typeof(CreateAccountCommand))
                    .EntityType);

            Assert.AreEqual(typeof(Account),
                mappings
                    .Single(m => m.CommandType ==
                                 typeof(UpdateAccountBalanceCommand))
                    .EntityType);
        }

        [Test]
        public void
            GivenEntityCommandAndHandler_WhenGetCommandHandlerMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetCommandHandlerMappings(
                new[] {typeof(ResolveUtilsTest).GetTypeInfo().Assembly});

            Assert.IsTrue(mappings.Any(
                m => m.EntityType == typeof(Account) &&
                     m.CommandType == typeof(CreateAccountCommand) &&
                     m.CommandHandlerType ==
                     typeof(CreateAccountCommandHandler)));

            Assert.IsTrue(mappings.Any(
                m => m.EntityType == typeof(Account) &&
                     m.CommandType == typeof(UpdateAccountBalanceCommand) &&
                     m.CommandHandlerType ==
                     typeof(UpdateAccountBalanceCommandHandler)));
        }

        [Test]
        public void
            GivenEntityCommandAndHandler_WhenGetEventHandlerMappings_ThenReturn()
        {
            var mappings = ResolveUtils.GetEventHandlerMappings(
                new[] {typeof(ResolveUtilsTest).GetTypeInfo().Assembly});

            Assert.IsTrue(mappings.Any(
                m => m.EntityType == typeof(Account) &&
                     m.EventType == typeof(AccountCreatedEvent) &&
                     m.EventHandlerType == typeof(AccountCreatedEventHandler)));

            Assert.IsTrue(mappings.Any(
                m => m.EntityType == typeof(Account) &&
                     m.EventType == typeof(AccountBalanceUpdatedEvent) &&
                     m.EventHandlerType == typeof(AccountBalanceUpdatedEventHandler)));
        }

        [Test]
        public void GivenClassWithDependecyAttribute_WhenGetDependencies_ThenReturn()
        {
            var dependencies = ResolveUtils.GetDependencies(
                new[] {typeof(ResolveUtilsTest).GetTypeInfo().Assembly});

            Assert.IsTrue(dependencies.Any(
                d => d.Interface == typeof(IEventStore) &&
                     d.Implementation == typeof(InMemoryEventStore)));
        }
    }
}