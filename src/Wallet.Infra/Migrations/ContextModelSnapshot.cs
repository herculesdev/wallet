﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Wallet.Infra.Data.Relational.Contexts;

#nullable disable

namespace Wallet.Infra.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.2.22153.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Wallet.Domain.Entities.Account", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric")
                        .HasColumnName("Balance");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeletedAt");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("OwnerId");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("Type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<DateTime>("UpdatedBalanceAt")
                        .IsConcurrencyToken()
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedBalanceAt");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Account", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Balance", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeletedAt");

                    b.Property<bool>("IsDebit")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDebit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<Guid>("TransactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("TransactionId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<decimal>("Value")
                        .HasColumnType("numeric")
                        .HasColumnName("Value");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("TransactionId");

                    b.ToTable("Balance", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Transaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric")
                        .HasColumnName("Amount");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeletedAt");

                    b.Property<Guid>("DestinationAccountId")
                        .HasColumnType("uuid")
                        .HasColumnName("DestinationAccountId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<Guid?>("ReferringId")
                        .HasColumnType("uuid")
                        .HasColumnName("ReferringId");

                    b.Property<Guid?>("SourceAccountId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("Type");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("DestinationAccountId");

                    b.HasIndex("ReferringId");

                    b.HasIndex("SourceAccountId");

                    b.ToTable("Transaction", (string)null);
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User.BaseUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("Id")
                        .HasDefaultValueSql("uuid_generate_v4()");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("Date")
                        .HasColumnName("BirthDate");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeletedAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("Email");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("IsDeleted");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("LastName");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("character varying(64)")
                        .HasColumnName("Name");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("User", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("BaseUser");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User.User", b =>
                {
                    b.HasBaseType("Wallet.Domain.Entities.User.BaseUser");

                    b.Property<int>("Nature")
                        .HasColumnType("integer")
                        .HasColumnName("Nature");

                    b.Property<int>("RegistrationStatus")
                        .HasColumnType("integer")
                        .HasColumnName("RegistrationStatus");

                    b.Property<DateTime>("RegistrationStatusUpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("RegistrationStatusUpdatedAt");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Account", b =>
                {
                    b.HasOne("Wallet.Domain.Entities.User.User", "Owner")
                        .WithMany("Accounts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Balance", b =>
                {
                    b.HasOne("Wallet.Domain.Entities.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wallet.Domain.Entities.Transaction", "Transaction")
                        .WithMany()
                        .HasForeignKey("TransactionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Transaction");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Wallet.Domain.Entities.Account", "DestinationAccount")
                        .WithMany()
                        .HasForeignKey("DestinationAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wallet.Domain.Entities.Transaction", "Referring")
                        .WithMany()
                        .HasForeignKey("ReferringId");

                    b.HasOne("Wallet.Domain.Entities.Account", "SourceAccount")
                        .WithMany()
                        .HasForeignKey("SourceAccountId");

                    b.Navigation("DestinationAccount");

                    b.Navigation("Referring");

                    b.Navigation("SourceAccount");
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User.BaseUser", b =>
                {
                    b.OwnsOne("Wallet.Domain.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<Guid>("BaseUserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("EncryptedValue")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("EncryptedPassword");

                            b1.HasKey("BaseUserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("BaseUserId");
                        });

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User.User", b =>
                {
                    b.OwnsOne("Wallet.Domain.ValueObjects.DocumentNumber", "Document", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uuid");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasMaxLength(14)
                                .HasColumnType("character varying(14)")
                                .HasColumnName("DocumentNumber");

                            b1.HasKey("UserId");

                            b1.ToTable("User");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Document")
                        .IsRequired();
                });

            modelBuilder.Entity("Wallet.Domain.Entities.User.User", b =>
                {
                    b.Navigation("Accounts");
                });
#pragma warning restore 612, 618
        }
    }
}
