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
            serviceLocator.ResolveCommandHandler(Arg.Any<CreateAccountCommand>())
                .Returns((ICommandHandler<CreateAccountCommand>)null);

            var sut = CreateSut<CreateAccountCommand>();

            Assert.That(() => sut.Process(new CreateAccountCommand()),
                Throws.TypeOf<ServiceLocationException>());
        }

        [Test]
        public void GivenCommand_WhenProcess_ThenLoadRootAndGenerateEvents()
        {
            var rootId = Guid.NewGuid();

            var cmd = new UpdateAccountBalanceCommand()
                      {
                          RootId = rootId,
                          Change = 10
                      };

            eventStore.GetEvents(rootId)
                .Returns(new List<IEvent>()
                    {
                        new AccountCreatedEvent() { RootId = rootId }
                    });

            rootFactory.Create(Arg.Any<AccountCreatedEvent>())
                .Returns(r => new Account());

            serviceLocator
                .ResolveCommandHandler(cmd)
                .Returns(new UpdateAccountBalanceCommandHandler());

            serviceLocator
                .ResolveEventHandler(Arg.Any<Account>(), Arg.Any<AccountCreatedEvent>())
                .Returns(new AccountCreatedEventHandler());

            var sut = CreateSut<UpdateAccountBalanceCommand>();
            sut.Process(cmd);

            serviceLocator.Received(1).ResolveCommandHandler(cmd);
            eventStore.Received(1).GetEvents(rootId);
            rootFactory.Received(1).Create(Arg.Any<AccountCreatedEvent>());
            eventStore.Received(1).SaveEvents(Arg.Any<IList<IEvent>>());
        }

        private ICommandProcessor<TCmd> CreateSut<TCmd>() 
            where TCmd : ICommand
        {
            return new CommandProcessor<TCmd>(
                rootFactory, serviceLocator, eventStore);
        }
    }
}
