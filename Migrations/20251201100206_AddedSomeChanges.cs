using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Loan_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackgroundVerifications_LoanOfficers_LoanOfficerId",
                table: "BackgroundVerifications");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "LoanVerifications");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HelpReports");

            migrationBuilder.DropColumn(
                name: "Aadhaar",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PAN",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "LoanOfficerId",
                table: "BackgroundVerifications",
                newName: "AssignedOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_BackgroundVerifications_LoanOfficerId",
                table: "BackgroundVerifications",
                newName: "IX_BackgroundVerifications_AssignedOfficerId");

            migrationBuilder.AddColumn<int>(
                name: "AssignedOfficerId",
                table: "LoanVerifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "LoanVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "LoanOfficers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "HelpReports",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "HelpReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "BackgroundVerifications",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "BackgroundVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "FeedbackQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AnswerType = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackQuestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerFeedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FeedbackQuestionId = table.Column<int>(type: "int", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerFeedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerFeedbacks_FeedbackQuestions_FeedbackQuestionId",
                        column: x => x.FeedbackQuestionId,
                        principalTable: "FeedbackQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanVerifications_AssignedOfficerId",
                table: "LoanVerifications",
                column: "AssignedOfficerId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                unique: true,
                filter: "[ApplicationUserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_CustomerId",
                table: "CustomerFeedbacks",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerFeedbacks_FeedbackQuestionId",
                table: "CustomerFeedbacks",
                column: "FeedbackQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundVerifications_LoanOfficers_AssignedOfficerId",
                table: "BackgroundVerifications",
                column: "AssignedOfficerId",
                principalTable: "LoanOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanVerifications_LoanOfficers_AssignedOfficerId",
                table: "LoanVerifications",
                column: "AssignedOfficerId",
                principalTable: "LoanOfficers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackgroundVerifications_LoanOfficers_AssignedOfficerId",
                table: "BackgroundVerifications");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanVerifications_LoanOfficers_AssignedOfficerId",
                table: "LoanVerifications");

            migrationBuilder.DropTable(
                name: "CustomerFeedbacks");

            migrationBuilder.DropTable(
                name: "FeedbackQuestions");

            migrationBuilder.DropIndex(
                name: "IX_LoanVerifications_AssignedOfficerId",
                table: "LoanVerifications");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AssignedOfficerId",
                table: "LoanVerifications");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "LoanVerifications");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "LoanOfficers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "HelpReports");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "HelpReports");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "BackgroundVerifications");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Customers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "AssignedOfficerId",
                table: "BackgroundVerifications",
                newName: "LoanOfficerId");

            migrationBuilder.RenameIndex(
                name: "IX_BackgroundVerifications_AssignedOfficerId",
                table: "BackgroundVerifications",
                newName: "IX_BackgroundVerifications_LoanOfficerId");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "LoanVerifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HelpReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Aadhaar",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PAN",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "BackgroundVerifications",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                table: "Customers",
                column: "ApplicationUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_CustomerId",
                table: "Feedbacks",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackgroundVerifications_LoanOfficers_LoanOfficerId",
                table: "BackgroundVerifications",
                column: "LoanOfficerId",
                principalTable: "LoanOfficers",
                principalColumn: "Id");
        }
    }
}
