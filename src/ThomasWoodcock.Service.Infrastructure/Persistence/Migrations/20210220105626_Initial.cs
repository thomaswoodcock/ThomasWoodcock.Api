using System;

using Microsoft.EntityFrameworkCore.Migrations;

namespace ThomasWoodcock.Service.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable("Accounts",
                table => new
                {
                    Id = table.Column<Guid>("TEXT", nullable: false),
                    Name = table.Column<string>("TEXT", nullable: false),
                    EmailAddress__value = table.Column<string>("TEXT", nullable: false),
                    Password = table.Column<string>("TEXT", nullable: false),
                    IsActive = table.Column<bool>("INTEGER", nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_Accounts", x => x.Id); });

            migrationBuilder.CreateTable("ActivationKeys",
                table => new
                {
                    Value = table.Column<Guid>("TEXT", nullable: false),
                    AccountId = table.Column<Guid>("TEXT", nullable: false)
                }, constraints: table =>
                {
                    table.PrimaryKey("PK_ActivationKeys", x => x.Value);

                    table.ForeignKey("FK_ActivationKeys_Accounts_AccountId", x => x.AccountId, "Accounts", "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex("IX_ActivationKeys_AccountId", "ActivationKeys", "AccountId", unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("ActivationKeys");
            migrationBuilder.DropTable("Accounts");
        }
    }
}
