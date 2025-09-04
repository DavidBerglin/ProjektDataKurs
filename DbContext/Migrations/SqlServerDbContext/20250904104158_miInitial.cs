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
                name: "AttractionDbM",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttractionDbM", x => x.Name);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttractionDbM");

            migrationBuilder.DropTable(
                name: "CreditCardDbM");
        }
    }
}
