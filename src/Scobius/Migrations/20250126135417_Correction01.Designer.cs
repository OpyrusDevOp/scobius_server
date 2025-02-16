﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Scobius;

#nullable disable

namespace Scobius.Migrations
{
    [DbContext(typeof(ScobiusTest_Context))]
    [Migration("20250126135417_Correction01")]
    partial class Correction01
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Scobius.Entities.Chat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("UserAId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserBId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserAId");

                    b.HasIndex("UserBId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("Scobius.Entities.Friendship", b =>
                {
                    b.Property<string>("UserAId")
                        .HasColumnType("text");

                    b.Property<string>("UserBId")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("UserAId", "UserBId");

                    b.HasIndex("UserBId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("Scobius.Entities.FriendshipRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("FriendshipRequests");
                });

            modelBuilder.Entity("Scobius.Entities.Media", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("mediaType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Scobius.Entities.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ChatId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("JointMedia")
                        .HasColumnType("uuid");

                    b.Property<string>("MessageContent")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("MessageRepliedTo")
                        .HasColumnType("uuid");

                    b.Property<bool>("Seen")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("SendAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("JointMedia");

                    b.HasIndex("MessageRepliedTo");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Scobius.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastSeen")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Scobius.Entities.Chat", b =>
                {
                    b.HasOne("Scobius.Entities.User", "UserA")
                        .WithMany()
                        .HasForeignKey("UserAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scobius.Entities.User", "UserB")
                        .WithMany()
                        .HasForeignKey("UserBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("Scobius.Entities.Friendship", b =>
                {
                    b.HasOne("Scobius.Entities.User", "UserA")
                        .WithMany()
                        .HasForeignKey("UserAId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scobius.Entities.User", "UserB")
                        .WithMany()
                        .HasForeignKey("UserBId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserA");

                    b.Navigation("UserB");
                });

            modelBuilder.Entity("Scobius.Entities.FriendshipRequest", b =>
                {
                    b.HasOne("Scobius.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scobius.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Scobius.Entities.Message", b =>
                {
                    b.HasOne("Scobius.Entities.Chat", "Chat")
                        .WithMany()
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Scobius.Entities.Media", "Media")
                        .WithMany()
                        .HasForeignKey("JointMedia");

                    b.HasOne("Scobius.Entities.Message", "RepliedTo")
                        .WithMany()
                        .HasForeignKey("MessageRepliedTo");

                    b.HasOne("Scobius.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Media");

                    b.Navigation("RepliedTo");

                    b.Navigation("Sender");
                });
#pragma warning restore 612, 618
        }
    }
}
