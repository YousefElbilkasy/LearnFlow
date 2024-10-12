﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnFlow.Migrations
{
  /// <inheritdoc />
  public partial class AddIdentity : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Courses_Users_InstructorId",
          table: "Courses");

      migrationBuilder.DropForeignKey(
          name: "FK_Enrollments_Users_StudentId",
          table: "Enrollments");

      migrationBuilder.DropForeignKey(
          name: "FK_Payments_Users_StudentId",
          table: "Payments");

      migrationBuilder.DropForeignKey(
          name: "FK_QuizResults_Users_StudentId",
          table: "QuizResults");

      migrationBuilder.DropForeignKey(
          name: "FK_Reviews_Users_StudentId",
          table: "Reviews");

      migrationBuilder.DropPrimaryKey(
          name: "PK_Users",
          table: "Users");

      migrationBuilder.RenameTable(
          name: "Users",
          newName: "AspNetUsers");

      migrationBuilder.RenameColumn(
          name: "Role",
          table: "AspNetUsers",
          newName: "AccessFailedCount");

      migrationBuilder.RenameColumn(
          name: "UserId",
          table: "AspNetUsers",
          newName: "Id");

      migrationBuilder.AlterColumn<string>(
          name: "PasswordHash",
          table: "AspNetUsers",
          type: "nvarchar(max)",
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AlterColumn<string>(
          name: "Email",
          table: "AspNetUsers",
          type: "nvarchar(256)",
          maxLength: 256,
          nullable: true,
          oldClrType: typeof(string),
          oldType: "nvarchar(max)");

      migrationBuilder.AddColumn<string>(
          name: "ConcurrencyStamp",
          table: "AspNetUsers",
          type: "nvarchar(max)",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "EmailConfirmed",
          table: "AspNetUsers",
          type: "bit",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "ImageUrl",
          table: "AspNetUsers",
          type: "nvarchar(max)",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "LockoutEnabled",
          table: "AspNetUsers",
          type: "bit",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<DateTimeOffset>(
          name: "LockoutEnd",
          table: "AspNetUsers",
          type: "datetimeoffset",
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "NormalizedEmail",
          table: "AspNetUsers",
          type: "nvarchar(256)",
          maxLength: 256,
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "NormalizedUserName",
          table: "AspNetUsers",
          type: "nvarchar(256)",
          maxLength: 256,
          nullable: true);

      migrationBuilder.AddColumn<string>(
          name: "PhoneNumber",
          table: "AspNetUsers",
          type: "nvarchar(max)",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "PhoneNumberConfirmed",
          table: "AspNetUsers",
          type: "bit",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "SecurityStamp",
          table: "AspNetUsers",
          type: "nvarchar(max)",
          nullable: true);

      migrationBuilder.AddColumn<bool>(
          name: "TwoFactorEnabled",
          table: "AspNetUsers",
          type: "bit",
          nullable: false,
          defaultValue: false);

      migrationBuilder.AddColumn<string>(
          name: "UserName",
          table: "AspNetUsers",
          type: "nvarchar(256)",
          maxLength: 256,
          nullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "PK_AspNetUsers",
          table: "AspNetUsers",
          column: "Id");

      migrationBuilder.CreateTable(
          name: "AspNetRoles",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
            ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetRoles", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            UserId = table.Column<int>(type: "int", nullable: false),
            ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
            table.ForeignKey(
                      name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserLogins",
          columns: table => new
          {
            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
            ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
            UserId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
            table.ForeignKey(
                      name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserTokens",
          columns: table => new
          {
            UserId = table.Column<int>(type: "int", nullable: false),
            LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
            Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
            Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
            table.ForeignKey(
                      name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "AspNetRoleClaims",
          columns: table => new
          {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            RoleId = table.Column<int>(type: "int", nullable: false),
            ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
            ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
            table.ForeignKey(
                      name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateTable(
          name: "AspNetUserRoles",
          columns: table => new
          {
            UserId = table.Column<int>(type: "int", nullable: false),
            RoleId = table.Column<int>(type: "int", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
            table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                      column: x => x.RoleId,
                      principalTable: "AspNetRoles",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
            table.ForeignKey(
                      name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                      column: x => x.UserId,
                      principalTable: "AspNetUsers",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.NoAction);
          });

      migrationBuilder.CreateIndex(
          name: "EmailIndex",
          table: "AspNetUsers",
          column: "NormalizedEmail");

      migrationBuilder.CreateIndex(
          name: "UserNameIndex",
          table: "AspNetUsers",
          column: "NormalizedUserName",
          unique: true,
          filter: "[NormalizedUserName] IS NOT NULL");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetRoleClaims_RoleId",
          table: "AspNetRoleClaims",
          column: "RoleId");

      migrationBuilder.CreateIndex(
          name: "RoleNameIndex",
          table: "AspNetRoles",
          column: "NormalizedName",
          unique: true,
          filter: "[NormalizedName] IS NOT NULL");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserClaims_UserId",
          table: "AspNetUserClaims",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserLogins_UserId",
          table: "AspNetUserLogins",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_AspNetUserRoles_RoleId",
          table: "AspNetUserRoles",
          column: "RoleId");

      migrationBuilder.AddForeignKey(
          name: "FK_Courses_AspNetUsers_InstructorId",
          table: "Courses",
          column: "InstructorId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Enrollments_AspNetUsers_StudentId",
          table: "Enrollments",
          column: "StudentId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Payments_AspNetUsers_StudentId",
          table: "Payments",
          column: "StudentId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_QuizResults_AspNetUsers_StudentId",
          table: "QuizResults",
          column: "StudentId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Reviews_AspNetUsers_StudentId",
          table: "Reviews",
          column: "StudentId",
          principalTable: "AspNetUsers",
          principalColumn: "Id",
          onDelete: ReferentialAction.NoAction);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropForeignKey(
          name: "FK_Courses_AspNetUsers_InstructorId",
          table: "Courses");

      migrationBuilder.DropForeignKey(
          name: "FK_Enrollments_AspNetUsers_StudentId",
          table: "Enrollments");

      migrationBuilder.DropForeignKey(
          name: "FK_Payments_AspNetUsers_StudentId",
          table: "Payments");

      migrationBuilder.DropForeignKey(
          name: "FK_QuizResults_AspNetUsers_StudentId",
          table: "QuizResults");

      migrationBuilder.DropForeignKey(
          name: "FK_Reviews_AspNetUsers_StudentId",
          table: "Reviews");

      migrationBuilder.DropTable(
          name: "AspNetRoleClaims");

      migrationBuilder.DropTable(
          name: "AspNetUserClaims");

      migrationBuilder.DropTable(
          name: "AspNetUserLogins");

      migrationBuilder.DropTable(
          name: "AspNetUserRoles");

      migrationBuilder.DropTable(
          name: "AspNetUserTokens");

      migrationBuilder.DropTable(
          name: "AspNetRoles");

      migrationBuilder.DropPrimaryKey(
          name: "PK_AspNetUsers",
          table: "AspNetUsers");

      migrationBuilder.DropIndex(
          name: "EmailIndex",
          table: "AspNetUsers");

      migrationBuilder.DropIndex(
          name: "UserNameIndex",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "ConcurrencyStamp",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "EmailConfirmed",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "ImageUrl",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "LockoutEnabled",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "LockoutEnd",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "NormalizedEmail",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "NormalizedUserName",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "PhoneNumber",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "PhoneNumberConfirmed",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "SecurityStamp",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "TwoFactorEnabled",
          table: "AspNetUsers");

      migrationBuilder.DropColumn(
          name: "UserName",
          table: "AspNetUsers");

      migrationBuilder.RenameTable(
          name: "AspNetUsers",
          newName: "Users");

      migrationBuilder.RenameColumn(
          name: "AccessFailedCount",
          table: "Users",
          newName: "Role");

      migrationBuilder.RenameColumn(
          name: "Id",
          table: "Users",
          newName: "UserId");

      migrationBuilder.AlterColumn<string>(
          name: "PasswordHash",
          table: "Users",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(max)",
          oldNullable: true);

      migrationBuilder.AlterColumn<string>(
          name: "Email",
          table: "Users",
          type: "nvarchar(max)",
          nullable: false,
          defaultValue: "",
          oldClrType: typeof(string),
          oldType: "nvarchar(256)",
          oldMaxLength: 256,
          oldNullable: true);

      migrationBuilder.AddPrimaryKey(
          name: "PK_Users",
          table: "Users",
          column: "UserId");

      migrationBuilder.AddForeignKey(
          name: "FK_Courses_Users_InstructorId",
          table: "Courses",
          column: "InstructorId",
          principalTable: "Users",
          principalColumn: "UserId",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Enrollments_Users_StudentId",
          table: "Enrollments",
          column: "StudentId",
          principalTable: "Users",
          principalColumn: "UserId",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Payments_Users_StudentId",
          table: "Payments",
          column: "StudentId",
          principalTable: "Users",
          principalColumn: "UserId",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_QuizResults_Users_StudentId",
          table: "QuizResults",
          column: "StudentId",
          principalTable: "Users",
          principalColumn: "UserId",
          onDelete: ReferentialAction.NoAction);

      migrationBuilder.AddForeignKey(
          name: "FK_Reviews_Users_StudentId",
          table: "Reviews",
          column: "StudentId",
          principalTable: "Users",
          principalColumn: "UserId",
          onDelete: ReferentialAction.NoAction);
    }
  }
}