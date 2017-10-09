﻿// <auto-generated />
using DistCqrs.Sample.Service.Order.View;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace DistCqrs.Sample.Service.Order.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20171009125117_FixOrderToOrderItemConstraintToDeleteOnCascade")]
    partial class FixOrderToOrderItemConstraintToDeleteOnCascade
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DistCqrs.Sample.Domain.Order.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CustomerNotes");

                    b.Property<double>("Discount");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("OrderNo");

                    b.Property<double>("Total");

                    b.HasKey("Id");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("DistCqrs.Sample.Domain.Order.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<Guid?>("OrderId");

                    b.Property<int>("Ordinal");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("ProductText");

                    b.Property<int>("Qty");

                    b.Property<string>("QtyUnit");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Orderitem");
                });

            modelBuilder.Entity("DistCqrs.Sample.Domain.Order.OrderItem", b =>
                {
                    b.HasOne("DistCqrs.Sample.Domain.Order.Order", "Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
