using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoffeeShop2.Data.Migrations
{
    public partial class connect3CCIUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CartItem",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Cart",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Cart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_ApplicationUserId",
                table: "CartItem",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Cart_ApplicationUserId",
                table: "Cart",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_AspNetUsers_ApplicationUserId",
                table: "Cart",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_AspNetUsers_ApplicationUserId",
                table: "CartItem",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_AspNetUsers_ApplicationUserId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_AspNetUsers_ApplicationUserId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_CartItem_ApplicationUserId",
                table: "CartItem");

            migrationBuilder.DropIndex(
                name: "IX_Cart_ApplicationUserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cart");
        }
    }
}
