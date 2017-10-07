using System;
using System.Linq;
using DistCqrs.Sample.Domain.Order.Commands;
using DistCqrs.Sample.Domain.Order.Commands.Handlers;
using DistCqrs.Sample.Domain.Order.Events;
using NUnit.Framework;

namespace DistCqrs.Sample.Domain.Order.Test.Commands.Handlers
{
    [TestFixture]
    public class UpdateOrderItemCommandHandlerTest
    {
        [Test]
        public void GivenNonExistingOrder_WhenHandleCommand_ThenThrowException()
        {
            var root = new Order { Id = Guid.Empty };
            var cmd =
                new UpdateOrderItemCommand(Guid.NewGuid(), Guid.NewGuid(), 10,
                    "test", 1, 100);

            var sut = CreateSut();
            var exception =
                Assert.ThrowsAsync<DomainException>(
                    () => sut.Handle(root, cmd));
            Assert.IsTrue(exception.Message.ToLower()
                .Contains("does not exists"));
        }

        [Test]
        public void GivenNonExistingOrderItem_WhenHandleCommand_ThenThrowException()
        {
            var root = new Order { Id = Guid.NewGuid() };

            var cmd =
                new UpdateOrderItemCommand(root.Id, Guid.NewGuid(), 10,
                    "test", 1, 100);

            var sut = CreateSut();
            var exception =
                Assert.ThrowsAsync<DomainException>(
                    () => sut.Handle(root, cmd));
            Assert.IsTrue(exception.Message.ToLower()
                .Contains("does not exists"));
        }

        [Test]
        public void GivenDeletedOrder_WhenHandleCommand_ThenThrowException()
        {
            var root = new Order {Id = Guid.NewGuid(), IsDeleted = true};
            var productItem = new OrderItem { Id = Guid.NewGuid() };
            root.Items.Add(productItem);

            var cmd =
                new UpdateOrderItemCommand(root.Id, productItem.Id, 10,
                    "test", 1, 100);

            var sut = CreateSut();
            var exception =
                Assert.ThrowsAsync<DomainException>(
                    () => sut.Handle(root, cmd));
            Assert.IsTrue(exception.Message.ToLower()
                .Contains("deleted"));
        }

        [Test]
        public void GivenOrderItem_WhenHandleCommand_ThenReturnEvents()
        {
            var root = new Order { Id = Guid.NewGuid() };
            var productItem = new OrderItem { Id = Guid.NewGuid() };
            root.Items.Add(productItem);

            var cmd =
                new UpdateOrderItemCommand(root.Id, productItem.Id, 10,
                    "test", 1, 100);

            var sut = CreateSut();
            var task = sut.Handle(root, cmd);
            task.Wait();

            var result = task.Result;
            Assert.AreEqual(1, result.Count);

            var evt = result.First() as OrderItemUpdatedEvent;
            Assert.IsNotNull(evt);
            Assert.AreEqual(root.Id, evt.RootId);
            Assert.AreEqual(cmd.OrderItemId, evt.OrderItemId);
            Assert.AreEqual(cmd.ProductText, evt.ProductText);
            Assert.AreEqual(cmd.Ordinal, evt.Ordinal);
            Assert.AreEqual(cmd.Qty, evt.Qty);
            Assert.AreEqual(cmd.Amount, evt.Amount);
        }

        
        private UpdateOrderItemCommandHandler CreateSut()
        {
            return new UpdateOrderItemCommandHandler();
        }
    }
}
