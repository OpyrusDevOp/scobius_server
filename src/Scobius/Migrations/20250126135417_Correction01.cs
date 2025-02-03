using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scobius.Migrations
{
    /// <inheritdoc />
    public partial class Correction01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Frienships");

            migrationBuilder.RenameColumn(
                name: "jointMedia",
                table: "Messages",
                newName: "JointMedia");

            migrationBuilder.AddColumn<Guid>(
                name: "MessageRepliedTo",
                table: "Messages",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Seen",
                table: "Messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "SendAt",
                table: "Messages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "FriendshipRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<string>(type: "text", nullable: false),
                    ReceiverId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendshipRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FriendshipRequests_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    UserAId = table.Column<string>(type: "text", nullable: false),
                    UserBId = table.Column<string>(type: "text", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => new { x.UserAId, x.UserBId });
                    table.ForeignKey(
                        name: "FK_Friendships_Users_UserAId",
                        column: x => x.UserAId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Friendships_Users_UserBId",
                        column: x => x.UserBId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_JointMedia",
                table: "Messages",
                column: "JointMedia");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MessageRepliedTo",
                table: "Messages",
                column: "MessageRepliedTo");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserAId",
                table: "Chats",
                column: "UserAId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_UserBId",
                table: "Chats",
                column: "UserBId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequests_ReceiverId",
                table: "FriendshipRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_FriendshipRequests_SenderId",
                table: "FriendshipRequests",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_UserBId",
                table: "Friendships",
                column: "UserBId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserAId",
                table: "Chats",
                column: "UserAId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chats_Users_UserBId",
                table: "Chats",
                column: "UserBId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Medias_JointMedia",
                table: "Messages",
                column: "JointMedia",
                principalTable: "Medias",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Messages_MessageRepliedTo",
                table: "Messages",
                column: "MessageRepliedTo",
                principalTable: "Messages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserAId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Chats_Users_UserBId",
                table: "Chats");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Chats_ChatId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Medias_JointMedia",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Messages_MessageRepliedTo",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropTable(
                name: "FriendshipRequests");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropIndex(
                name: "IX_Messages_ChatId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_JointMedia",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_MessageRepliedTo",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_SenderId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserAId",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_UserBId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "MessageRepliedTo",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Seen",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "SendAt",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "JointMedia",
                table: "Messages",
                newName: "jointMedia");

            migrationBuilder.CreateTable(
                name: "Frienships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserAId = table.Column<string>(type: "text", nullable: false),
                    UserBId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frienships", x => x.Id);
                });
        }
    }
}
