using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class miInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddressDbM",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "varchar(200)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", maxLength: 100, nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDbM", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "AttractionDbM",
                columns: table => new
                {
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: true),
                    Description = table.Column<string>(type: "varchar(200)", nullable: true),
                    City = table.Column<string>(type: "varchar(200)", nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionDbM", x => x.AttractionId);
                    table.ForeignKey(
                        name: "FK_AttractionDbM_AddressDbM_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressDbM",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersDbM",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "varchar(200)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDbM", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UsersDbM_AddressDbM_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressDbM",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CommentDbM",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "varchar(200)", nullable: true),
                    AttractionDbMAttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    usersDbMUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDbM", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentDbM_AttractionDbM_AttractionDbMAttractionId",
                        column: x => x.AttractionDbMAttractionId,
                        principalTable: "AttractionDbM",
                        principalColumn: "AttractionId");
                    table.ForeignKey(
                        name: "FK_CommentDbM_UsersDbM_usersDbMUserId",
                        column: x => x.usersDbMUserId,
                        principalTable: "UsersDbM",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttractionDbM_AddressId",
                table: "AttractionDbM",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentDbM_AttractionDbMAttractionId",
                table: "CommentDbM",
                column: "AttractionDbMAttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDbM_usersDbMUserId",
                table: "CommentDbM",
                column: "usersDbMUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDbM_AddressId",
                table: "UsersDbM",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentDbM");

            migrationBuilder.DropTable(
                name: "AttractionDbM");

            migrationBuilder.DropTable(
                name: "UsersDbM");

            migrationBuilder.DropTable(
                name: "AddressDbM");
        }
    }
}
