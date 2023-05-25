using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class someChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Hobbies_HobbyId",
                table: "UserHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Users_UserId",
                table: "UserHobby");

            migrationBuilder.RenameColumn(
                name: "HobbyId",
                table: "UserHobby",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "UserHobby",
                newName: "HobbiesId");

            migrationBuilder.RenameIndex(
                name: "IX_UserHobby_HobbyId",
                table: "UserHobby",
                newName: "IX_UserHobby_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Hobbies_HobbiesId",
                table: "UserHobby",
                column: "HobbiesId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Users_UsersId",
                table: "UserHobby",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Hobbies_HobbiesId",
                table: "UserHobby");

            migrationBuilder.DropForeignKey(
                name: "FK_UserHobby_Users_UsersId",
                table: "UserHobby");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "UserHobby",
                newName: "HobbyId");

            migrationBuilder.RenameColumn(
                name: "HobbiesId",
                table: "UserHobby",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserHobby_UsersId",
                table: "UserHobby",
                newName: "IX_UserHobby_HobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Hobbies_HobbyId",
                table: "UserHobby",
                column: "HobbyId",
                principalTable: "Hobbies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserHobby_Users_UserId",
                table: "UserHobby",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
