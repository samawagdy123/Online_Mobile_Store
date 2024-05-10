using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_mobile_store.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Gender = table.Column<int>(type: "int", unicode: false, maxLength: 20, nullable: false),
                    Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    prod_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    prod_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    price = table.Column<decimal>(type: "money", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sellerid = table.Column<int>(type: "int", nullable: true),
                    image = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.prod_id);
                    table.ForeignKey(
                        name: "FK_Products_Users",
                        column: x => x.sellerid,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Users_Products",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    prod_id = table.Column<int>(type: "int", nullable: false),
                    selected_quantity = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selected_Products", x => new { x.user_id, x.prod_id });
                    table.ForeignKey(
                        name: "FK_Users_Products_Products",
                        column: x => x.prod_id,
                        principalTable: "Products",
                        principalColumn: "prod_id");
                    table.ForeignKey(
                        name: "FK_Users_Products_Users",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_sellerid",
                table: "Products",
                column: "sellerid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Products_prod_id",
                table: "Users_Products",
                column: "prod_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users_Products");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
