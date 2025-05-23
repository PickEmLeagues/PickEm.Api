﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PickEm.Api.DataAccess;

#nullable disable

namespace PickEm.Api.DataAccess.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250507171242_AddSport")]
    partial class AddSport
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LeagueUser", b =>
                {
                    b.Property<long>("MemberLeaguesId")
                        .HasColumnType("bigint");

                    b.Property<long>("MembersId")
                        .HasColumnType("bigint");

                    b.HasKey("MemberLeaguesId", "MembersId");

                    b.HasIndex("MembersId");

                    b.ToTable("LeagueUser");
                });

            modelBuilder.Entity("PickEm.Api.Domain.Game", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("AwayOdds")
                        .HasColumnType("numeric");

                    b.Property<int?>("AwayScore")
                        .HasColumnType("integer");

                    b.Property<string>("AwayTeam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("DrawOdds")
                        .HasColumnType("numeric");

                    b.Property<decimal>("HomeOdds")
                        .HasColumnType("numeric");

                    b.Property<int?>("HomeScore")
                        .HasColumnType("integer");

                    b.Property<string>("HomeTeam")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsFinal")
                        .HasColumnType("boolean");

                    b.Property<bool>("OddsClosed")
                        .HasColumnType("boolean");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Week")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("SportId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PickEm.Api.Domain.League", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("OwnerId")
                        .HasColumnType("bigint");

                    b.Property<long>("SportId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.HasIndex("SportId");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("PickEm.Api.Domain.LeagueGame", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("LeagueId")
                        .HasColumnType("bigint");

                    b.Property<bool>("PicksClosed")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("LeagueId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("PickEm.Api.Domain.Pick", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("GameId")
                        .HasColumnType("bigint");

                    b.Property<long>("GameLeagueId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<int>("TeamType")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Wager")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Picks");
                });

            modelBuilder.Entity("PickEm.Api.Domain.Sport", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Season")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Week")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("PickEm.Api.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("LeagueUser", b =>
                {
                    b.HasOne("PickEm.Api.Domain.League", null)
                        .WithMany()
                        .HasForeignKey("MemberLeaguesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PickEm.Api.Domain.User", null)
                        .WithMany()
                        .HasForeignKey("MembersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PickEm.Api.Domain.Game", b =>
                {
                    b.HasOne("PickEm.Api.Domain.Sport", "Sport")
                        .WithMany("Games")
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("PickEm.Api.Domain.League", b =>
                {
                    b.HasOne("PickEm.Api.Domain.User", "Owner")
                        .WithMany("OwnedLeagues")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PickEm.Api.Domain.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("PickEm.Api.Domain.LeagueGame", b =>
                {
                    b.HasOne("PickEm.Api.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PickEm.Api.Domain.League", "League")
                        .WithMany("Schedule")
                        .HasForeignKey("LeagueId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("League");
                });

            modelBuilder.Entity("PickEm.Api.Domain.Pick", b =>
                {
                    b.HasOne("PickEm.Api.Domain.LeagueGame", "Game")
                        .WithMany("Picks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PickEm.Api.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("PickEm.Api.Domain.League", b =>
                {
                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("PickEm.Api.Domain.LeagueGame", b =>
                {
                    b.Navigation("Picks");
                });

            modelBuilder.Entity("PickEm.Api.Domain.Sport", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("PickEm.Api.Domain.User", b =>
                {
                    b.Navigation("OwnedLeagues");
                });
#pragma warning restore 612, 618
        }
    }
}
