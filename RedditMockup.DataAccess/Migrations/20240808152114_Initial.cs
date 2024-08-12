using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RedditMockup.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsBookmarked = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookmarks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<bool>(type: "bit", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionVotes_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswerVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Kind = table.Column<bool>(type: "bit", nullable: false),
                    AnswerId = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerVotes_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "FirstName", "Guid", "LastName", "LastUpdated" },
                values: new object[,]
                {
                    { 1, "Sepehr", new Guid("b2b12704-d6af-4b4d-9de5-15514c963a69"), "Foroughi Rad", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5160) },
                    { 2, "Bernard", new Guid("b973b686-6c74-4852-a8bc-8f4af0bf982d"), "Cool", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5180) },
                    { 3, "Alfonso", new Guid("8fc9eb3d-0766-4eda-b04c-e8acb83a7ee7"), "Bednar", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5200) },
                    { 4, "Brock", new Guid("d17f1821-b9e4-40af-8079-01f1cdbe5b7f"), "Conroy", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5440) },
                    { 5, "Nicolas", new Guid("cf4ee7f6-4072-4abc-84a6-30ddd2c293bc"), "VonRueden", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5470) },
                    { 6, "Evalyn", new Guid("9994c33b-c0cb-4522-9ead-0b82ed09e7be"), "Bailey", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5510) },
                    { 7, "Gerard", new Guid("acc6ba1a-60e1-4e92-b311-3802d80ad996"), "Bogan", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5530) },
                    { 8, "Mariam", new Guid("7534a43f-aa9c-436e-8748-4ce778e5be6c"), "Monahan", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5550) },
                    { 9, "Raven", new Guid("61a0cbc1-072c-4cb7-a8a6-9af69fe8db1f"), "Swift", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5580) },
                    { 10, "Rubye", new Guid("c0d677f1-c4b6-4d4d-bf56-5bfb5dadef80"), "Bogisich", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5600) },
                    { 11, "Mafalda", new Guid("415ed24d-2a8d-4fa7-a714-a8a2c3a1f3f7"), "Strosin", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5620) },
                    { 12, "Reymundo", new Guid("b9d15b85-d14d-4ae0-9dfb-6d826f04be9e"), "Huel", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5630) },
                    { 13, "Justyn", new Guid("434db3ca-4119-47f9-81c5-e492cac0267e"), "Bernhard", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5650) },
                    { 14, "Jazmin", new Guid("d7226083-ecfa-4951-aa4b-d7b40fad354b"), "Stamm", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5670) },
                    { 15, "Tiffany", new Guid("21ebd276-da1a-49ad-bcfb-65cfde80d506"), "Gusikowski", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5690) },
                    { 16, "Harry", new Guid("ab1fa988-3ae6-4602-b374-6734c19ca94e"), "Leuschke", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5710) },
                    { 17, "Asha", new Guid("f68cef23-c8a6-4c0a-b02e-26f57c6d1a78"), "Windler", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5730) },
                    { 18, "Jonathon", new Guid("c2b8f90d-64c7-4146-93c2-a272e2dee1e5"), "Barton", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5760) },
                    { 19, "Addison", new Guid("a6b4b74f-a6ed-4574-b0e1-80b4dc31ae41"), "Maggio", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5780) },
                    { 20, "Karl", new Guid("b78f44a4-ace2-44eb-82de-1c1d9a5e7977"), "Bernier", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5790) },
                    { 21, "Rowena", new Guid("6212a26d-c61b-4d80-a3f4-c6e117db0d51"), "Corkery", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5800) },
                    { 22, "Marguerite", new Guid("41e0efd9-0410-4f42-aeda-79a2a9a1e623"), "Ziemann", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5820) },
                    { 23, "Pink", new Guid("fc2588a3-cd40-46e9-a320-8fcdec0fedd9"), "Romaguera", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5830) },
                    { 24, "Susan", new Guid("fb5a3823-e60e-417e-bc0f-759993f06a0c"), "Johns", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5850) },
                    { 25, "Kim", new Guid("9b005b91-37b6-417a-8c60-d0802a40d7e1"), "Huels", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5860) },
                    { 26, "Verlie", new Guid("cd7c7de9-a4fe-44dc-b3f8-510c72aa1ea7"), "Beer", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5870) },
                    { 27, "Scotty", new Guid("7c5e7705-73a5-4b32-9b36-67d5a15bfb0c"), "Hintz", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5890) },
                    { 28, "Terry", new Guid("59c3f79a-57ff-4809-853b-948e57b0ea9c"), "Zemlak", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5910) },
                    { 29, "Tiffany", new Guid("20731b1d-0605-4cf3-9042-a5a2fc8febbf"), "Howe", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5920) },
                    { 30, "Kianna", new Guid("82e4fb8c-4aab-4a00-9343-b0681d3261a3"), "Schneider", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5930) },
                    { 31, "Arturo", new Guid("dfbf4637-bd13-4040-8387-077592c86c6a"), "Gleason", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5960) },
                    { 32, "Jammie", new Guid("a3a23bad-358d-4536-bd7d-f8ec9057f883"), "Mayert", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(5980) },
                    { 33, "Bulah", new Guid("1efd1992-b626-49e9-9377-b81a18d52836"), "Torp", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6000) },
                    { 34, "Jayme", new Guid("bbe051f5-bd36-4880-a79a-c36aa586bdd0"), "Johns", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6020) },
                    { 35, "Mellie", new Guid("7dbfdaaa-f160-4e8f-b3f6-df990e279442"), "Langworth", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6030) },
                    { 36, "Kaleigh", new Guid("bb271248-7193-458e-b88d-5b470a581186"), "Reichel", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6060) },
                    { 37, "Virginie", new Guid("d4a87ca4-0f3b-4d09-ba4f-a82ff7e48222"), "Corkery", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6080) },
                    { 38, "Jedidiah", new Guid("7d46dfa9-b6d2-4faa-8ad5-014d52de659f"), "DuBuque", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6090) },
                    { 39, "Addie", new Guid("6e819dc8-10b8-4638-9a0d-816c03be9e12"), "Lynch", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6100) },
                    { 40, "Mateo", new Guid("10d60c84-8d8d-4e49-a09d-781a88696ec7"), "Kuhn", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6110) },
                    { 41, "Favian", new Guid("b81996a6-6953-455a-a400-22f16f384bae"), "Adams", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6130) },
                    { 42, "Elenor", new Guid("b9b4338a-b869-4129-b3fc-3410590a77b0"), "Grant", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6140) },
                    { 43, "Cooper", new Guid("522c3b19-4c3e-402c-8453-f1fcb3e42b47"), "Kreiger", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6160) },
                    { 44, "Angus", new Guid("8d6cb72a-76a3-4564-bb44-4386ecfcec59"), "Beatty", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6170) },
                    { 45, "Lessie", new Guid("94d1c4ca-0392-472e-9b7e-e8067cd09bfe"), "Bradtke", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6180) },
                    { 46, "Maci", new Guid("ef6434b5-9c7b-4473-ab67-daf204bc6e49"), "Welch", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6200) },
                    { 47, "Jayden", new Guid("d2169929-c0c8-456a-8bb7-1411d0b484ed"), "Ferry", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6220) },
                    { 48, "Alene", new Guid("c48c08e3-749c-4baa-b0a2-eb48490427c5"), "Legros", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6230) },
                    { 49, "Adrienne", new Guid("04a6d940-df4c-4661-957a-00c5a09d8a8e"), "Schulist", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6250) },
                    { 50, "Lester", new Guid("b8eccb41-52e9-40fa-ab7a-c5478608cad2"), "Schinner", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6270) },
                    { 51, "Zella", new Guid("725157ed-5044-4d72-9ae0-cf84d81566b3"), "Beier", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6280) },
                    { 52, "Walker", new Guid("3a57b5f1-e5ab-4a56-8030-93b44eefa690"), "Klein", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6290) },
                    { 53, "Providenci", new Guid("5d009d30-0e29-4394-815c-047f7d0179b2"), "Lubowitz", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6320) },
                    { 54, "Willis", new Guid("b2599361-524a-4343-b4b4-88e2d8421110"), "Kerluke", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6330) },
                    { 55, "Clemens", new Guid("48c122a2-b06d-4a90-b4f0-5ab0a9339aec"), "Kshlerin", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6350) },
                    { 56, "Gregoria", new Guid("a03741f2-c533-4ca2-a771-95b22060d641"), "MacGyver", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6360) },
                    { 57, "Jaquan", new Guid("66ef838e-6568-4dbe-a41f-f5e51384892f"), "Kuvalis", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6380) },
                    { 58, "Ali", new Guid("a95c8ad2-8509-40d8-b36f-e6ca01bcc470"), "Upton", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6390) },
                    { 59, "Melody", new Guid("43799a55-36d2-462f-8996-7f6797140b39"), "Klein", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6420) },
                    { 60, "Brody", new Guid("11d8664a-4bf6-48c3-ba07-f3151b1ec488"), "West", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6430) },
                    { 61, "Queen", new Guid("0877c696-ea37-442e-814f-ec4624cf2b7a"), "Pagac", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6440) },
                    { 62, "Imelda", new Guid("77e4397a-8826-4c4b-83ea-3d333df5ca45"), "Jast", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6450) },
                    { 63, "Crawford", new Guid("20d54cf8-747c-4268-a01b-2a5faf94eb2d"), "Prohaska", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6460) },
                    { 64, "Jayda", new Guid("ff5056ec-dd62-43dc-bb1c-03bc72fb48f2"), "D'Amore", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6470) },
                    { 65, "Connie", new Guid("6f8587c5-95ea-4354-af79-9e7be9accd0e"), "Veum", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6480) },
                    { 66, "Alberto", new Guid("e9068d81-5db0-469d-8221-6aaca9c8a6dd"), "Zulauf", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6500) },
                    { 67, "Godfrey", new Guid("3e7a3e3f-d615-4272-83cb-8d0c59ed2471"), "Hegmann", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6510) },
                    { 68, "Curt", new Guid("44b38f9e-74b3-4571-b8d5-2d3397ebc437"), "Koepp", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6520) },
                    { 69, "Buddy", new Guid("fa93dba3-3b6e-43dc-a0fc-b42e7fde0e8c"), "Kuhn", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6530) },
                    { 70, "Veronica", new Guid("ad0318fd-47bc-452b-a118-7adae6e5490a"), "Gorczany", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6540) },
                    { 71, "Brad", new Guid("c2c60b2c-c5bb-4f9a-864a-e79581ac9f5e"), "Block", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6550) },
                    { 72, "Dylan", new Guid("e2a09589-45d4-4d2a-b453-29a69b37433d"), "Cremin", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6570) },
                    { 73, "Alice", new Guid("a3e73659-b905-4ae6-8c0f-3c7a0488967d"), "Yost", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6600) },
                    { 74, "Erich", new Guid("17a97170-3fa5-4a07-a33c-ec7b6a1e5ada"), "Prohaska", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6610) },
                    { 75, "Kayleigh", new Guid("6c5a6cc9-b026-4577-9d3e-d5e84f28e98c"), "Larson", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6620) },
                    { 76, "Benny", new Guid("a98556f8-196a-49f8-bd8c-7bc23b578cdb"), "Gleichner", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6640) },
                    { 77, "Sabryna", new Guid("e6149196-591c-4a64-b915-96e0ce54e8f4"), "Reichel", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6660) },
                    { 78, "Delia", new Guid("6fcb6a2c-0b52-46f1-be1e-a6f1a706572d"), "Hand", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6670) },
                    { 79, "Miguel", new Guid("605a9b41-a07b-46ee-8886-cd7b350122d3"), "Smith", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6690) },
                    { 80, "Xzavier", new Guid("b45283ed-328c-41fa-997c-90fde66d025b"), "Lueilwitz", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6710) },
                    { 81, "Fannie", new Guid("fe83b549-1736-46a6-8c09-1155ab15c7cd"), "Nicolas", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6720) },
                    { 82, "Julius", new Guid("fa0da5d2-4fbd-4302-8955-56a18609183e"), "Kautzer", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6740) },
                    { 83, "Demarco", new Guid("79fb4551-c0a2-43c1-87f0-629677ed025c"), "Hackett", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6750) },
                    { 84, "Ruby", new Guid("734789c6-3c15-4ef2-a69f-9b6781acfed5"), "Bogan", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6760) },
                    { 85, "Werner", new Guid("ee154f4c-79fa-4a63-af1c-0c6ceced8f26"), "Ernser", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6770) },
                    { 86, "Marcelo", new Guid("1a096346-1deb-4693-84b8-6da2b7133128"), "Conroy", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6780) },
                    { 87, "Macey", new Guid("feb008ce-cc6a-4adf-81ae-c10c82af1c5d"), "Pfeffer", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6800) },
                    { 88, "Joseph", new Guid("065c84b2-e4f8-4ee0-8e38-0076a3b7abfd"), "Dickens", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6810) },
                    { 89, "Johanna", new Guid("2f640e4d-0759-4ed0-ba8f-2e47905af305"), "Lemke", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6830) },
                    { 90, "Green", new Guid("da9bf79f-f599-4304-922e-321adc725b5b"), "Pfannerstill", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6840) },
                    { 91, "Preston", new Guid("09c626f1-5bf4-48fc-adf1-d66cb1649f0e"), "Metz", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6850) },
                    { 92, "Raina", new Guid("d42577f0-20b9-4c59-9fc5-401309ccef2c"), "Leffler", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6860) },
                    { 93, "Jasper", new Guid("f7aa566d-2971-4269-be09-53f64017209a"), "Tillman", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6870) },
                    { 94, "Garth", new Guid("beae47c0-ce9f-440f-b0d0-dfe6517e7aeb"), "Abshire", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6890) },
                    { 95, "Susanna", new Guid("d11f7eaa-ea03-4057-95f8-0f9549a4c540"), "Howe", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6900) },
                    { 96, "Candida", new Guid("35d8c89a-60ba-47fd-9d08-aab9d2855b9f"), "McKenzie", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6910) },
                    { 97, "Nia", new Guid("40c59b2c-ebfd-4643-813e-1b5a75d27ae6"), "Kuvalis", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6920) },
                    { 98, "Waldo", new Guid("6d6a57cf-bcd3-40ae-b316-703d4214c6eb"), "Beahan", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6930) },
                    { 99, "Lessie", new Guid("7773ffcc-cce8-4bbe-947e-9c44940a4f81"), "Hoppe", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6940) },
                    { 100, "Nash", new Guid("28a0e416-3b4d-424a-9701-2bb5d18fe7a2"), "Quigley", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6950) },
                    { 101, "Waylon", new Guid("4e872555-62fe-4871-8704-850f11d46d13"), "Abernathy", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6970) },
                    { 102, "Don", new Guid("1548c350-e9f1-4710-a5fe-df3f7d3c5e01"), "West", new DateTime(2024, 8, 8, 18, 51, 14, 94, DateTimeKind.Local).AddTicks(6980) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Guid", "LastUpdated", "Title" },
                values: new object[,]
                {
                    { 1, new Guid("89f86caa-c18a-4312-b35b-1936b54c5637"), new DateTime(2024, 8, 8, 18, 51, 14, 92, DateTimeKind.Local).AddTicks(7550), "Admin" },
                    { 2, new Guid("d4411257-2532-456d-8f7a-e39840bf6581"), new DateTime(2024, 8, 8, 18, 51, 14, 92, DateTimeKind.Local).AddTicks(7620), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Guid", "LastUpdated", "Password", "PersonId", "Score", "Username" },
                values: new object[,]
                {
                    { 1, new Guid("0434f39a-09c9-43ab-a420-b685f99d82c7"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(8630), "b6766fafe72f0c89d1f95e65d3e7ae6c8a90de36a390e44acaec3af65f04ed06c29881dda3e8b091639b8243ef3d839a17c243c8e94b0fa145b667740724119f", 1, 0, "sepehr_frd" },
                    { 2, new Guid("a6416094-3139-42cf-ba20-2adebac851f4"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9000), "4cc6b8c45dc13ffecb5a36c0f301de6f47eafb64437730a5f163b8d770fd0d6e0302c51cf9c600dfbd9991030343c53c9a06c1fc27642e66968405e67013ef32", 2, 0, "bernard_cool" },
                    { 3, new Guid("351eca14-62d8-4c23-9371-d8e4a1c2a10d"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9060), "BIEmg15D8A", 3, 22, "Payton_Witting34" },
                    { 4, new Guid("10a33ca5-8e7c-45f0-84c0-e88f7a4959e7"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9550), "W_4SdQ0_zo", 4, 17, "Ulices57" },
                    { 5, new Guid("cfc54ced-ec9d-4767-a7e7-527da3948f51"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9640), "hhaNQyuRkp", 5, 11, "Magali_Willms" },
                    { 6, new Guid("d562bae6-7d54-4660-8268-4614d335f088"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9730), "pt_skSBfa0", 6, 38, "Darion.White" },
                    { 7, new Guid("237e242c-76ba-4678-ba0c-470b10e901bc"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9800), "Mw7vOudwMp", 7, 18, "Cecelia_Schmeler13" },
                    { 8, new Guid("0d4211ba-d14c-4561-8023-3e140a1c635e"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9870), "5GyEKeRLeg", 8, 45, "Ashlee44" },
                    { 9, new Guid("05740fb3-556d-46f9-b578-5578568b0412"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9930), "JJX4EJBSWe", 9, 46, "Jordan22" },
                    { 10, new Guid("8e8e3bad-1439-44c1-ae52-5f824b19b9c9"), new DateTime(2024, 8, 8, 18, 51, 14, 95, DateTimeKind.Local).AddTicks(9980), "7oF9rlzhjf", 10, 48, "Elias_Price" },
                    { 11, new Guid("0e060236-92bf-476a-bb78-9b83a7dab504"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(40), "VU2Mv0rowC", 11, 30, "Rodolfo.Metz45" },
                    { 12, new Guid("947caced-2438-4054-bf64-ef8fe8ce9444"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(110), "kb9Yy4atuh", 12, 50, "Clarissa64" },
                    { 13, new Guid("dc553360-11b3-4fc5-a088-604edd9ade7d"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(190), "LemOjFdhWz", 13, 40, "Laisha_Mante" },
                    { 14, new Guid("3f8e0a46-e7ab-4f4f-8dbc-3e521c017b88"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(250), "bYekdgCPhP", 14, 13, "Vallie_Quitzon" },
                    { 15, new Guid("420f00e3-a744-441f-a6da-b3fff3a492e0"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(310), "xLMvnWtCbY", 15, 24, "Johnpaul_Lang" },
                    { 16, new Guid("1c547594-dac0-4f3a-a112-ed3a84eaddc4"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(370), "HHD4lhhdiS", 16, 32, "Arthur91" },
                    { 17, new Guid("9f6b8a2d-8897-41ef-8415-8a21e3e61de5"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(430), "89vA9IKM0v", 17, 26, "Gregorio_Kunze75" },
                    { 18, new Guid("76c1e9a4-40e0-4077-8f5c-e5526bc5623f"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(490), "AIH86b59J7", 18, 1, "Sarah_Koss" },
                    { 19, new Guid("355ee4a6-444f-4705-aa30-27e0a562d875"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(540), "Crl8XiuLvO", 19, 19, "Ewald19" },
                    { 20, new Guid("47c16a23-1555-4b2e-89ed-7fcfd202b63e"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(610), "I_hSodP_m8", 20, 45, "Iliana85" },
                    { 21, new Guid("621ab3fe-e361-434a-bb4d-b38a15bbae0d"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(670), "gYhi7YmnDx", 21, 12, "Marielle30" },
                    { 22, new Guid("5bf2c09e-e643-4566-97ac-f48ad9ac5e87"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(730), "0QjSqDNH5T", 22, 39, "Una_Gottlieb" },
                    { 23, new Guid("ace9d4fa-5527-4aff-a844-6ee1a8619747"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(780), "QbX0eNAg0W", 23, 7, "Eleanore.Krajcik90" },
                    { 24, new Guid("2a9d7702-6fed-4ed6-b117-bb2651c9feb6"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(840), "K1bvO8Sh1v", 24, 0, "Marcelino_Emard46" },
                    { 25, new Guid("5c20862b-327d-4cf4-a3a8-ca31e7a72d90"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(900), "2oJ9QxPXhH", 25, 14, "Kole_Ernser27" },
                    { 26, new Guid("1f01181b-32a3-4f85-9c5b-ccf8496ccb90"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(960), "qVGejI1poA", 26, 20, "Bella.Frami78" },
                    { 27, new Guid("e5611bb1-4b66-48e1-beb1-3304f017b175"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1010), "EdInansZX4", 27, 43, "Mustafa.Kilback38" },
                    { 28, new Guid("03ffbef8-5389-44cd-98fc-9227d3de6d1a"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1070), "X0CejvF4Si", 28, 29, "Flo67" },
                    { 29, new Guid("17c6fb20-2b14-4af0-abc8-09d6813283e0"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1140), "bnYqmVZrGY", 29, 15, "Stephen66" },
                    { 30, new Guid("2a7db2d5-b5c6-46e4-82a0-8f0b26c74b97"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1230), "ocDZzOEg4V", 30, 45, "Raphaelle.Macejkovic" },
                    { 31, new Guid("f0d467e6-41cc-474d-b4a5-99b25f3fc148"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1290), "9HJmbnKbJr", 31, 28, "Mikayla71" },
                    { 32, new Guid("f00e04d4-5f46-4474-98ce-fab4ecd42b63"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1340), "1Gm_rOEnVO", 32, 36, "Abbey83" },
                    { 33, new Guid("ab412a17-375a-475a-8c43-1af15e0d362b"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1390), "6wepjTN9Na", 33, 26, "Wilton_Anderson" },
                    { 34, new Guid("8fc03f48-8d39-402c-9b1a-4ba2fe407603"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1450), "6pXoxfM79A", 34, 19, "Ally_Purdy" },
                    { 35, new Guid("1303eaba-017e-4879-b2c6-95394360efdb"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1510), "zMNkjmTCnj", 35, 42, "Jarret.Kulas8" },
                    { 36, new Guid("25455c81-a15a-4783-8458-500d7fd57461"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1580), "scd4iwWlgW", 36, 43, "Stephen.Zulauf23" },
                    { 37, new Guid("f1818711-aa12-40ef-968c-5558ca2efea3"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1630), "cFwmrrpxuZ", 37, 43, "Aleen_Mann" },
                    { 38, new Guid("2fbea997-e9cf-4fc0-97ee-3e94bfc5f0a5"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1680), "uPvwI2mpQY", 38, 7, "Ladarius_Cremin" },
                    { 39, new Guid("9fe46967-72a9-492c-b5b4-772143fd38d1"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1740), "M7fxRmE3dn", 39, 18, "Gunner_Thompson45" },
                    { 40, new Guid("7a9b1d2f-026d-42f0-a254-aee00b4f4fd4"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1800), "9OoDR2R5k_", 40, 4, "Emmet_Runolfsson" },
                    { 41, new Guid("12476917-5d98-4f06-aee9-d37abf556c80"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1860), "oA0LHfnmF4", 41, 16, "Furman.Johnson" },
                    { 42, new Guid("426f015a-1769-4470-bb78-34dba60ed6bc"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1910), "RNr0OE0fXJ", 42, 29, "Lexi.Murphy58" },
                    { 43, new Guid("518a145f-688e-4a03-b807-40e312f51a3c"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(1980), "_p23iXf5M7", 43, 42, "Nick_Schneider" },
                    { 44, new Guid("cf7f168e-caf6-441d-a7b8-37ea63d8bcd2"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2030), "ZY9ingQ9oZ", 44, 29, "Stuart.Harris18" },
                    { 45, new Guid("a7703683-b3cf-4c3d-85c3-96cf340ea047"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2090), "Erq683Izy6", 45, 17, "Amos42" },
                    { 46, new Guid("30f9a947-f25d-4ea4-a5c2-3250eac39cfd"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2140), "_9vE4evC8t", 46, 23, "Mathew.Becker" },
                    { 47, new Guid("d51ce74f-02fb-4a1e-b0fd-e208faaf99b3"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2200), "Q1OvbDUNY5", 47, 30, "Laney.Rice86" },
                    { 48, new Guid("09cabaf2-7148-4fde-84a6-61ea4f071fa1"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2240), "klypJBUT9w", 48, 5, "Dedric.Torphy30" },
                    { 49, new Guid("2cd224a2-5edb-4139-9ee1-2e86087f3179"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2310), "5iltG8oMtf", 49, 45, "Jonathan.Mann" },
                    { 50, new Guid("05ae8bb4-6f3a-4b2a-868e-a1d9f0943363"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2370), "lEswJXbi42", 50, 48, "Stephen_Kulas61" },
                    { 51, new Guid("9820025c-1ab6-4877-b5ac-84199e40a270"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2420), "sC0VeCDyTh", 51, 26, "Shyanne.Weber" },
                    { 52, new Guid("be6c1bcd-25ae-4c64-a682-d07b083f9d03"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2490), "NmUY1BBYfV", 52, 10, "Hipolito.Larson" },
                    { 53, new Guid("0b10bc16-d5e3-43a0-a400-e50479718088"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2540), "pE5ysQWBwz", 53, 19, "Christina_Blanda" },
                    { 54, new Guid("ed947af1-9e0b-410f-86d3-948ae35bde74"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2620), "eVrGiSBjvF", 54, 33, "Summer2" },
                    { 55, new Guid("5edd6fde-495f-447c-8e52-78fd026b1519"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2670), "NY7V67ptS_", 55, 20, "Madaline.Purdy0" },
                    { 56, new Guid("ab285ba3-f994-43c9-ace6-a80d833b556b"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2720), "jordz0sbxE", 56, 6, "Linwood.Ebert" },
                    { 57, new Guid("29ec307d-17b9-4818-8982-4b3a5e4c1ccb"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2780), "90zuI4gMum", 57, 19, "Cicero_Luettgen54" },
                    { 58, new Guid("25a5aab9-31b1-4d14-927e-f30b9f3bd112"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2840), "itsUrxkRk_", 58, 5, "Duane.Bogisich95" },
                    { 59, new Guid("79edb3eb-30b4-4da4-b93f-d24924ab513f"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2890), "zK1pudD8lV", 59, 29, "Caesar32" },
                    { 60, new Guid("0b323aec-1e5c-449e-a4d5-130191757617"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(2950), "PO_R5XF0X9", 60, 44, "Scot.Rolfson" },
                    { 61, new Guid("8f6c5798-2a23-4b05-a00e-b8e0009796e4"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3000), "g_9FLb6ELR", 61, 48, "Annalise.Bode49" },
                    { 62, new Guid("723690e1-344a-4e0a-95f3-da10043d25f4"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3060), "UBFB5hLNef", 62, 49, "Beulah.Bergnaum42" },
                    { 63, new Guid("f25ae8c0-4414-4ecc-b427-5d7d3410efdd"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3120), "aDyjiu2eu7", 63, 48, "Bulah.West42" },
                    { 64, new Guid("f858c143-f25e-441e-ad6e-f7de4f967d3b"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3190), "C9oUfAlNem", 64, 47, "Darrin22" },
                    { 65, new Guid("d5cf0437-235b-4832-95e6-66bd5ac955f3"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3240), "7IihnT2NH9", 65, 26, "Claudia.Gutkowski" },
                    { 66, new Guid("ff142e42-748d-4bde-82d5-2ae50266765c"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3300), "kHWDAlXzWl", 66, 14, "Annabell.Monahan" },
                    { 67, new Guid("cb698283-d094-42bb-8ba1-376c83cb4a84"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3360), "69tCqZQM8S", 67, 27, "Vance.MacGyver" },
                    { 68, new Guid("0f059e95-9b4e-4bf4-90af-66ca25413c47"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3410), "c6jhlLLN0j", 68, 39, "Billy_Gottlieb" },
                    { 69, new Guid("5ebfedb8-f327-42e7-85a3-18577f9e68c9"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3470), "tp7wyGeunV", 69, 2, "Destinee.Medhurst" },
                    { 70, new Guid("b873d00a-0c60-4111-858b-baaa08ee608b"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3540), "DopOJ7Wc8E", 70, 50, "Nicholaus.Deckow" },
                    { 71, new Guid("182ac601-1833-4b35-86c8-0b13260c6716"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3590), "HzuqP4_qWy", 71, 13, "Cielo44" },
                    { 72, new Guid("ff13c013-e532-479b-96eb-c9f76966ae31"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3640), "0iUiISf5xz", 72, 45, "Melody59" },
                    { 73, new Guid("0b504f47-61fb-4204-bae4-e66ed193f5bb"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3700), "TMdnZ4soex", 73, 15, "Darrell.Funk87" },
                    { 74, new Guid("c5bfd4eb-e5ba-409b-9c8c-c485540a5987"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3760), "HHWZKLSYnY", 74, 45, "Dylan20" },
                    { 75, new Guid("bd8d42e5-84af-4f73-a946-283efa5dc628"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3800), "STHxJ37NiM", 75, 14, "Leora30" },
                    { 76, new Guid("0493071a-b929-4631-9393-19d295775f3a"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3850), "2_utNJ2DYB", 76, 39, "Murl.Hoeger64" },
                    { 77, new Guid("527d6120-ef2f-4938-98b4-7d87441550b9"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3920), "U1NLNvBgdv", 77, 39, "Adonis41" },
                    { 78, new Guid("4ae266ca-cc89-47e5-9f95-8f607ad068f8"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(3980), "5BvaAtUbIT", 78, 15, "Brice_Rath" },
                    { 79, new Guid("9de43fce-ba30-480b-bbe9-4fcd1878c95b"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4030), "ASzHgiuTmq", 79, 23, "Linwood50" },
                    { 80, new Guid("4f2d9c67-f22c-43b0-a8fe-95df0deb59a2"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4080), "i0BXq7BaXI", 80, 42, "Gay_Donnelly38" },
                    { 81, new Guid("9b1324d9-f06f-4d3c-b508-f242096858ff"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4130), "wqEZbUaZTd", 81, 6, "Darrel75" },
                    { 82, new Guid("a8acbf4f-d5e9-4972-9bb7-6c1a17ff6770"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4190), "MwwWKKwjvF", 82, 48, "Esperanza.Bergnaum90" },
                    { 83, new Guid("1f8d8412-5a05-42d4-8b53-22b4e969e8bb"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4240), "I8aV5tp_T4", 83, 20, "Laurianne.Robel5" },
                    { 84, new Guid("b3ff4309-5e6d-4877-b32b-935bf3b42f9d"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4330), "j9WG2cod2O", 84, 6, "Fredy_Von36" },
                    { 85, new Guid("7409cf4b-ef3b-4dc2-9f7d-2c4d5d344541"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4380), "CmMLEox4bl", 85, 12, "Shyanne_Zulauf" },
                    { 86, new Guid("ed6b7058-f3ff-460a-be14-7a551715d023"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4430), "EhddeEQ5Cn", 86, 13, "Milan20" },
                    { 87, new Guid("27da0179-ded9-4a91-b2dc-8eceaecee7a1"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4490), "iOcodLqBSW", 87, 0, "Laverna77" },
                    { 88, new Guid("140dd0e0-4122-47ba-84d4-c04ef4494277"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4540), "HGai7WrBTN", 88, 10, "Rodrick.Prohaska" },
                    { 89, new Guid("70090651-cfc1-40fd-9e25-97df64aebd11"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4590), "MP7YsxbBRq", 89, 25, "Esta_Armstrong" },
                    { 90, new Guid("05ea2e88-66bb-4758-b563-117201ccce80"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4640), "Y_7UXkEiU8", 90, 22, "Foster.Grady" },
                    { 91, new Guid("8c174c40-e05d-4ee6-a370-a1bf964257a2"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4710), "dntl4boAx5", 91, 33, "Kyleigh_Considine" },
                    { 92, new Guid("cd3b9c0a-11e3-4775-a423-8617c130071f"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4760), "9Pg_vyDztM", 92, 7, "Agustina_Gleason" },
                    { 93, new Guid("5edd5c93-b091-4888-a892-92eb2d6329c1"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4820), "ev8P6WgTCm", 93, 45, "Tanner77" },
                    { 94, new Guid("e86d7411-21e5-4c00-abe6-2fe107897f95"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4880), "U2b2qUJWSp", 94, 38, "Kolby_Jenkins7" },
                    { 95, new Guid("ec84f504-bfe6-4c94-9b2c-221592bdc24d"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4930), "T0ieR0Zimu", 95, 50, "Drew_Champlin74" },
                    { 96, new Guid("7d2da33a-3e0c-42e6-9b6e-c9f88a6dfc87"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(4990), "6YDzEwgsLm", 96, 22, "Vida_Rohan71" },
                    { 97, new Guid("a3a3a423-746c-42c4-a306-a4b7c0c0aae4"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5030), "1mtuBobgl9", 97, 31, "Corrine.McLaughlin" },
                    { 98, new Guid("367f85ac-dcd2-4bd6-b259-1b331fd8a459"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5100), "8fBPtWddEk", 98, 0, "Mariela11" },
                    { 99, new Guid("72ba57e5-4978-45e9-8d77-8819d06cc792"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5160), "AqLXE7fGSW", 99, 6, "Humberto.Reichel11" },
                    { 100, new Guid("df029053-1f83-4cba-aa50-3919d914dd9f"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5210), "AJ1ylXSDUd", 100, 5, "Pierre54" },
                    { 101, new Guid("e0f648a7-2da2-4dbb-9976-9e4dd9c55c92"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5270), "1GXH4JD95c", 101, 0, "Zackery.Mante16" },
                    { 102, new Guid("af1eaa37-a34c-4d87-a0e6-0b379b375714"), new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5320), "u_w7Q213Ju", 102, 7, "Shayna80" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Bio", "Email", "LastUpdated", "UserId" },
                values: new object[,]
                {
                    { 1, ".NET Developer", "sepfrd@outlook.com", new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5480), 1 },
                    { 2, "React Developer", "bercool@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 96, DateTimeKind.Local).AddTicks(5490), 2 },
                    { 3, "Legacy Functionality Technician", "Ian.Krajcik68@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(7160), 3 },
                    { 4, "Regional Functionality Strategist", "Laurianne98@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8210), 4 },
                    { 5, "Lead Functionality Supervisor", "Zita.Blanda24@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8270), 5 },
                    { 6, "Customer Functionality Manager", "Tracy.Kshlerin2@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8300), 6 },
                    { 7, "Regional Solutions Architect", "Roberto60@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8350), 7 },
                    { 8, "Forward Research Liaison", "Mitchell.Conroy@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8390), 8 },
                    { 9, "Investor Response Engineer", "Lurline_Reichert78@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8430), 9 },
                    { 10, "Internal Optimization Facilitator", "Christiana85@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8470), 10 },
                    { 11, "Investor Response Developer", "Gaston_Murphy@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8530), 11 },
                    { 12, "Product Usability Developer", "Triston.Ferry@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8570), 12 },
                    { 13, "National Paradigm Orchestrator", "Isadore40@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8610), 13 },
                    { 14, "Direct Accountability Administrator", "Angela4@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8650), 14 },
                    { 15, "Central Integration Architect", "Dominic_Reichel42@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8690), 15 },
                    { 16, "Legacy Paradigm Planner", "Dangelo_Hyatt12@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8730), 16 },
                    { 17, "Regional Factors Supervisor", "Felicita.Cremin82@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8800), 17 },
                    { 18, "Dynamic Communications Engineer", "Ashley59@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8860), 18 },
                    { 19, "Central Response Consultant", "Ryleigh.Botsford@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8890), 19 },
                    { 20, "Principal Tactics Specialist", "Araceli.Carter43@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8930), 20 },
                    { 21, "International Tactics Director", "Blaze.Ryan@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(8980), 21 },
                    { 22, "Chief Usability Strategist", "Caesar_Kiehn@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9020), 22 },
                    { 23, "Customer Accounts Supervisor", "Kraig.Frami44@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9050), 23 },
                    { 24, "Principal Intranet Supervisor", "Grayce61@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9090), 24 },
                    { 25, "Investor Accounts Specialist", "Granville44@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9120), 25 },
                    { 26, "Legacy Brand Producer", "Everardo.Heaney@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9180), 26 },
                    { 27, "National Intranet Director", "Laury.Littel@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9220), 27 },
                    { 28, "Principal Brand Facilitator", "Emmanuel_Ullrich57@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9250), 28 },
                    { 29, "Future Interactions Administrator", "Drew.Murphy22@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9290), 29 },
                    { 30, "International Web Consultant", "Mossie_Towne86@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9330), 30 },
                    { 31, "Dynamic Data Orchestrator", "Effie26@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9360), 31 },
                    { 32, "Principal Mobility Representative", "Amani_Morar42@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9400), 32 },
                    { 33, "Principal Data Director", "Kristin_Gislason67@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9430), 33 },
                    { 34, "Forward Accounts Representative", "Norval_Miller22@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9490), 34 },
                    { 35, "Global Response Technician", "Rebeka_Collier@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9530), 35 },
                    { 36, "Global Usability Producer", "Malinda.Rippin@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9580), 36 },
                    { 37, "Future Infrastructure Facilitator", "Gonzalo99@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9620), 37 },
                    { 38, "Future Factors Analyst", "Colleen46@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9650), 38 },
                    { 39, "Central Metrics Coordinator", "Annie.Mills66@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9690), 39 },
                    { 40, "Legacy Program Designer", "Bettie.Hirthe44@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9720), 40 },
                    { 41, "Lead Usability Developer", "Tyrese.Homenick@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9770), 41 },
                    { 42, "Human Infrastructure Designer", "Madisyn.Hegmann@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9810), 42 },
                    { 43, "Dynamic Assurance Engineer", "Gwen32@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9840), 43 },
                    { 44, "Dynamic Interactions Orchestrator", "Jermain_Gorczany@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9880), 44 },
                    { 45, "District Identity Supervisor", "Samir94@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9930), 45 },
                    { 46, "Regional Directives Designer", "Ramon.Wintheiser@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 97, DateTimeKind.Local).AddTicks(9960), 46 },
                    { 47, "District Quality Agent", "Mylene_Kerluke6@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local), 47 },
                    { 48, "Global Branding Supervisor", "Davon.Turner37@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(40), 48 },
                    { 49, "Dynamic Solutions Strategist", "Reymundo.Blanda@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(90), 49 },
                    { 50, "Forward Optimization Coordinator", "Dagmar_Cremin13@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(130), 50 },
                    { 51, "Central Applications Architect", "Maximus_Berge@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(160), 51 },
                    { 52, "Dynamic Data Liaison", "Moriah11@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(200), 52 },
                    { 53, "Lead Security Manager", "Ryleigh_Kihn71@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(240), 53 },
                    { 54, "National Directives Developer", "Keshaun.Beatty@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(270), 54 },
                    { 55, "Senior Infrastructure Orchestrator", "Lenora_Doyle@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(310), 55 },
                    { 56, "Internal Interactions Engineer", "Arianna0@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(340), 56 },
                    { 57, "Lead Research Planner", "Valentine69@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(400), 57 },
                    { 58, "International Branding Manager", "Yoshiko23@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(440), 58 },
                    { 59, "Senior Optimization Administrator", "Quinn_Rath18@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(480), 59 },
                    { 60, "International Mobility Director", "Rollin4@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(510), 60 },
                    { 61, "International Marketing Agent", "Lon_OReilly@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(550), 61 },
                    { 62, "District Functionality Analyst", "Lorenza_Champlin8@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(600), 62 },
                    { 63, "Forward Solutions Engineer", "Lafayette_Legros62@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(650), 63 },
                    { 64, "Central Brand Designer", "Ferne.Morar23@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(690), 64 },
                    { 65, "Dynamic Accountability Supervisor", "Francis19@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(730), 65 },
                    { 66, "Global Communications Associate", "Alexys_Howell73@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(800), 66 },
                    { 67, "Customer Integration Orchestrator", "Shanon_Hauck57@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(840), 67 },
                    { 68, "Forward Intranet Orchestrator", "Kaleb71@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(880), 68 },
                    { 69, "Product Directives Coordinator", "Loyce.Schinner25@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(910), 69 },
                    { 70, "Forward Web Orchestrator", "Izabella.Casper9@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(960), 70 },
                    { 71, "Forward Quality Representative", "Isaias73@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1000), 71 },
                    { 72, "National Branding Representative", "Else_Ruecker@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1040), 72 },
                    { 73, "Lead Configuration Designer", "Angelica_Corkery47@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1080), 73 },
                    { 74, "Legacy Division Officer", "Danial.Moen53@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1120), 74 },
                    { 75, "Dynamic Markets Analyst", "Reginald85@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1150), 75 },
                    { 76, "District Identity Liaison", "Leon89@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1190), 76 },
                    { 77, "Human Communications Architect", "Stanton.Johnson46@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1230), 77 },
                    { 78, "Forward Configuration Analyst", "Austyn.Mosciski@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1270), 78 },
                    { 79, "Future Research Representative", "Kay_Smith22@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1310), 79 },
                    { 80, "Dynamic Program Specialist", "Jamel_Hahn20@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1340), 80 },
                    { 81, "Forward Infrastructure Technician", "Jairo15@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1380), 81 },
                    { 82, "Customer Interactions Agent", "Efren.Kihn11@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1410), 82 },
                    { 83, "Future Brand Technician", "Bette74@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1440), 83 },
                    { 84, "Central Branding Analyst", "Viola.Hyatt@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1480), 84 },
                    { 85, "International Accounts Officer", "Gloria.Reilly96@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1520), 85 },
                    { 86, "Product Quality Producer", "Lyla32@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1560), 86 },
                    { 87, "Lead Usability Administrator", "Sabrina67@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1590), 87 },
                    { 88, "Global Directives Manager", "Zita_McLaughlin@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1630), 88 },
                    { 89, "Dynamic Branding Strategist", "Lauriane_Paucek@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1670), 89 },
                    { 90, "Dynamic Directives Planner", "Mina82@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1710), 90 },
                    { 91, "Human Solutions Coordinator", "Ethan95@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1740), 91 },
                    { 92, "Regional Data Facilitator", "Devon35@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1770), 92 },
                    { 93, "Product Factors Consultant", "Gaylord37@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1830), 93 },
                    { 94, "Internal Quality Specialist", "Cristina41@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1870), 94 },
                    { 95, "Direct Accounts Assistant", "Stuart26@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1910), 95 },
                    { 96, "Human Optimization Manager", "Amaya.Ziemann@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1940), 96 },
                    { 97, "Product Optimization Consultant", "Jada.Konopelski77@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(1980), 97 },
                    { 98, "Future Interactions Strategist", "Emely_Rau@hotmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2020), 98 },
                    { 99, "Investor Branding Consultant", "Jayden_Upton@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2050), 99 },
                    { 100, "Legacy Division Specialist", "Uriel.Krajcik80@yahoo.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2090), 100 },
                    { 101, "Global Factors Engineer", "Elvis_Lindgren@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2140), 101 },
                    { 102, "Corporate Accountability Agent", "Morgan.Ebert@gmail.com", new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2180), 102 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Description", "Guid", "LastUpdated", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("cee81c73-3e2d-411d-9546-68e703a7a590"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(650), "Checking Account Kids", 64 },
                    { 2, "The Football Is Good For Training And Recreational Purposes", new Guid("c951f910-b3a7-4bbe-835f-ed521d45793c"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1550), "e-tailers analyzer", 5 },
                    { 3, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("254ff076-dfc3-4483-85ab-0e1374bce436"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1610), "indexing Operations", 98 },
                    { 4, "The Football Is Good For Training And Recreational Purposes", new Guid("85481ce9-6b72-4759-b36d-5b839f4c3f67"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1640), "actuating vertical", 77 },
                    { 5, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("4c404081-10f9-47f1-8cb8-2adb9eded5bc"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1670), "Concrete Architect", 54 },
                    { 6, "The Football Is Good For Training And Recreational Purposes", new Guid("42b9dd6c-ecb2-44bf-a288-3ea64067b3e8"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1690), "frame deliver", 67 },
                    { 7, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("073ead80-a484-4da7-999a-1a0f380163c9"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1710), "Practical teal", 54 },
                    { 8, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("8360e9dd-eda9-4192-ad85-c7333ef26acd"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1740), "Mobility card", 44 },
                    { 9, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("0ec3d798-62c7-452a-bf42-086991bae726"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1760), "Borders integrate", 97 },
                    { 10, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("11ba6fe4-97c4-4e77-b498-4072f6a59753"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1790), "withdrawal Pataca", 74 },
                    { 11, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("0eb22a8a-7bc9-46ea-b47a-bda1f60906f9"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1830), "virtual Handmade Fresh Towels", 72 },
                    { 12, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("7051f922-912c-49b0-bd38-4f42c1d9a402"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1860), "Optimization Ukraine", 68 },
                    { 13, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("7969533c-3daa-4403-90c1-5e6d3d797848"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1890), "generating Ergonomic Cotton Tuna", 17 },
                    { 14, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("77d6bfca-9af5-48e9-a4d0-a23bd5934df9"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1920), "Cotton Grocery, Sports & Automotive", 32 },
                    { 15, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("c5ce9b6d-3306-4b78-99db-7976ef62220e"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(1970), "synthesize Bermuda", 57 },
                    { 16, "The Football Is Good For Training And Recreational Purposes", new Guid("491558dd-6240-4e49-b6ba-1f8bfcb1026a"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2010), "synergize Practical Fresh Mouse", 77 },
                    { 17, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("59aa51a0-ecf0-4077-82fc-2099d31a69e5"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2030), "Practical Frozen Pants programming", 61 },
                    { 18, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("92ddc3a4-f349-4841-9b94-ecae438e30d1"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2060), "Savings Account HTTP", 68 },
                    { 19, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("68e9fc50-b2b8-4d12-887b-c15adc6a136d"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2090), "indigo Rustic", 25 },
                    { 20, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("3c8c615b-6c5e-46e6-a72c-65b6b42a9d4f"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2100), "Agent synergize", 64 },
                    { 21, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("6a50d658-19e9-4cb0-af63-f1c602ceb82b"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2120), "Gorgeous Rubber Chips plum", 95 },
                    { 22, "The Football Is Good For Training And Recreational Purposes", new Guid("39f855c1-c6eb-4d44-8936-df044ce73e0a"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2140), "Hawaii Shoes & Home", 8 },
                    { 23, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("4e3afd08-c537-4efe-a310-bd85e5ac2462"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2170), "Outdoors & Grocery Practical Plastic Soap", 93 },
                    { 24, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("6b3a8360-346b-4493-9bfb-a7dacfe24bb5"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2200), "Mountain sky blue", 60 },
                    { 25, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("ac275c6a-5f3f-4280-9966-7a2c21faa5d7"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2220), "payment Missouri", 70 },
                    { 26, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("6115f8da-c140-45e1-8cac-e8344123ffbc"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2240), "Licensed Metal Keyboard frictionless", 63 },
                    { 27, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("b4dc8935-898e-49b4-9957-a565facbc8b5"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2270), "array Armenian Dram", 18 },
                    { 28, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("9470fff4-fb20-4311-8019-909f401fb77a"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2310), "Movies & Shoes generate", 64 },
                    { 29, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("b8571c5b-d1e1-4514-9ef8-7b2ddc0a146e"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2330), "Orchestrator Martinique", 54 },
                    { 30, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("0fe267d3-801d-4cdc-bec7-ea32e7732047"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2350), "National Industrial & Kids", 49 },
                    { 31, "The Football Is Good For Training And Recreational Purposes", new Guid("069fbec1-2835-4b7b-ad74-6369dc2f5830"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2370), "Avon e-markets", 66 },
                    { 32, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("f7621fb1-5b96-4b34-ad2c-4c1a42e859f7"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2390), "neural Tools", 22 },
                    { 33, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("557e8bed-0726-43d9-80d7-a2242feae35d"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2410), "Internal reboot", 47 },
                    { 34, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("6a8af461-161a-4740-a6e8-d7f2d1c8a7dd"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2450), "action-items Dynamic", 47 },
                    { 35, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("cdd800bc-4a25-4c28-9163-e9a52b5d1d07"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2460), "Palladium grey", 75 },
                    { 36, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("c93f1cbd-5dc7-4f64-b413-7855e80adbcb"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2490), "Road SDD", 80 },
                    { 37, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("5479c1b1-a5b3-4d66-a58e-f73f8f5ba080"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2510), "generating redundant", 46 },
                    { 38, "The Football Is Good For Training And Recreational Purposes", new Guid("ba0f056d-c814-4fe6-ba89-2c79e13fe164"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2530), "Rustic Buckinghamshire", 60 },
                    { 39, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("06a0e3f6-e1f6-47cf-9830-65925b9c9d19"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2540), "Executive Investment Account", 4 },
                    { 40, "The Football Is Good For Training And Recreational Purposes", new Guid("c904ab2c-f940-4d18-bc9f-27c662436f07"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2560), "extend Refined Cotton Hat", 39 },
                    { 41, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("73609ad0-b89c-4269-936c-df8c0af5ef38"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2590), "e-services Borders", 7 },
                    { 42, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("4074fbea-4fe8-41c6-9bc4-13fe507be99f"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2600), "Squares mobile", 79 },
                    { 43, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("d80db77a-3aa7-4ec5-bc5b-e93e80d51d5d"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2620), "back-end primary", 22 },
                    { 44, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("bd9644c8-320f-4aaf-b065-d4ea48b88caa"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2640), "Compatible Wisconsin", 22 },
                    { 45, "The Football Is Good For Training And Recreational Purposes", new Guid("7f78e0a7-c19a-4065-802a-5307a912dcd5"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2660), "azure Fantastic Cotton Pizza", 40 },
                    { 46, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("72f67748-1845-45c6-b48c-934c675888e7"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2680), "Licensed Concrete Tuna Communications", 31 },
                    { 47, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("60b4ff56-d68a-4378-b04d-01a558317fd0"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2700), "aggregate copying", 42 },
                    { 48, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("4eade778-2341-4380-b49a-23ade407dbaf"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2720), "Kids, Baby & Computers Lane", 37 },
                    { 49, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("80ed63c1-38d3-450b-8196-3f226364cd6f"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2750), "Islands Gorgeous Rubber Bike", 32 },
                    { 50, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("6a9e975c-9198-4b7b-9572-add9d2183acd"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2770), "Senior Persistent", 94 },
                    { 51, "The Football Is Good For Training And Recreational Purposes", new Guid("84a401da-a729-4b33-90b8-ceb61f6114d3"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2790), "Rustic Cotton Sausages Loop", 46 },
                    { 52, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("653b3d3b-2537-4d25-9c9e-7e34813cb222"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2830), "Research Unbranded", 5 },
                    { 53, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("bcb27b20-cfea-4707-a236-4417ee537ac3"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2850), "neural back up", 72 },
                    { 54, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("ce13a629-5f44-4ef5-9981-9f816c867e18"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2860), "Programmable Bedfordshire", 5 },
                    { 55, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("f89b214f-4fef-4296-8ae3-88b9623a479e"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2880), "protocol Automotive, Movies & Sports", 74 },
                    { 56, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("ffd1a7b0-47d7-4fb7-9830-ae767c36ecd7"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2900), "New Jersey Borders", 71 },
                    { 57, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("92eb9eba-578b-4919-9102-d9e1bf8e8bb1"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2920), "Outdoors synergistic", 41 },
                    { 58, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("403d1217-78b0-413f-b151-d12f2e5c07f9"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2940), "sexy Run", 33 },
                    { 59, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("ebfe7067-f54f-4bb6-9a71-3cb9ce64cdf7"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2960), "yellow Borders", 32 },
                    { 60, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("31cc79a2-525c-44b3-814d-807f24557e2b"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(2970), "Metrics Music, Jewelery & Jewelery", 24 },
                    { 61, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("3b44d37f-0905-4cd4-a1ae-1f2d94c3f7c2"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3000), "scalable wireless", 55 },
                    { 62, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("df0295fe-d699-4800-8997-6dd020eae65c"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3020), "Handcrafted Armenian Dram", 68 },
                    { 63, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("84b44a1b-de91-4e38-b1ca-63a90ff016b2"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3030), "Awesome Cotton Cheese Cambridgeshire", 8 },
                    { 64, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("98139eb9-99b2-4972-bd3f-9c9802716c30"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3060), "Kids index", 64 },
                    { 65, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("8bf6fa4e-3ec8-4b57-bd82-76b17fd7f8ac"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3070), "Synergistic Islands", 88 },
                    { 66, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("eb30917c-8f6e-4aa0-b9aa-ffa66c9f5237"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3090), "capacitor Aruban Guilder", 84 },
                    { 67, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("b0e59c64-575d-402a-a260-78ea4dd056c5"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3120), "Savings Account Texas", 75 },
                    { 68, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("77071658-d101-41cf-a51a-5e40c84aa103"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3130), "Handcrafted Soft Keyboard Investor", 27 },
                    { 69, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("0726ce28-be82-4c3c-ae8e-7ac69a7d60cf"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3180), "Steel array", 92 },
                    { 70, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("5c46b77d-1e97-4412-a4a1-d35cb444b336"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3190), "Clothing & Jewelery revolutionize", 96 },
                    { 71, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("04dbec1e-056b-4a2c-a919-edd6ca51279d"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3210), "Algeria Incredible Fresh Chair", 33 },
                    { 72, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("832feea3-92f2-4bce-a38f-f2442c441903"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3240), "Ergonomic Frozen Chips multi-byte", 59 },
                    { 73, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("eba32c10-9c64-454f-817d-dc75ea1858d6"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3260), "Rustic Rubber Mouse Beauty & Music", 31 },
                    { 74, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("55d737df-1a0b-4475-a585-9ed30d7e1a75"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3290), "Illinois Synergized", 59 },
                    { 75, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("b6097433-50d9-49a7-90cd-cccaea438e8d"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3300), "Route Manager", 28 },
                    { 76, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("092dde54-20d3-4126-a71c-1a999b69d649"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3320), "Direct Summit", 60 },
                    { 77, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("5682fd7b-0c4b-4998-b6e9-42816f2c8eab"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3340), "Administrator payment", 40 },
                    { 78, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("b7aa259d-3206-4069-a877-dafa8c5e0d35"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3360), "deposit reboot", 36 },
                    { 79, "The Football Is Good For Training And Recreational Purposes", new Guid("c934abb5-96e3-47f5-89bb-72158ad1227a"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3370), "architectures input", 55 },
                    { 80, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("feb166e5-e8fc-46ab-b8f5-0681055d5abf"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3390), "Steel Handmade Frozen Computer", 95 },
                    { 81, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("b3494073-1881-49c6-9dcb-0320dc51a7f2"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3410), "Internal Romania", 22 },
                    { 82, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("a6bbc106-7244-486b-a5bd-bac082693516"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3430), "Congo connect", 37 },
                    { 83, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("0e5eb4d8-193d-48d5-8a43-f94f1e918a53"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3440), "Frozen deposit", 29 },
                    { 84, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("d8459e21-61ce-4b07-80d4-46ae21e77ca0"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3460), "invoice Sri Lanka Rupee", 32 },
                    { 85, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("b4271d94-7411-41ef-898c-07dddf80a3b1"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3480), "Generic Frozen Computer Glen", 68 },
                    { 86, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("73fe9d6c-c341-4420-be02-3ebe249503b2"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3510), "Frozen online", 46 },
                    { 87, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("bb3d702a-b1a5-4f69-9e0a-25868aa86275"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3530), "Baby, Health & Baby back-end", 15 },
                    { 88, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("e224a868-d072-41f4-aceb-c917813ecb43"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3550), "District port", 31 },
                    { 89, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("f477ae14-cc35-4777-aa6b-04a8711c958a"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3570), "Intelligent Forint", 32 },
                    { 90, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("ac1a4818-f3c0-4fa7-8d13-8ea698a53233"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3590), "Intelligent Electronics", 63 },
                    { 91, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("b1f8da71-8ca7-424b-a853-a2794cd2ef5f"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3610), "needs-based pixel", 77 },
                    { 92, "The Football Is Good For Training And Recreational Purposes", new Guid("fc9b4fcd-7132-425e-8285-6a9b2570e36e"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3620), "capacitor invoice", 70 },
                    { 93, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("b649c391-e2ce-4c1c-8ee5-cf108b399472"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3640), "Regional Coordinator", 54 },
                    { 94, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("0a9f3941-a3f7-486c-883f-a929b4d7e8cb"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3660), "IB Compatible", 79 },
                    { 95, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("a842b41b-8fe2-4498-9445-bfb52ec7185b"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3670), "Refined Wooden Shoes Pennsylvania", 79 },
                    { 96, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("0e118ccb-a345-42fd-8488-77e7aad5b3bb"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3700), "United Kingdom Inlet", 92 },
                    { 97, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("254f448f-4786-4677-a0be-d6f677be2f27"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3720), "Awesome Plastic Chair redundant", 3 },
                    { 98, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("7437d2cd-6af9-41d5-a8f9-5654b6858cea"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3740), "Producer solution-oriented", 98 },
                    { 99, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("1de4b318-bd92-4df0-8d8f-770b5e9063dd"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3760), "index Mongolia", 40 },
                    { 100, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("38b2dd25-745a-4014-8499-152a9773508b"), new DateTime(2024, 8, 8, 18, 51, 14, 102, DateTimeKind.Local).AddTicks(3770), "bypassing Unbranded Wooden Mouse", 25 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "LastUpdated", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2390), 1, 1 },
                    { 2, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2390), 2, 2 },
                    { 3, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2400), 2, 3 },
                    { 4, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2400), 2, 4 },
                    { 5, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2400), 2, 5 },
                    { 6, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2400), 2, 6 },
                    { 7, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 7 },
                    { 8, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 8 },
                    { 9, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 9 },
                    { 10, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 10 },
                    { 11, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 11 },
                    { 12, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2410), 2, 12 },
                    { 13, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 13 },
                    { 14, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 14 },
                    { 15, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 15 },
                    { 16, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 16 },
                    { 17, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 17 },
                    { 18, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 18 },
                    { 19, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 19 },
                    { 20, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 20 },
                    { 21, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2420), 2, 21 },
                    { 22, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 22 },
                    { 23, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 23 },
                    { 24, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 24 },
                    { 25, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 25 },
                    { 26, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 26 },
                    { 27, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 27 },
                    { 28, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 28 },
                    { 29, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 29 },
                    { 30, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 30 },
                    { 31, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 31 },
                    { 32, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2430), 2, 32 },
                    { 33, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 33 },
                    { 34, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 34 },
                    { 35, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 35 },
                    { 36, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 36 },
                    { 37, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 37 },
                    { 38, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 38 },
                    { 39, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 39 },
                    { 40, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2440), 2, 40 },
                    { 41, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 41 },
                    { 42, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 42 },
                    { 43, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 43 },
                    { 44, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 44 },
                    { 45, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 45 },
                    { 46, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 46 },
                    { 47, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 47 },
                    { 48, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 48 },
                    { 49, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 49 },
                    { 50, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 50 },
                    { 51, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2450), 2, 51 },
                    { 52, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 52 },
                    { 53, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 53 },
                    { 54, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 54 },
                    { 55, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 55 },
                    { 56, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 56 },
                    { 57, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 57 },
                    { 58, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 58 },
                    { 59, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 59 },
                    { 60, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2460), 2, 60 },
                    { 61, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 61 },
                    { 62, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 62 },
                    { 63, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 63 },
                    { 64, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 64 },
                    { 65, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 65 },
                    { 66, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 66 },
                    { 67, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 67 },
                    { 68, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 68 },
                    { 69, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 69 },
                    { 70, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2470), 2, 70 },
                    { 71, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 71 },
                    { 72, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 72 },
                    { 73, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 73 },
                    { 74, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 74 },
                    { 75, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 75 },
                    { 76, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 76 },
                    { 77, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 77 },
                    { 78, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 78 },
                    { 79, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2480), 2, 79 },
                    { 80, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 80 },
                    { 81, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 81 },
                    { 82, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 82 },
                    { 83, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 83 },
                    { 84, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 84 },
                    { 85, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 85 },
                    { 86, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 86 },
                    { 87, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 87 },
                    { 88, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 88 },
                    { 89, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2490), 2, 89 },
                    { 90, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 90 },
                    { 91, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 91 },
                    { 92, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 92 },
                    { 93, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 93 },
                    { 94, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 94 },
                    { 95, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 95 },
                    { 96, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 96 },
                    { 97, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 97 },
                    { 98, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 98 },
                    { 99, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2500), 2, 99 },
                    { 100, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2510), 2, 100 },
                    { 101, new DateTime(2024, 8, 8, 18, 51, 14, 98, DateTimeKind.Local).AddTicks(2510), 2, 101 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Description", "Guid", "LastUpdated", "QuestionId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("c4a6bab9-d750-4704-8491-5c65aa364e8e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(5600), 1, "Rustic solutions Generic Fresh Bike", 62 },
                    { 2, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("19c685f4-b839-47a0-9c95-1b21cb1eb838"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(5890), 1, "sky blue Customizable Gorgeous", 73 },
                    { 3, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("3b17cf80-6f59-45ef-bf59-d99b0d414b9c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(5920), 1, "Officer Ergonomic Kids, Toys & Books", 2 },
                    { 4, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("d715ef97-f2d6-4610-9bf4-8225f64c7c1f"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(5960), 1, "utilize Delaware project", 19 },
                    { 5, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("5578f937-6c11-4b10-ad29-e1430fdecb71"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(5990), 1, "Principal Portugal Fully-configurable", 76 },
                    { 6, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("e7d998df-1483-46f8-a304-822eb9ddc4ce"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6020), 1, "pixel Small Rubber Chair capacity", 53 },
                    { 7, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("af4969b7-ceae-4089-8b09-9560b03064e6"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6040), 1, "contextually-based Cove Home Loan Account", 85 },
                    { 8, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("94205dcf-3aff-49ef-805e-a2defb6b63b3"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6070), 1, "Overpass Ireland Philippines", 76 },
                    { 9, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("e4159a36-6a6c-47e6-bcf4-ad3ba032bf2f"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6100), 1, "magenta Customer Locks", 22 },
                    { 10, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("9f383517-f3d1-4cc9-82c6-5b550b31c8fc"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6120), 1, "Buckinghamshire copying extensible", 77 },
                    { 11, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("d801d1af-e23f-4d79-af2d-dd82af2e0b13"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6140), 1, "Health, Electronics & Health Tasty Outdoors", 65 },
                    { 12, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("342ab394-16a4-4dc0-8db5-8b8bffdce487"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6170), 1, "platforms withdrawal fuchsia", 73 },
                    { 13, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("a13af8e4-02fe-4faa-a76f-d4c6862f5fe6"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6230), 1, "Money Market Account tan hack", 84 },
                    { 14, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("28ad9e3e-e0f1-44c3-88ca-96278c041c2a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6250), 1, "Shoes & Grocery Health & Movies synthesizing", 30 },
                    { 15, "The Football Is Good For Training And Recreational Purposes", new Guid("04462d43-7d63-4f89-918b-e56646dd67b7"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6280), 1, "Falls Liaison efficient", 11 },
                    { 16, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("454aed92-183d-4536-9e16-4316732ab435"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6300), 1, "Cotton Checking Account matrix", 59 },
                    { 17, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("6b26c46a-1680-4af4-8d01-1f6445eb0a45"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6320), 1, "Ethiopia Architect Concrete", 87 },
                    { 18, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("bec94f57-68e3-44f3-a452-07998ea6d517"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6340), 1, "Books, Health & Music Trafficway system-worthy", 31 },
                    { 19, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("8e4299dc-1bc7-4d26-bf50-b7b5af87a119"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6370), 1, "generating magenta Berkshire", 89 },
                    { 20, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("33424d0f-d65d-4006-b291-818ffd69964d"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6390), 1, "Ergonomic Wooden Chicken Awesome Frozen Computer envisioneer", 28 },
                    { 21, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("8ebfd55a-e826-485e-a2a5-0b47981f3719"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6440), 1, "virtual Coordinator Rustic Cotton Pants", 9 },
                    { 22, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("b10dcd09-38b5-4aee-bbe1-ef3f94c3afc3"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6460), 1, "Guam Organized Architect", 62 },
                    { 23, "The Football Is Good For Training And Recreational Purposes", new Guid("39a25050-621a-4673-8693-5bd84f30258e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6480), 1, "Berkshire Agent convergence", 15 },
                    { 24, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("d7cb5141-12eb-47ab-b3e1-64abfc96221e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6500), 1, "Practical Tasty Steel Table Square", 71 },
                    { 25, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("4c490e02-fc16-4076-a61b-b0210c11848f"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6530), 1, "Sleek Fresh Salad Berkshire Synergized", 80 },
                    { 26, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("55cae79e-7e68-437b-a96e-e8317b4916f6"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6560), 1, "salmon open-source Freeway", 85 },
                    { 27, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("09a7026d-e899-447e-ad41-bcf8ae38ed6f"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6580), 1, "Human Summit Grocery", 41 },
                    { 28, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("2b037cfe-07cd-4a8e-8269-9076ccba3f14"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6600), 1, "overriding Mobility Avon", 19 },
                    { 29, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("9899ba33-0bb1-417e-9f1b-b5fe01108dd2"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6620), 1, "Credit Card Account methodical Gorgeous Steel Soap", 15 },
                    { 30, "The Football Is Good For Training And Recreational Purposes", new Guid("42b187bd-0e65-4263-b6fa-f266e91b16f9"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6650), 1, "Practical Cotton Ball fuchsia Supervisor", 89 },
                    { 31, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("1ed140d0-ee49-45ed-b42d-5b8b9b06bb2e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6670), 1, "Avon Land Borders", 59 },
                    { 32, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("6fe673d0-fcd9-4601-8e1b-adf76f36545c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6700), 1, "South Carolina Saint Helena Pound XML", 56 },
                    { 33, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("b5c110b8-463b-4935-a4ba-1fc854eeca1b"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6720), 1, "haptic Consultant Summit", 52 },
                    { 34, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("23bfec76-75bf-45d6-9dfc-f8fc939f59fd"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6750), 1, "copying Sleek Metal Salad B2B", 65 },
                    { 35, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("2f59d5dd-9984-4767-8d21-fae48431cafd"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6790), 1, "Rustic e-commerce plum", 39 },
                    { 36, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("80ab410d-5939-48bd-a2a6-92597e52e29d"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6810), 1, "Berkshire synthesizing homogeneous", 100 },
                    { 37, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("128160f2-1882-4c91-bbd5-1884a981e52c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6830), 1, "sensor Bedfordshire Ergonomic Steel Shoes", 21 },
                    { 38, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("f88a8b15-c99c-47b1-a3c1-c32dc3304d61"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6850), 1, "Coordinator online Course", 49 },
                    { 39, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("b2a48b3d-5596-495f-b67d-989df5d19e16"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6870), 1, "next generation reboot transition", 35 },
                    { 40, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("56a78137-5a16-4334-9b94-3ec3ae0e0f3a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6900), 1, "withdrawal connecting Spurs", 29 },
                    { 41, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("d120eb2f-a29e-4644-a196-25de7e7cfbc5"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6920), 1, "Albania generate deploy", 21 },
                    { 42, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("b622efc5-2094-4385-a7a0-34d70a6dad4a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6940), 1, "Pines online Senior", 61 },
                    { 43, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("8293ded9-c347-4833-8e75-887df741f049"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6960), 1, "wireless mobile Shoal", 62 },
                    { 44, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("d4f73fb9-75a9-4f02-b1c3-856fc3e673ce"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(6980), 1, "Connecticut Frozen users", 4 },
                    { 45, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("6d7be19f-09e2-46a5-b6bc-76eb8a32775e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7000), 1, "Customer Automotive Factors", 62 },
                    { 46, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("db0feb67-bee2-4cdf-a9c7-cfb66ab9b37c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7020), 1, "Alabama engineer Kentucky", 11 },
                    { 47, "The Football Is Good For Training And Recreational Purposes", new Guid("3963f421-6bf8-4472-a12e-1e5399a330a0"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7040), 1, "e-services Lempira Berkshire", 25 },
                    { 48, "The Football Is Good For Training And Recreational Purposes", new Guid("2135b179-f1dc-4283-850d-75eb0e210c31"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7060), 1, "markets invoice Track", 81 },
                    { 49, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("669d38c7-b308-4544-a698-b83859ef4b12"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7080), 1, "Iranian Rial repurpose multi-byte", 82 },
                    { 50, "The Football Is Good For Training And Recreational Purposes", new Guid("84bb46fc-7699-452f-9065-c6fea51887c9"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7130), 1, "Infrastructure flexibility Producer", 54 },
                    { 51, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("6cb94a5e-480f-4045-9f77-647dc7b536ca"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7150), 1, "asynchronous Brooks green", 69 },
                    { 52, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("c19a75f5-3430-4f5f-8d6a-cdb956d903e5"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7170), 1, "programming Overpass parse", 49 },
                    { 53, "The Football Is Good For Training And Recreational Purposes", new Guid("bc3fc4a5-64bd-4aa8-8909-e48ee245df9c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7190), 1, "turquoise Practical Rubber Salad Configuration", 10 },
                    { 54, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("3f39df84-fd01-49e5-bac1-e18d80db3081"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7230), 1, "transmitting Russian Federation regional", 31 },
                    { 55, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("fa048a56-6cd5-485d-8174-1851aeac40ca"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7250), 1, "Orchestrator Crossroad whiteboard", 49 },
                    { 56, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("020f545f-c627-4d44-b6b2-33d8d6314388"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7280), 1, "turn-key Metrics Alabama", 20 },
                    { 57, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("8242015d-e8b7-438d-96f7-bc74f333b474"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7290), 1, "Incredible Soft Fish Riel Radial", 24 },
                    { 58, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("4987dc20-b4fd-4679-bacb-c1b183db647c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7330), 1, "monitor customer loyalty Gorgeous", 67 },
                    { 59, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("6df1b780-aaa1-446a-82cc-f473b10b133c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7350), 1, "compress Florida Realigned", 87 },
                    { 60, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("4219187a-c1a5-4ded-b529-6d82597a3448"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7370), 1, "withdrawal optical modular", 50 },
                    { 61, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("32615437-111c-47f5-8a9c-996c1e50f5f1"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7390), 1, "bypassing Oklahoma back up", 50 },
                    { 62, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("7cbd6fe8-45b6-49bd-a23a-239bcff5d832"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7410), 1, "utilize Connecticut Harbor", 11 },
                    { 63, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("10ae17e0-e2b1-4ded-ba6c-20de547434ce"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7430), 1, "primary Cotton bricks-and-clicks", 100 },
                    { 64, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("a6474f74-3a53-478f-b8a8-cdfcd38f67af"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7450), 1, "User-friendly Generic Metal Tuna holistic", 38 },
                    { 65, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("cd8dcff4-0318-4191-848c-3cd7ae4d249b"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7470), 1, "Peru primary Awesome Metal Shoes", 32 },
                    { 66, "The Football Is Good For Training And Recreational Purposes", new Guid("ffaaa3d4-ecca-43bf-bb39-417820ea1992"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7500), 1, "North Carolina matrix Springs", 22 },
                    { 67, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("3b818954-8ac0-44ef-9583-417b283db04e"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7520), 1, "Sports & Automotive XML Sports", 88 },
                    { 68, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("101453be-7bdc-445e-9e2b-a608bcd2867a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7540), 1, "Michigan Austria Czech Republic", 34 },
                    { 69, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("51fb0c99-9e89-4e55-a7ad-0cead9823cce"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7580), 1, "Sharable Buckinghamshire interface", 64 },
                    { 70, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("16d0e2a9-719b-4c88-9560-58e2cc651810"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7600), 1, "parse SQL Mission", 22 },
                    { 71, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("f141317e-270b-4197-aafa-4d67ef654003"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7620), 1, "Product Optimization quantifying", 83 },
                    { 72, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("2d94bbee-a692-4a56-b932-bc400f028a68"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7640), 1, "Incredible virtual transmitting", 36 },
                    { 73, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("3f243ea6-4e3d-44ef-a36c-e0a2eb28cfb8"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7660), 1, "bypass Programmable Loop", 53 },
                    { 74, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("cbf93bc9-5a3c-4119-8d5e-0b306ed035ca"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7680), 1, "Plastic Fantastic Interactions", 62 },
                    { 75, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("ef70f598-960c-4c43-9ed5-d46a1b1409e8"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7700), 1, "International Way best-of-breed", 62 },
                    { 76, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("743e2ad1-caed-4c66-a8d8-e370b423c1f6"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7720), 1, "input Money Market Account International", 81 },
                    { 77, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("305b1f27-1299-42ca-8698-c5749cc0d264"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7740), 1, "open-source Reverse-engineered SMS", 88 },
                    { 78, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("51a75a36-cfb6-4d5a-ab86-c4d22b204883"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7760), 1, "Licensed Soft Shirt Principal Stream", 17 },
                    { 79, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("329e9588-daa4-419e-a436-370b6ca75b6d"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7790), 1, "Triple-buffered tan Expressway", 27 },
                    { 80, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("a9abdf12-fc64-4eb4-b1c3-c0c34197c3df"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7810), 1, "Iranian Rial hacking connect", 84 },
                    { 81, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("d13f9403-db2b-4897-bf69-f609c7a7b768"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7820), 1, "navigating Cayman Islands target", 14 },
                    { 82, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("b9d8d07a-8ee6-4ec7-a936-a27198c5224a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7840), 1, "Legacy Applications invoice", 15 },
                    { 83, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("5022eac8-86d7-4dff-9b40-b223e9737ea5"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7870), 1, "Pennsylvania sky blue Cook Islands", 16 },
                    { 84, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("839d109a-ef41-4b1b-a3a1-b83458c061a5"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7880), 1, "Sleek Orchestrator Vietnam", 28 },
                    { 85, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("0e1919b6-6453-4ba1-9d87-5a576f482580"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7900), 1, "withdrawal Dale overriding", 17 },
                    { 86, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("a943fff5-3e7e-4168-8370-7e47682dc0a1"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7920), 1, "Barbados Dollar Intelligent generate", 88 },
                    { 87, "The Football Is Good For Training And Recreational Purposes", new Guid("4689c032-1778-4b8d-8264-8575added8ed"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7970), 1, "silver copy Generic", 74 },
                    { 88, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("d22f3939-ded9-4a51-8889-7e89a8621cef"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(7990), 1, "archive maroon parse", 6 },
                    { 89, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("d287395a-f418-4edd-92b8-761ca75b1ab7"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8010), 1, "Bedfordshire Buckinghamshire Cambridgeshire", 43 },
                    { 90, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("825e79e4-27d2-43d3-b1e4-853863727f0a"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8020), 1, "core gold integrate", 77 },
                    { 91, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("928628a7-f87c-4e4d-bda8-02a624e2e769"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8040), 1, "Finland RSS Ergonomic", 99 },
                    { 92, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("6d98517f-1930-4be8-af59-d93cf6cf500c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8060), 1, "Buckinghamshire extranet Australian Dollar", 59 },
                    { 93, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("4d5a7c40-535d-406e-9d46-6551d95a61e7"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8090), 1, "Enhanced Armenia next-generation", 29 },
                    { 94, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("882f5120-4ba0-4ba7-8738-cb4c7c66ab72"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8110), 1, "Personal Loan Account Integration Road", 70 },
                    { 95, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("0181afc3-bbbe-43ad-94aa-b43f6f81389c"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8130), 1, "Music & Tools quantifying experiences", 2 },
                    { 96, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("d26ff78e-43a0-40b5-beb5-dc50a8558cc9"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8160), 1, "Producer HDD applications", 81 },
                    { 97, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("0b6364c9-836e-4fa7-a3e6-c30d7f523c90"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8180), 1, "Distributed harness Incredible Soft Pizza", 7 },
                    { 98, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("64ee33ea-1905-4022-b48d-2c47d6bdc066"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8210), 1, "cyan Alaska Shoals", 10 },
                    { 99, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("b58d0330-9014-4aab-9b7f-d69451dfdb10"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8220), 1, "Architect Sleek Granite Table bricks-and-clicks", 87 },
                    { 100, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("bdd590e7-975b-4a4e-9302-ebad84ee8297"), new DateTime(2024, 8, 8, 18, 51, 14, 103, DateTimeKind.Local).AddTicks(8250), 1, "Vietnam Designer JBOD", 32 }
                });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "IsBookmarked", "LastUpdated", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 8, 8, 18, 51, 14, 104, DateTimeKind.Local).AddTicks(9890), 1, 1 },
                    { 2, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(360), 2, 2 },
                    { 3, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(370), 3, 3 },
                    { 4, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(380), 4, 4 },
                    { 5, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(380), 5, 5 },
                    { 6, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(390), 6, 6 },
                    { 7, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(400), 7, 7 },
                    { 8, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(400), 8, 8 },
                    { 9, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(400), 9, 9 },
                    { 10, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(410), 10, 10 },
                    { 11, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(410), 11, 11 },
                    { 12, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(420), 12, 12 },
                    { 13, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(420), 13, 13 },
                    { 14, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(430), 14, 14 },
                    { 15, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(430), 15, 15 },
                    { 16, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(440), 16, 16 },
                    { 17, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(440), 17, 17 },
                    { 18, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(440), 18, 18 },
                    { 19, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(450), 19, 19 },
                    { 20, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(450), 20, 20 },
                    { 21, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(460), 21, 21 },
                    { 22, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(460), 22, 22 },
                    { 23, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(470), 23, 23 },
                    { 24, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(470), 24, 24 },
                    { 25, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(470), 25, 25 },
                    { 26, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(500), 26, 26 },
                    { 27, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(500), 27, 27 },
                    { 28, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(510), 28, 28 },
                    { 29, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(510), 29, 29 },
                    { 30, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(520), 30, 30 },
                    { 31, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(520), 31, 31 },
                    { 32, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(530), 32, 32 },
                    { 33, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(530), 33, 33 },
                    { 34, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(530), 34, 34 },
                    { 35, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(540), 35, 35 },
                    { 36, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(540), 36, 36 },
                    { 37, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(550), 37, 37 },
                    { 38, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(550), 38, 38 },
                    { 39, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(560), 39, 39 },
                    { 40, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(560), 40, 40 },
                    { 41, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(560), 41, 41 },
                    { 42, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(570), 42, 42 },
                    { 43, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(570), 43, 43 },
                    { 44, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(580), 44, 44 },
                    { 45, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(580), 45, 45 },
                    { 46, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(580), 46, 46 },
                    { 47, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(590), 47, 47 },
                    { 48, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(590), 48, 48 },
                    { 49, true, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(600), 49, 49 },
                    { 50, false, new DateTime(2024, 8, 8, 18, 51, 14, 105, DateTimeKind.Local).AddTicks(600), 50, 50 }
                });

            migrationBuilder.InsertData(
                table: "QuestionVotes",
                columns: new[] { "Id", "Kind", "LastUpdated", "QuestionId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(7780), 1 },
                    { 2, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8550), 2 },
                    { 3, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8560), 3 },
                    { 4, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8570), 4 },
                    { 5, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8570), 5 },
                    { 6, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8580), 6 },
                    { 7, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8580), 7 },
                    { 8, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8590), 8 },
                    { 9, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8590), 9 },
                    { 10, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8600), 10 },
                    { 11, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8600), 11 },
                    { 12, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8610), 12 },
                    { 13, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8610), 13 },
                    { 14, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8610), 14 },
                    { 15, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8620), 15 },
                    { 16, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8620), 16 },
                    { 17, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8630), 17 },
                    { 18, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8630), 18 },
                    { 19, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8630), 19 },
                    { 20, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8640), 20 },
                    { 21, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8640), 21 },
                    { 22, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8650), 22 },
                    { 23, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8650), 23 },
                    { 24, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8650), 24 },
                    { 25, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8660), 25 },
                    { 26, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8660), 26 },
                    { 27, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8660), 27 },
                    { 28, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8670), 28 },
                    { 29, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8670), 29 },
                    { 30, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8680), 30 },
                    { 31, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8680), 31 },
                    { 32, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8680), 32 },
                    { 33, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8690), 33 },
                    { 34, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8690), 34 },
                    { 35, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8700), 35 },
                    { 36, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8700), 36 },
                    { 37, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8700), 37 },
                    { 38, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8710), 38 },
                    { 39, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8710), 39 },
                    { 40, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8720), 40 },
                    { 41, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8720), 41 },
                    { 42, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8750), 42 },
                    { 43, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8750), 43 },
                    { 44, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8750), 44 },
                    { 45, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8760), 45 },
                    { 46, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8760), 46 },
                    { 47, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8760), 47 },
                    { 48, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8770), 48 },
                    { 49, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8770), 49 },
                    { 50, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8780), 50 },
                    { 51, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8780), 51 },
                    { 52, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8780), 52 },
                    { 53, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8790), 53 },
                    { 54, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8790), 54 },
                    { 55, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8790), 55 },
                    { 56, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8800), 56 },
                    { 57, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8800), 57 },
                    { 58, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8810), 58 },
                    { 59, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8810), 59 },
                    { 60, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8810), 60 },
                    { 61, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8820), 61 },
                    { 62, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8820), 62 },
                    { 63, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8820), 63 },
                    { 64, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8830), 64 },
                    { 65, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8830), 65 },
                    { 66, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8840), 66 },
                    { 67, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8840), 67 },
                    { 68, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8840), 68 },
                    { 69, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8850), 69 },
                    { 70, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8850), 70 },
                    { 71, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8850), 71 },
                    { 72, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8860), 72 },
                    { 73, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8860), 73 },
                    { 74, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8870), 74 },
                    { 75, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8870), 75 },
                    { 76, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8870), 76 },
                    { 77, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8880), 77 },
                    { 78, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8880), 78 },
                    { 79, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8880), 79 },
                    { 80, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8890), 80 },
                    { 81, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8890), 81 },
                    { 82, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8900), 82 },
                    { 83, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8900), 83 },
                    { 84, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8900), 84 },
                    { 85, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8910), 85 },
                    { 86, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8910), 86 },
                    { 87, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8910), 87 },
                    { 88, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8920), 88 },
                    { 89, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8920), 89 },
                    { 90, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8930), 90 },
                    { 91, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8930), 91 },
                    { 92, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8930), 92 },
                    { 93, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8940), 93 },
                    { 94, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8940), 94 },
                    { 95, false, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8950), 95 },
                    { 96, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8950), 96 },
                    { 97, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8950), 97 },
                    { 98, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8960), 98 },
                    { 99, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8970), 99 },
                    { 100, true, new DateTime(2024, 8, 8, 18, 51, 14, 100, DateTimeKind.Local).AddTicks(8970), 100 }
                });

            migrationBuilder.InsertData(
                table: "AnswerVotes",
                columns: new[] { "Id", "AnswerId", "Kind", "LastUpdated" },
                values: new object[,]
                {
                    { 1, 1, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(4350) },
                    { 2, 2, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5260) },
                    { 3, 3, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5270) },
                    { 4, 4, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5280) },
                    { 5, 5, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5280) },
                    { 6, 6, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5290) },
                    { 7, 7, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5290) },
                    { 8, 8, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5300) },
                    { 9, 9, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5300) },
                    { 10, 10, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5300) },
                    { 11, 11, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5310) },
                    { 12, 12, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5310) },
                    { 13, 13, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5320) },
                    { 14, 14, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5320) },
                    { 15, 15, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5330) },
                    { 16, 16, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5330) },
                    { 17, 17, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5330) },
                    { 18, 18, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5340) },
                    { 19, 19, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5340) },
                    { 20, 20, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5350) },
                    { 21, 21, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5350) },
                    { 22, 22, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5350) },
                    { 23, 23, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5360) },
                    { 24, 24, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5360) },
                    { 25, 25, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5360) },
                    { 26, 26, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5370) },
                    { 27, 27, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5370) },
                    { 28, 28, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5380) },
                    { 29, 29, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5380) },
                    { 30, 30, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5380) },
                    { 31, 31, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5390) },
                    { 32, 32, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5390) },
                    { 33, 33, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5400) },
                    { 34, 34, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5430) },
                    { 35, 35, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5430) },
                    { 36, 36, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5430) },
                    { 37, 37, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5440) },
                    { 38, 38, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5440) },
                    { 39, 39, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5440) },
                    { 40, 40, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5450) },
                    { 41, 41, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5450) },
                    { 42, 42, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5460) },
                    { 43, 43, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5460) },
                    { 44, 44, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5460) },
                    { 45, 45, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5470) },
                    { 46, 46, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5470) },
                    { 47, 47, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5480) },
                    { 48, 48, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5480) },
                    { 49, 49, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5480) },
                    { 50, 50, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5490) },
                    { 51, 51, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5490) },
                    { 52, 52, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5500) },
                    { 53, 53, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5500) },
                    { 54, 54, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5500) },
                    { 55, 55, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5510) },
                    { 56, 56, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5510) },
                    { 57, 57, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5510) },
                    { 58, 58, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5520) },
                    { 59, 59, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5520) },
                    { 60, 60, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5530) },
                    { 61, 61, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5530) },
                    { 62, 62, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5530) },
                    { 63, 63, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5540) },
                    { 64, 64, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5540) },
                    { 65, 65, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5550) },
                    { 66, 66, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5550) },
                    { 67, 67, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5550) },
                    { 68, 68, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5560) },
                    { 69, 69, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5560) },
                    { 70, 70, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5570) },
                    { 71, 71, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5570) },
                    { 72, 72, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5570) },
                    { 73, 73, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5580) },
                    { 74, 74, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5580) },
                    { 75, 75, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5590) },
                    { 76, 76, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5590) },
                    { 77, 77, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5590) },
                    { 78, 78, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5600) },
                    { 79, 79, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5600) },
                    { 80, 80, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5600) },
                    { 81, 81, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5610) },
                    { 82, 82, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5610) },
                    { 83, 83, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5610) },
                    { 84, 84, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5620) },
                    { 85, 85, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5620) },
                    { 86, 86, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5630) },
                    { 87, 87, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5630) },
                    { 88, 88, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5640) },
                    { 89, 89, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5640) },
                    { 90, 90, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5640) },
                    { 91, 91, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5650) },
                    { 92, 92, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5650) },
                    { 93, 93, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5660) },
                    { 94, 94, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5660) },
                    { 95, 95, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5660) },
                    { 96, 96, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5670) },
                    { 97, 97, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5670) },
                    { 98, 98, false, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5670) },
                    { 99, 99, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5680) },
                    { 100, 100, true, new DateTime(2024, 8, 8, 18, 51, 14, 99, DateTimeKind.Local).AddTicks(5680) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_Guid",
                table: "Answers",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_UserId",
                table: "Answers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerVotes_AnswerId",
                table: "AnswerVotes",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_QuestionId",
                table: "Bookmarks",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_UserId",
                table: "Bookmarks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Guid",
                table: "Persons",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_Guid",
                table: "Questions",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserId",
                table: "Questions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionVotes_QuestionId",
                table: "QuestionVotes",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Guid",
                table: "Roles",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Guid",
                table: "Users",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerVotes");

            migrationBuilder.DropTable(
                name: "Bookmarks");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "QuestionVotes");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
