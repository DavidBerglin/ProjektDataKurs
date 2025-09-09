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
                name: "Attraction",
                columns: table => new
                {
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address = table.Column<string>(type: "varchar(200)", nullable: true),
                    City = table.Column<string>(type: "varchar(200)", nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false),
                    Discriminator = table.Column<string>(type: "varchar(200)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attraction", x => x.AttractionId);
                });

            migrationBuilder.CreateTable(
                name: "CreditCardDbM",
                columns: table => new
                {
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Issuer = table.Column<int>(type: "int", nullable: false),
                    CardNumber = table.Column<string>(type: "varchar(200)", nullable: true),
                    ExpiryMonth = table.Column<string>(type: "varchar(200)", nullable: true),
                    ExpiryYear = table.Column<string>(type: "varchar(200)", nullable: true),
                    CardHolderName = table.Column<string>(type: "varchar(200)", nullable: true),
                    EncryptedToken = table.Column<string>(type: "varchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCardDbM", x => x.CreditCardId);
                });

            migrationBuilder.CreateTable(
                name: "AddressDbM",
                columns: table => new
                {
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StreetAddress = table.Column<string>(type: "varchar(200)", nullable: true),
                    City = table.Column<string>(type: "varchar(200)", nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", nullable: true),
                    ZipCode = table.Column<int>(type: "int", nullable: false),
                    AttractionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressDbM", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_AddressDbM_Attraction_AttractionId",
                        column: x => x.AttractionId,
                        principalTable: "Attraction",
                        principalColumn: "AttractionId");
                });

            migrationBuilder.CreateTable(
                name: "UsersDbM",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "varchar(200)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    AddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDbM", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UsersDbM_AddressDbM_AddressId",
                        column: x => x.AddressId,
                        principalTable: "AddressDbM",
                        principalColumn: "AddressId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressDbM_AttractionId",
                table: "AddressDbM",
                column: "AttractionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDbM_AddressId",
                table: "UsersDbM",
                column: "AddressId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditCardDbM");

            migrationBuilder.DropTable(
                name: "UsersDbM");

            migrationBuilder.DropTable(
                name: "AddressDbM");

            migrationBuilder.DropTable(
                name: "Attraction");
        }
    }
}
