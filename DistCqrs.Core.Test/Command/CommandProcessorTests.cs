using System;
using System.Collections.Generic;
using DistCqrs.Core.Command;
using DistCqrs.Core.Command.Impl;
using DistCqrs.Core.DependencyInjection;
using DistCqrs.Core.Domain;
using DistCqrs.Core.EventStore;
using DistCqrs.Core.Exceptions;
using DistCqrs.Core.Test.TestData;
using NSubstitute;
using NUnit.Framework;

namespace DistCqrs.Core.Test.Command
{
    [TestFixture]
    public class CommandProcessorTests
    {
        private IRootFactory rootFactory;
        private IServiceLocator serviceLocator;
        private IEventStore eventStore;

        [SetUp]
        public void Init()
        {
            rootFactory = Substitute.For<IRootFactory>();
            serviceLocator = Substitute.For<IServiceLocator>();
            eventStore = Substitute.For<IEventStore>();
        }

        [Test]
        public void GivenNoCommandHandlerRegistered_WhenProcess_ThenThrowException()
        {
            serviceLocator.ResolveCommandHandler(Arg.Any<ICommand>())
                .Returns((ICommandHandler<ICommand>)null);

            var sut = CreateSut();

            Assert.That(() => sut.Process(new CreateAccountCommand()),
                Throws.TypeOf<ServiceLocationException>());
        }

        [Test]
        public void GivenCommand_WhenProcess_ThenLoadRootAndGenerateEvents()
        {
            var rootId = Guid.NewGuid();

            eventStore.GetEvents(rootId)
                .Returns(new List<IEvent>()
                    {
                        new AccountCreatedEvent() { RootId = rootId }
                    });

            rootFactory.Create(Arg.Any<AccountCreatedEvent>())
                .Returns(r => new Account());

            serviceLocator
                .ResolveEventHandler(Arg.Any<Account>(),
                    new AccountCreatedEvent())
                .Returns(new AccountCreatedEventHandler());



            Assert.IsTrue(true);
        }

        private ICommandProcessor CreateSut()
        {
            return new CommandProcessor(rootFactory, serviceLocator, eventStore);
        }
    }
}
