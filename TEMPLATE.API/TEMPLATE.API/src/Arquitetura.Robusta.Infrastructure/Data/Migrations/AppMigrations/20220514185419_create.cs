using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Template.Infrastructure.Data.Migrations.AppMigrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Email_Endereco = table.Column<string>(nullable: true),
                    Cpf_Numero = table.Column<string>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    DataExclusao = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");
        }
    }
}
