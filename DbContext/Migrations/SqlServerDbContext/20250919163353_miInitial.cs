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
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 1000, nullable: true),
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
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", maxLength: 250, nullable: false),
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
                    Text = table.Column<string>(type: "varchar(200)", maxLength: 1000, nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentDbM", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentDbM_AttractionDbM_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "AttractionDbM",
                        principalColumn: "AttractionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentDbM_UsersDbM_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDbM",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttractionDbM_AddressId",
                table: "AttractionDbM",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentDbM_AttractionId",
                table: "CommentDbM",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentDbM_UserId",
                table: "CommentDbM",
                column: "UserId");

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
