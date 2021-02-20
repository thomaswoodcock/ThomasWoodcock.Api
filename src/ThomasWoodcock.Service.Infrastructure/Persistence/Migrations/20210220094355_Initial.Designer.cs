﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThomasWoodcock.Service.Infrastructure.Persistence.Accounts;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(AccountContext))]
    [Migration("20210220094355_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("ThomasWoodcock.Service.Application.Accounts.Entities.AccountActivationKey", b =>
                {
                    b.Property<Guid>("Value")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.HasKey("Value");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("ActivationKeys");
                });

            modelBuilder.Entity("ThomasWoodcock.Service.Domain.Accounts.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("ThomasWoodcock.Service.Application.Accounts.Entities.AccountActivationKey", b =>
                {
                    b.HasOne("ThomasWoodcock.Service.Domain.Accounts.Account", null)
                        .WithOne()
                        .HasForeignKey("ThomasWoodcock.Service.Application.Accounts.Entities.AccountActivationKey", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThomasWoodcock.Service.Domain.Accounts.Account", b =>
                {
                    b.OwnsOne("ThomasWoodcock.Service.Domain.SharedKernel.EmailAddress", "EmailAddress", b1 =>
                        {
                            b1.Property<Guid>("AccountId")
                                .HasColumnType("TEXT");

                            b1.Property<string>("_value")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("AccountId");

                            b1.ToTable("Accounts");

                            b1.WithOwner()
                                .HasForeignKey("AccountId");
                        });

                    b.Navigation("EmailAddress")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
