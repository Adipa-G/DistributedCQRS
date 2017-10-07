using System;
using DistCqrs.Sample.Domain.Order.Events;
using DistCqrs.Sample.Domain.Order.Events.Handlers;
using NUnit.Framework;

namespace DistCqrs.Sample.Domain.Order.Test.Events.Handlers
{
    [TestFixture]
    public class OrderDeletedEventHandlerTest
    {
        [Test]
        public void GivenOrderAndEvent_WhenApply_ThenDeleteOrder()
        {
            var root = new Order {Id = Guid.NewGuid()};

            var evt = new OrderDeletedEvent(root.Id);

            var sut = CreateSut();
            sut.Apply(root, evt).Wait();

            Assert.IsTrue(root.IsDeleted);
        }

        private OrderDeletedEventHandler CreateSut()
        {
            return new OrderDeletedEventHandler();
        }
    }
}
