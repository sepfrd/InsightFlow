using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace InsightFlow.DataAccess.Migrations
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
                    { 1, "Sepehr", new Guid("e305e580-dbd2-4302-8386-9095dde81de2"), "Foroughi Rad", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(480) },
                    { 2, "Bernard", new Guid("f1a1e79c-4aee-45a6-b8ce-2d7328508664"), "Cool", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(520) },
                    { 3, "Delores", new Guid("3dbd6b19-d926-4d0c-8c58-590788526f24"), "Haley", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(540) },
                    { 4, "Erwin", new Guid("cbbeb005-7bb2-40fa-9788-3446b2047e6b"), "Willms", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(670) },
                    { 5, "Tyler", new Guid("84108724-ea30-4bd2-82aa-bb737ee80b56"), "Halvorson", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(700) },
                    { 6, "Stephanie", new Guid("0f23e454-cb6a-437d-b0cc-257985a220b5"), "Ziemann", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(710) },
                    { 7, "Marilou", new Guid("d08d4943-f2aa-4afd-8759-d7cbb604559f"), "Kozey", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(730) },
                    { 8, "Erik", new Guid("05609ab4-4dd8-496e-95ee-a4d60daa0a98"), "Altenwerth", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(750) },
                    { 9, "Chadrick", new Guid("ad817b60-1e22-4744-8694-2cfb7da594bb"), "Hettinger", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(760) },
                    { 10, "Kamille", new Guid("7b751fbe-f008-4bff-a5ca-8e54ffeb8683"), "Stanton", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(770) },
                    { 11, "Darrell", new Guid("beb0a2a8-e20f-4ccd-aa3d-70860861edd8"), "Howell", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(790) },
                    { 12, "Jessica", new Guid("e4a60b84-e1c5-49f2-8111-8c6722671a28"), "Schimmel", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(800) },
                    { 13, "Elmore", new Guid("f6fd1ed6-7cba-47b5-92c2-60b4ff8d6b5b"), "Morar", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(820) },
                    { 14, "Gina", new Guid("804b5b4e-4598-49c0-8628-0d15277318e9"), "Bode", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(830) },
                    { 15, "Remington", new Guid("a65aa020-6502-44fd-a7ad-76cfaaa7cb79"), "Klein", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(850) },
                    { 16, "Roosevelt", new Guid("ea963705-9bb9-4a39-b40c-61d76167b368"), "Zemlak", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(860) },
                    { 17, "Gregg", new Guid("9d525f8d-07e8-48d7-ab15-66c6d0921c20"), "Ondricka", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(870) },
                    { 18, "Fae", new Guid("e50b39ef-8335-4aca-8a41-67e2dc9c6854"), "Stiedemann", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(890) },
                    { 19, "Torrance", new Guid("cfdcf66d-551e-48ad-8e64-d9107b7e7789"), "Rohan", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(900) },
                    { 20, "Maegan", new Guid("8505b48c-ab58-43a6-ac2a-4f7d8fd4a6ff"), "Krajcik", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(910) },
                    { 21, "Gisselle", new Guid("6c892634-8efc-414c-a7b1-279710aa149a"), "Swaniawski", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(940) },
                    { 22, "Preston", new Guid("331be114-ef7f-4157-9e93-e3586023891f"), "Purdy", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(950) },
                    { 23, "Andy", new Guid("74d08e13-d007-42cc-a49f-35bd81880844"), "Kling", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(970) },
                    { 24, "Claudine", new Guid("9c87b387-fbea-4fd9-946e-6f44dd48bdbc"), "Zieme", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(980) },
                    { 25, "Shyann", new Guid("41422914-8678-4a33-b7e3-e2431e4b878b"), "Dickens", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(990) },
                    { 26, "Kamron", new Guid("4f757f80-d231-445b-9069-c5461324f894"), "Rath", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1010) },
                    { 27, "Roberto", new Guid("91642268-7760-4116-a8a4-2e7b16b57de9"), "Boyer", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1020) },
                    { 28, "Misael", new Guid("07f937e6-a67c-4d79-a63a-c13dd722726d"), "Lehner", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1040) },
                    { 29, "Quinn", new Guid("e97e22e2-db9b-4cd2-90a9-98a5c0559d98"), "Bailey", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1050) },
                    { 30, "Woodrow", new Guid("83a76023-5c7a-433e-a89c-e9df5c0828aa"), "Emard", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1080) },
                    { 31, "Bradly", new Guid("3c5df1f7-39e7-42cf-a96f-6cac0a79d1f1"), "Lesch", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1100) },
                    { 32, "Katelyn", new Guid("e602a9ab-c626-4d79-998b-e46562d02215"), "Hahn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1120) },
                    { 33, "Muriel", new Guid("85f4db58-7d80-4da2-928e-1f70c06b185a"), "Schiller", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1130) },
                    { 34, "Meggie", new Guid("8bcd9e75-443f-4f5b-9f86-028846f98f47"), "Hahn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1150) },
                    { 35, "Noel", new Guid("ee028322-fbef-4c64-b6a9-9e9349eccecd"), "King", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1160) },
                    { 36, "Gonzalo", new Guid("036d856d-da18-46a6-9bcb-261c0ae70507"), "Dare", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1170) },
                    { 37, "Felix", new Guid("37f1b132-79a0-413e-ad05-1f238d7580b3"), "Shields", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1180) },
                    { 38, "Jeanie", new Guid("bdf0b7c7-2645-4a6d-a0d4-e4cac1ef40ba"), "Marks", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1200) },
                    { 39, "Trenton", new Guid("03f8ebef-6e5b-4176-bf97-1609835d74d9"), "Kunde", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1210) },
                    { 40, "Ruth", new Guid("98f8c694-9c42-4861-b43a-a6954239dfd4"), "O'Keefe", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1220) },
                    { 41, "Roderick", new Guid("9bfb610e-edff-4236-aaae-40cb70ed68f7"), "Bayer", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1230) },
                    { 42, "Muriel", new Guid("35367674-ac4c-4019-9757-2e7f93305c38"), "Beatty", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1240) },
                    { 43, "Braeden", new Guid("d97f5f77-dc14-4454-bebe-7390ec43c462"), "Baumbach", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1250) },
                    { 44, "Christ", new Guid("278a7810-9da2-43f1-a7d7-9ef9244a8750"), "Hudson", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1260) },
                    { 45, "Thad", new Guid("269c3b8c-62cb-4d4d-85d9-3337d9d26f7b"), "Cummerata", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1280) },
                    { 46, "Louvenia", new Guid("4562a952-89bb-4d82-b8dc-3f8d817899cd"), "Schuppe", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1290) },
                    { 47, "Chanelle", new Guid("5259f9a5-76f1-4586-863e-1d5c4497a942"), "Gorczany", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1300) },
                    { 48, "Wyman", new Guid("f2f3d098-79ee-45ba-bb39-e0924e439a0d"), "Harber", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1310) },
                    { 49, "Filomena", new Guid("57b89daf-3456-4584-ad9f-56926ac47e02"), "Kessler", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1330) },
                    { 50, "Hermina", new Guid("e56a6580-d23f-4cdf-a81b-1ae9fabb0850"), "Jerde", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1340) },
                    { 51, "Mylene", new Guid("5f4bbeb8-ba67-48fe-969b-749425101207"), "Hintz", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1350) },
                    { 52, "Ivah", new Guid("8edf8233-f5b5-44aa-a6ff-efd8bae66e3a"), "McDermott", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1360) },
                    { 53, "Meredith", new Guid("a9e9c7ff-e014-4aa1-b589-17c83dcccd32"), "Wilkinson", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1380) },
                    { 54, "Jeremie", new Guid("9319cf34-c02b-47b3-b099-8ec57a8f31d8"), "Wuckert", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1390) },
                    { 55, "Emelia", new Guid("157b1cb0-4524-4cc5-982e-0878b34bd6fc"), "Hagenes", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1400) },
                    { 56, "Destiny", new Guid("bcc12faf-09e2-4dbc-94f8-823a30724e6b"), "Ritchie", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1410) },
                    { 57, "Aliyah", new Guid("6db9dd7f-9bbc-4af4-88a9-9785d9f4e804"), "Sauer", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1440) },
                    { 58, "Leonora", new Guid("9c61e252-51f2-449e-ad35-3aa3da1b7846"), "Feeney", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1450) },
                    { 59, "Lindsay", new Guid("0ec8ba34-2962-4b8d-9a3d-a92e5d0ed041"), "Ankunding", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1460) },
                    { 60, "Aracely", new Guid("269b479b-76e3-481e-86c7-f019a3bc4b36"), "Reichel", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1470) },
                    { 61, "Lavonne", new Guid("25d3b8e6-7be2-4be1-b4f6-3ba35d1fd7fe"), "Maggio", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1490) },
                    { 62, "Melvina", new Guid("da87875f-200a-4b5b-a9f7-2608e90094ff"), "Kuhn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1500) },
                    { 63, "Emilie", new Guid("a7a28a4e-330f-460a-952a-ebda8160a402"), "Lockman", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1510) },
                    { 64, "Oswaldo", new Guid("f860b1d4-a482-4210-a872-5932e5724b9a"), "Larson", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1520) },
                    { 65, "Cary", new Guid("d8da4063-0418-442b-abf2-807417410940"), "Mohr", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1540) },
                    { 66, "Thomas", new Guid("9dd2d2e8-e612-4e37-bf49-2bcfd2d90c8f"), "Vandervort", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1560) },
                    { 67, "Diamond", new Guid("6617f5b0-71b9-48d6-af5b-a3c6e64d5545"), "Dickens", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1570) },
                    { 68, "Guy", new Guid("ce1cf579-7c52-471f-8a3f-3128fc2d444f"), "Orn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1590) },
                    { 69, "Catherine", new Guid("31aab869-63ab-4a0a-9fb0-3843687a3887"), "Mosciski", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1600) },
                    { 70, "Marcelino", new Guid("35b7f584-d0d1-41f6-8a5c-84f3a5f9289e"), "Doyle", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1610) },
                    { 71, "Margarett", new Guid("2f3c532d-8c59-4fb8-bc28-8444db4318a7"), "Harber", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1620) },
                    { 72, "Anabel", new Guid("45ef169a-7610-457d-a1c1-9efaa8063616"), "Conn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1630) },
                    { 73, "Ardella", new Guid("38d4b568-d24e-425b-a7e3-fb14a4ee06e8"), "Bauch", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1650) },
                    { 74, "Madaline", new Guid("6b6ffba3-a33d-4b27-b04b-881916176b17"), "Romaguera", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1660) },
                    { 75, "Kenya", new Guid("3827321e-ff6b-4e49-aba7-994b5771ef8a"), "Jast", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1670) },
                    { 76, "Jack", new Guid("72468414-46ee-49dd-90a3-5ebd2cdbb22b"), "Volkman", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1680) },
                    { 77, "Ila", new Guid("15f00cbd-6ee5-420b-b580-a18837bdcbc6"), "Adams", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1700) },
                    { 78, "Davon", new Guid("73e1044f-c795-46ab-9ca2-d76e0e2f5c69"), "Gerlach", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1710) },
                    { 79, "Shaun", new Guid("96e9f3b3-55b5-46c5-a4e0-ca57c5300b1a"), "Kiehn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1720) },
                    { 80, "Hollis", new Guid("ee971c65-7f5e-4b9c-8e84-abcb5d0cb71f"), "Lesch", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1730) },
                    { 81, "Adalberto", new Guid("53f1739e-a5f2-4fa9-9727-16e690cc2836"), "Aufderhar", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1750) },
                    { 82, "Eula", new Guid("9c7567af-42d7-4fb3-aec4-d5c826c8ac46"), "Kihn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1770) },
                    { 83, "Priscilla", new Guid("258ceebf-ee38-49d9-aec3-a95de74654a8"), "Adams", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1780) },
                    { 84, "Cicero", new Guid("92271347-4984-4668-9c0d-603d999c5802"), "Moen", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1790) },
                    { 85, "Angeline", new Guid("60cf3377-e403-4b08-ae1b-f10eb90af175"), "Carter", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1800) },
                    { 86, "Thalia", new Guid("376531de-f2ca-4328-8fb4-0a5959c4b207"), "Wyman", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1810) },
                    { 87, "Tommie", new Guid("3cb2570a-0293-4bc5-a4db-a8e8084fca11"), "Wolff", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1820) },
                    { 88, "Celestino", new Guid("c73adb49-1b7d-4db2-8b71-783fe5cc8c2b"), "Will", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1840) },
                    { 89, "Joannie", new Guid("f7a0d66c-bb87-4b97-97f0-41e29c725a66"), "Nicolas", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1850) },
                    { 90, "Emiliano", new Guid("1c95cff5-7051-479a-8907-fd3f55f46752"), "Gislason", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1860) },
                    { 91, "Ozella", new Guid("9f933604-881f-4edf-886a-24ad8d84c155"), "Jacobson", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1870) },
                    { 92, "Sonya", new Guid("3033b273-77e3-4f67-8f37-a27bbf6fb17d"), "Goyette", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1880) },
                    { 93, "Jaydon", new Guid("c41393b0-de48-415d-996c-bc7f569f0a66"), "Rowe", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1890) },
                    { 94, "Vida", new Guid("7b8e2342-987d-4f6e-a755-1a13bfaed759"), "Orn", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1910) },
                    { 95, "Katharina", new Guid("7ca75a48-f590-4ab6-be92-0941f47063f3"), "Okuneva", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1920) },
                    { 96, "Adella", new Guid("6baa0caf-2879-479a-aaa1-1e31b3141178"), "Turner", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1930) },
                    { 97, "Lillian", new Guid("09db256d-aa0e-4a19-8347-a9c50a6b3860"), "Kshlerin", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1950) },
                    { 98, "Jo", new Guid("9f10772a-912e-42d2-a15b-674b55f0c9f2"), "Bartell", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1960) },
                    { 99, "Ena", new Guid("b33424b4-3434-4ced-9ec2-9179b0ffed64"), "Stanton", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1970) },
                    { 100, "Branson", new Guid("3caa5577-b8bf-4abc-8fa8-d0319483293e"), "Swift", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(1990) },
                    { 101, "Annabelle", new Guid("e91f3dcb-a42e-4b55-9612-1f73b6e1a606"), "Cremin", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(2000) },
                    { 102, "Crawford", new Guid("9bcc6487-2b52-408d-a3fb-40f66639b73b"), "Will", new DateTime(2024, 8, 19, 1, 7, 42, 179, DateTimeKind.Local).AddTicks(2010) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Guid", "LastUpdated", "Title" },
                values: new object[,]
                {
                    { 1, new Guid("aef9bffa-dc3a-48a9-855b-8cd216729eb0"), new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7800), "Admin" },
                    { 2, new Guid("772b8f9b-f4df-44df-9192-9f0be58c9273"), new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7820), "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Guid", "LastUpdated", "Password", "PersonId", "Score", "Username" },
                values: new object[,]
                {
                    { 1, new Guid("ceb8cebf-49f4-4393-9e28-b7e07690019b"), new DateTime(2024, 8, 19, 1, 7, 41, 961, DateTimeKind.Local).AddTicks(8030), "b6766fafe72f0c89d1f95e65d3e7ae6c8a90de36a390e44acaec3af65f04ed06c29881dda3e8b091639b8243ef3d839a17c243c8e94b0fa145b667740724119f", 1, 0, "sepehr_frd" },
                    { 2, new Guid("10e18222-48dd-4d18-b1b3-7dba759b2bc3"), new DateTime(2024, 8, 19, 1, 7, 41, 967, DateTimeKind.Local).AddTicks(3780), "4cc6b8c45dc13ffecb5a36c0f301de6f47eafb64437730a5f163b8d770fd0d6e0302c51cf9c600dfbd9991030343c53c9a06c1fc27642e66968405e67013ef32", 2, 0, "bernard_cool" },
                    { 3, new Guid("fc9b7d13-f05c-4495-b5f0-7da1785e9d2c"), new DateTime(2024, 8, 19, 1, 7, 42, 184, DateTimeKind.Local).AddTicks(9850), "IosVBnZXbl", 3, 8, "Ricky_OConnell62" },
                    { 4, new Guid("731b81c2-f715-4550-a97b-8bddba95e64a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(230), "vSHLgXJSOI", 4, 50, "Terence74" },
                    { 5, new Guid("179c25f3-d486-4d19-ae3c-0b53abca3035"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(300), "G4MS1EcQlK", 5, 31, "Jordyn6" },
                    { 6, new Guid("e2dcdf6d-0f00-4634-9e39-dda769b93747"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(350), "ohSBjGzJTm", 6, 9, "Florence_Blanda" },
                    { 7, new Guid("d3894239-6c1e-4c28-abb7-64f6c7d470ba"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(410), "lUlduzyVzP", 7, 37, "Eliane8" },
                    { 8, new Guid("7d853fac-d79e-4162-9125-3e185b88d04b"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(470), "41bzUyJX5H", 8, 28, "Prince.Cormier38" },
                    { 9, new Guid("92a045ce-9eed-4bc9-a2b8-c8a33911439c"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(530), "PIvaXFIWgZ", 9, 23, "Evans.Dickens" },
                    { 10, new Guid("462e070e-7f7e-4107-9c0f-0a7474eab157"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(590), "DV9Y8iWZpv", 10, 21, "Opal84" },
                    { 11, new Guid("3a0a3929-f84f-4752-8da0-0bfcae06b4e8"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(680), "qzuBwtVWZN", 11, 50, "Fausto22" },
                    { 12, new Guid("3b2e2f5c-3986-4bc3-81b8-d42a09ff0cbc"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(740), "bHJDWxnIMy", 12, 29, "Maximus.Maggio" },
                    { 13, new Guid("79365095-02a8-4e03-8d74-137957be9cd1"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(790), "D8ZDm12ZXx", 13, 50, "Orville_Gusikowski" },
                    { 14, new Guid("cf1fec4d-db55-4848-b4bd-b5a5bd46bb78"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(850), "eCVo2z8OrF", 14, 0, "Taylor.Nader33" },
                    { 15, new Guid("69a219f5-ed62-4690-a8c0-462afa83d8b7"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(900), "36DX5ebzpJ", 15, 36, "Keaton_Zulauf49" },
                    { 16, new Guid("ac22fb83-cc87-4837-869b-8f8c29c5d5fb"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(950), "nk6l8WFJli", 16, 32, "Felix.Lebsack80" },
                    { 17, new Guid("1d8afaf6-abf2-464e-87c2-83d6cdd0e4ae"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1010), "8OjF7zYK_L", 17, 0, "Petra_Dibbert25" },
                    { 18, new Guid("2b8db5bb-327d-4047-acb3-da23007aac5d"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1080), "4Nvk6pkCco", 18, 5, "Hassan_Klocko12" },
                    { 19, new Guid("ac99a32e-db76-4709-8c4c-a3bcb79da0f4"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1140), "R4OxghvzBI", 19, 1, "Carrie_Schoen" },
                    { 20, new Guid("6081fa36-ba67-4541-ba0a-a30d8aa21197"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1200), "yICL_yE3M9", 20, 40, "Pablo47" },
                    { 21, new Guid("6ad541d3-1175-4d42-ad6a-4bf30e325ad6"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1260), "p0YfosKiLz", 21, 6, "Collin_Sauer" },
                    { 22, new Guid("dce1ece7-78b3-46a3-9764-8260ca640756"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1320), "Vt3GyHkTH7", 22, 21, "Santiago_Prosacco" },
                    { 23, new Guid("4c3c1139-b3a7-431d-92e6-f0475ff819d3"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1380), "u5p5yRBZ4K", 23, 19, "Jakob.Senger" },
                    { 24, new Guid("5c2e112d-1604-45a9-a617-0bf9b30cfa5e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1440), "TUgn1Ri5ro", 24, 23, "Reese.Pfannerstill46" },
                    { 25, new Guid("ee680b15-4838-4bc0-9be1-b34c5ad74032"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1510), "vwh5BLRoo7", 25, 37, "Karl33" },
                    { 26, new Guid("6eb5c509-e2b0-407a-9ac7-eb168eda632e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1570), "dPlva8lsUh", 26, 4, "Hallie96" },
                    { 27, new Guid("dc14cf96-38d8-4894-9918-ada452566eda"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1630), "YAgmQ_ms9q", 27, 9, "Milan_Howe" },
                    { 28, new Guid("92d3ec3b-a4ef-43b0-8261-16d0f0fb9cbe"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1680), "sZEgb0W9JZ", 28, 33, "Dedric.Hane52" },
                    { 29, new Guid("8ef18e14-10a3-4948-84c5-438172c25e22"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1730), "xVEGpod8VK", 29, 17, "Althea_Paucek" },
                    { 30, new Guid("61f0054d-2a28-45e4-96ad-e0811e776c68"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1790), "qUJjv6w4a8", 30, 26, "Jerrod_Kuvalis" },
                    { 31, new Guid("c453db88-af3d-4b9c-95cb-08a463018e7a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1860), "qfoQE6bqB8", 31, 12, "Cecil84" },
                    { 32, new Guid("5ef09b93-ff1a-46bd-afcc-56cda547da4a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1920), "lAVsHXaYC7", 32, 18, "Brennan58" },
                    { 33, new Guid("23294821-6aa6-49f6-87ee-8539bf38155a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(1990), "EjYKYYEcpc", 33, 32, "Jessy14" },
                    { 34, new Guid("12adb156-0fd4-474c-b309-5b0374ce4190"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2050), "97ehrhNAvi", 34, 9, "Anabel_Doyle0" },
                    { 35, new Guid("65bec7a8-727b-4bb2-ac8f-182375ca9635"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2100), "MmgwF1QAqB", 35, 43, "Leslie.Hansen86" },
                    { 36, new Guid("bb93b5f3-c5a9-4e8b-b33b-e3a559ea0cbe"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2160), "4Y2qqK1x75", 36, 44, "Vaughn61" },
                    { 37, new Guid("85a3c4bd-487e-4d64-b987-8a93007fc3b1"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2210), "eBEnel1QQe", 37, 34, "Ericka_Bogan" },
                    { 38, new Guid("cd3fd805-2f3e-4710-8e6c-7bb254e445a0"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2280), "O_IEzD51QJ", 38, 24, "Muriel_Bechtelar" },
                    { 39, new Guid("a7e4e036-e087-4ced-a279-541046a3d3ea"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2340), "j28x6b0Dl4", 39, 16, "Sallie.Treutel" },
                    { 40, new Guid("bd4206bc-5015-4d4c-a321-e5fee3c6fe98"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2390), "LD1R9rI5NW", 40, 19, "Kayli_Hahn99" },
                    { 41, new Guid("14a46bb4-0871-4357-a057-c82773cca6f8"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2440), "1gGKoBQLNv", 41, 45, "Cortez97" },
                    { 42, new Guid("f87b7b35-f789-4072-b2c4-c9f7deb07f9a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2500), "HZSZvfHm81", 42, 50, "Alisa_Funk57" },
                    { 43, new Guid("ca3b5275-f54c-4a53-a27c-2e1b8b6b63d9"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2550), "Olug0RttzL", 43, 28, "Darrell.Reinger83" },
                    { 44, new Guid("0ff6d161-e829-4870-a997-b236c63e41de"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2600), "nu5DNLnqYi", 44, 40, "Bennett_Braun" },
                    { 45, new Guid("86b1bd49-29bd-4648-8517-4ea6f4e11452"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2670), "NlWUbVg0aM", 45, 14, "Elwin19" },
                    { 46, new Guid("966465a3-d028-4125-862e-e7f5dd8b0ed1"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2730), "XOLP7wTXhd", 46, 5, "Annabelle_Leuschke" },
                    { 47, new Guid("2663bd3d-7580-4cdf-a1ae-6ad0724e3e30"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2780), "e4kUrRPLVA", 47, 42, "Bertrand_Kulas" },
                    { 48, new Guid("290d239e-fa6f-4bd6-a9d5-e4b5a6b4ec47"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2840), "Zr874SwD3p", 48, 42, "Weston41" },
                    { 49, new Guid("8b8f5582-41d4-4378-a03e-c69e844f8066"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2900), "kDkYBIVSqF", 49, 12, "Evelyn_Kessler1" },
                    { 50, new Guid("db36f193-fc06-48e0-b210-7d11418575cb"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(2950), "NYreIuYnU4", 50, 35, "Janick80" },
                    { 51, new Guid("b6cf5819-d8b6-479d-b1be-730e416e83ce"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3010), "84adEJZ9dl", 51, 44, "Rosie.Herman7" },
                    { 52, new Guid("0a672c06-c431-4bdc-ad0d-8969f4b05566"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3080), "n3EWdEFZ6I", 52, 27, "Charlotte.OConnell" },
                    { 53, new Guid("e3177ac8-094c-4f14-936c-0ea4c781d2f1"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3140), "llCmGmAhqA", 53, 27, "Florian_Mertz39" },
                    { 54, new Guid("74878a1d-56cb-4400-b6df-0159f26b26f9"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3190), "LHMogWVwJ7", 54, 47, "Jody.Streich" },
                    { 55, new Guid("a46fe5bf-72ac-4ec9-a322-2a4b59b81f14"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3240), "iWESLQYx8W", 55, 29, "Doris86" },
                    { 56, new Guid("bd1f7979-0944-47b7-88ed-8540176d254e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3300), "qkUXLQdp1_", 56, 46, "Carey.Kling1" },
                    { 57, new Guid("484024cc-e097-4fdd-9418-0b40e7f18d61"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3350), "mIoZ0OlflZ", 57, 35, "Murl80" },
                    { 58, new Guid("7eca890d-898f-4a1c-ac10-22d277a377f9"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3390), "bPRMsbEwEN", 58, 50, "Giles72" },
                    { 59, new Guid("6f7f7570-0482-4a71-b7bc-6fabcaedb75c"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3470), "NalqL8HOfN", 59, 13, "Jaylin_Ernser23" },
                    { 60, new Guid("4fb5efc0-0467-499d-8689-d26089a4750c"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3530), "AXdBQ3g3kU", 60, 13, "Clementina.Balistreri98" },
                    { 61, new Guid("a39a70c8-52d5-4dc4-ad88-9138ad7addfd"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3590), "CMdiXISw1n", 61, 8, "Jon.Schaden" },
                    { 62, new Guid("c8896cd4-be11-4104-a54e-98d62ea59b57"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3630), "rpjDHJBsgw", 62, 2, "Genesis.Batz" },
                    { 63, new Guid("e4f2b782-7cde-484f-862e-c8bf1dbc0335"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3680), "2MIoapf1uK", 63, 5, "Lilyan_Weimann" },
                    { 64, new Guid("62bf8274-9d38-4dfe-ac43-777a6da61dda"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3730), "5ihgXdJrL6", 64, 22, "Mathew1" },
                    { 65, new Guid("9975c71c-1f3a-4cca-9662-7fb6d9a86b9d"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3800), "HfO9ZRp9rC", 65, 17, "Carroll93" },
                    { 66, new Guid("8c37a30f-f55e-4909-909b-70bb3a93a1f9"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3870), "EzZGCTm2XJ", 66, 15, "Corbin_Hoppe56" },
                    { 67, new Guid("77691323-35a1-4398-8319-8e85fc1c4d0d"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3920), "qthN8qLsxY", 67, 38, "Vernie.Farrell" },
                    { 68, new Guid("026b7128-fe1f-4b6f-8a2e-ebdd6748ab29"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(3970), "CTQ_Hzkf4u", 68, 38, "Patsy.Lakin" },
                    { 69, new Guid("09d8467c-e016-4b96-ae5a-781509ad6cfa"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4020), "GILr1ffb69", 69, 2, "Kasey23" },
                    { 70, new Guid("46c31398-b3d9-4335-9664-3970f676ef2f"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4070), "qXG9nYa4og", 70, 37, "Elinore33" },
                    { 71, new Guid("0be9c027-132e-4a38-8ee8-3d2f8284c2b2"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4130), "m2xhgAa4If", 71, 7, "Keyon41" },
                    { 72, new Guid("d75c6050-96b5-4f45-8b6d-037357334ea8"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4200), "6Cdp89iWXu", 72, 20, "Ryleigh.Russel11" },
                    { 73, new Guid("5cd5f8cf-430a-4193-8ca1-d04326a52d49"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4250), "BQ68Mhr6B2", 73, 11, "Erik98" },
                    { 74, new Guid("73182a49-cccb-4693-ba1f-ff655212805c"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4300), "V7EHo6NZkI", 74, 33, "Karianne.Marquardt22" },
                    { 75, new Guid("a53aaa90-a3ce-4472-82b3-f0b8d0c95cc5"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4350), "EEw0W_nttO", 75, 7, "Beulah_Kreiger77" },
                    { 76, new Guid("9b0c9336-7408-41c4-8b30-604a2d028253"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4430), "BqDgpWzI0M", 76, 24, "Alessandra_Okuneva6" },
                    { 77, new Guid("ec29649d-c612-475b-b761-7e39b7c88fc3"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4490), "kZK86rxCzI", 77, 43, "Oren_Larkin39" },
                    { 78, new Guid("829346e9-0bea-48dc-a63b-b307179d1b13"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4540), "1dCGSeU8jf", 78, 31, "Warren_Runte51" },
                    { 79, new Guid("16c1fb40-32d0-43f9-bc1a-08ef77bbfc57"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4600), "ICTxlGDWMj", 79, 24, "Cora32" },
                    { 80, new Guid("db703fcd-8260-4dce-914c-587d954f852e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4650), "ALq9h_szm5", 80, 11, "Melyssa.Batz82" },
                    { 81, new Guid("f8b3943a-e781-4d08-bcb4-8d4fd147ee67"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4710), "qe0s1VFf40", 81, 22, "Abagail_Crist62" },
                    { 82, new Guid("5785612b-8976-4c70-bb0e-cb5bd475960e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4770), "IvrcWittol", 82, 33, "Oleta_Cole62" },
                    { 83, new Guid("71acce94-d841-4e74-b798-7a38df35e595"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4820), "f9pguCaAnW", 83, 39, "Alberta_Rohan" },
                    { 84, new Guid("26b0c70b-f02c-441b-997a-4ec10a3db37a"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4870), "Z04YT_msDr", 84, 32, "Flavio44" },
                    { 85, new Guid("ef8b50dc-5852-4bcf-8a84-4989834fabbe"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(4930), "YE1Ytu8849", 85, 1, "Leslie35" },
                    { 86, new Guid("91869be9-9984-4154-a3fd-fad22f66adb2"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5000), "gCraTSH_fu", 86, 2, "Loma27" },
                    { 87, new Guid("48708779-3595-4fd7-ac83-452755135ddf"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5050), "GNVyp75by6", 87, 33, "Josiah_Medhurst84" },
                    { 88, new Guid("c87adc5b-997a-4156-8520-e6e662a757ba"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5100), "7_tNODOA0C", 88, 43, "Eleanore.Pagac" },
                    { 89, new Guid("350566d5-b026-48df-9832-6a0c9f99eaca"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5150), "FcPQm3BbzD", 89, 3, "Woodrow99" },
                    { 90, new Guid("f8e63d56-ccf5-4f20-86f6-bc10fdf1f50f"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5200), "GwvdvLWJ16", 90, 1, "Brenna76" },
                    { 91, new Guid("34c9078c-ed96-4058-b855-ce019ec494c4"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5260), "WHnRCrLu_w", 91, 49, "Caesar.McGlynn30" },
                    { 92, new Guid("0d7cfb82-a225-40d2-a0aa-b409bb3f169e"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5320), "ieI61Zwky8", 92, 40, "Sadie.Collier" },
                    { 93, new Guid("443bed13-7c30-45d3-bf38-6ddd830c5640"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5390), "weR7t_qKpe", 93, 2, "Lera.Witting" },
                    { 94, new Guid("8b16b801-0120-4c6d-bba7-e021764db3cf"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5450), "9KR0BjSnTZ", 94, 28, "Patricia_Stroman" },
                    { 95, new Guid("fdc32154-32e4-4071-9e74-1a0fbcd23d06"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5510), "ocZ75M1RH6", 95, 23, "Rowena_Thompson" },
                    { 96, new Guid("7c316c6c-81aa-480c-9721-6e21524bbf39"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5560), "PwWsHCkugY", 96, 47, "Dusty.Sawayn36" },
                    { 97, new Guid("91b701f0-3f68-437c-aa82-d1574e594409"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5610), "79t0AAfJ4_", 97, 42, "Justyn41" },
                    { 98, new Guid("4c69b106-496b-41d9-ba24-fda22b9f071b"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5670), "E3YqiIhMD2", 98, 33, "Gage37" },
                    { 99, new Guid("f47c25e1-bde2-438f-b918-aeca1d3b8bcb"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5720), "ZWSBxCN7UN", 99, 36, "Juwan.Sanford" },
                    { 100, new Guid("7d883325-4746-439f-8a80-8611bfd3173f"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5780), "zJ_4YoDxmy", 100, 18, "Leif_Glover70" },
                    { 101, new Guid("9134f712-0341-4bee-8db7-f6b6915c5997"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5830), "qoX5oKARHR", 101, 1, "Clotilde54" },
                    { 102, new Guid("82e485ff-cfa5-4a1e-a7df-3d180e79d824"), new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(5880), "3UedC_k0VS", 102, 48, "Candida_Terry88" }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "Bio", "Email", "LastUpdated", "UserId" },
                values: new object[,]
                {
                    { 1, ".NET Developer", "sepfrd@outlook.com", new DateTime(2024, 8, 19, 1, 7, 41, 967, DateTimeKind.Local).AddTicks(4430), 1 },
                    { 2, "React Developer", "bercool@gmail.com", new DateTime(2024, 8, 19, 1, 7, 41, 967, DateTimeKind.Local).AddTicks(4870), 2 },
                    { 3, "Regional Division Analyst", "Gabriel.Lynch@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(3660), 3 },
                    { 4, "Future Brand Architect", "Joany.Okuneva29@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4530), 4 },
                    { 5, "Direct Intranet Officer", "Brionna_Kautzer@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4590), 5 },
                    { 6, "Customer Communications Architect", "Veronica81@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4650), 6 },
                    { 7, "Corporate Identity Engineer", "Juana_Wuckert@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4700), 7 },
                    { 8, "Forward Assurance Facilitator", "Frieda15@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4730), 8 },
                    { 9, "National Operations Coordinator", "Bella.Harvey@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4770), 9 },
                    { 10, "Forward Brand Consultant", "Kamille42@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4830), 10 },
                    { 11, "Human Communications Engineer", "Marguerite_Effertz@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4860), 11 },
                    { 12, "Lead Usability Facilitator", "Theodore_Parker91@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4910), 12 },
                    { 13, "Human Quality Technician", "Shanie.Fisher10@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4950), 13 },
                    { 14, "Future Operations Liaison", "Newell_Rice@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(4990), 14 },
                    { 15, "Corporate Communications Administrator", "Raquel_Hermann90@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5030), 15 },
                    { 16, "Direct Response Architect", "Turner.Hartmann14@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5060), 16 },
                    { 17, "Senior Optimization Manager", "Rod.Schulist91@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5120), 17 },
                    { 18, "Principal Usability Coordinator", "Carmella_Lueilwitz@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5160), 18 },
                    { 19, "Lead Integration Planner", "Sigurd.Klein11@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5200), 19 },
                    { 20, "Corporate Accounts Executive", "Daniela49@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5230), 20 },
                    { 21, "Corporate Branding Manager", "Isaac_Sawayn@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5270), 21 },
                    { 22, "Forward Paradigm Agent", "Shawna.Marvin5@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5310), 22 },
                    { 23, "Forward Metrics Planner", "Ignacio_Wilderman@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5340), 23 },
                    { 24, "Investor Directives Executive", "Reuben5@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5380), 24 },
                    { 25, "Corporate Group Manager", "Daniella_Boehm53@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5440), 25 },
                    { 26, "Central Communications Coordinator", "Constantin95@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5480), 26 },
                    { 27, "District Paradigm Assistant", "Aletha92@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5520), 27 },
                    { 28, "Senior Tactics Representative", "Fred.Mohr@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5570), 28 },
                    { 29, "Lead Assurance Representative", "Josefina.Sipes75@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5600), 29 },
                    { 30, "Customer Branding Designer", "Dayna_OConner@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5640), 30 },
                    { 31, "Global Communications Strategist", "Duane_Monahan8@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(5950), 31 },
                    { 32, "Product Research Planner", "Madisyn30@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6010), 32 },
                    { 33, "Customer Response Agent", "Paige.Haag@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6050), 33 },
                    { 34, "Product Factors Facilitator", "Webster_Konopelski11@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6080), 34 },
                    { 35, "Direct Assurance Specialist", "Crawford.Baumbach@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6130), 35 },
                    { 36, "Corporate Security Planner", "Barney.Bode@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6170), 36 },
                    { 37, "Dynamic Quality Supervisor", "Amina_Koepp68@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6200), 37 },
                    { 38, "Internal Usability Planner", "Nico69@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6240), 38 },
                    { 39, "Product Implementation Officer", "Shannon.Gusikowski52@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6270), 39 },
                    { 40, "Future Functionality Strategist", "Elnora_Ullrich@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6340), 40 },
                    { 41, "International Mobility Manager", "Fred.Ledner51@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6380), 41 },
                    { 42, "District Brand Officer", "Bernice20@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6410), 42 },
                    { 43, "Dynamic Operations Representative", "Yolanda.Bogisich@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6440), 43 },
                    { 44, "Direct Group Associate", "Clifton.Langosh@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6480), 44 },
                    { 45, "Direct Communications Officer", "Brandi.Ryan@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6520), 45 },
                    { 46, "Senior Assurance Technician", "Deonte_Cole@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6550), 46 },
                    { 47, "Central Research Specialist", "Austen.Lynch@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6580), 47 },
                    { 48, "Human Response Agent", "Ward_Sawayn@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6630), 48 },
                    { 49, "Customer Tactics Agent", "Kaden45@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6670), 49 },
                    { 50, "Corporate Interactions Liaison", "Tod.Lockman12@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6700), 50 },
                    { 51, "Human Solutions Technician", "Fae_Mante@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6740), 51 },
                    { 52, "Corporate Marketing Technician", "Ramon_Goodwin5@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6770), 52 },
                    { 53, "National Quality Representative", "Lonny40@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6800), 53 },
                    { 54, "Internal Web Developer", "Elena39@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6840), 54 },
                    { 55, "District Metrics Consultant", "Treva_Robel6@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6870), 55 },
                    { 56, "Central Usability Engineer", "Brigitte_White54@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6920), 56 },
                    { 57, "Central Functionality Liaison", "Duncan_Reichert@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(6960), 57 },
                    { 58, "National Data Manager", "Kaleigh15@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7000), 58 },
                    { 59, "Dynamic Implementation Agent", "Maggie_Thompson@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7030), 59 },
                    { 60, "Regional Implementation Specialist", "Xavier.Jacobson99@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7070), 60 },
                    { 61, "Human Assurance Planner", "Christelle26@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7110), 61 },
                    { 62, "Corporate Implementation Liaison", "Althea.Hilpert69@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7140), 62 },
                    { 63, "Dynamic Research Designer", "Vance_OHara52@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7180), 63 },
                    { 64, "Internal Infrastructure Planner", "Alexis_Hand@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7220), 64 },
                    { 65, "Investor Implementation Facilitator", "Nola.Mante81@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7300), 65 },
                    { 66, "Central Functionality Producer", "Connor98@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7340), 66 },
                    { 67, "Dynamic Factors Analyst", "Zita_Zemlak@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7370), 67 },
                    { 68, "Legacy Directives Developer", "Barrett31@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7420), 68 },
                    { 69, "Regional Markets Engineer", "Angelita57@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7450), 69 },
                    { 70, "Global Applications Designer", "Carroll27@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7490), 70 },
                    { 71, "National Branding Analyst", "Justice_Gorczany@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7530), 71 },
                    { 72, "Global Intranet Assistant", "Leanna66@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7570), 72 },
                    { 73, "International Accounts Director", "Otha_Jacobi@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7600), 73 },
                    { 74, "Internal Factors Assistant", "Isadore_Simonis71@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7630), 74 },
                    { 75, "Global Quality Assistant", "Odie_Will27@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7670), 75 },
                    { 76, "Regional Operations Producer", "Camille74@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7720), 76 },
                    { 77, "Dynamic Applications Director", "Crystal16@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7750), 77 },
                    { 78, "Dynamic Creative Facilitator", "Alycia.Kshlerin@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7780), 78 },
                    { 79, "Internal Markets Manager", "Amanda_Roob@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7820), 79 },
                    { 80, "Legacy Brand Assistant", "Verner.Jaskolski@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7850), 80 },
                    { 81, "National Identity Strategist", "Richard.Kerluke@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7890), 81 },
                    { 82, "Chief Communications Analyst", "Eunice_Runolfsson76@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7920), 82 },
                    { 83, "Human Paradigm Technician", "Elvie0@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(7960), 83 },
                    { 84, "Chief Accounts Analyst", "Ryann_Little57@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8010), 84 },
                    { 85, "Customer Division Producer", "Ludwig76@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8040), 85 },
                    { 86, "Investor Factors Facilitator", "Lora.Howell@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8080), 86 },
                    { 87, "Legacy Accounts Director", "Matilde.Batz@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8110), 87 },
                    { 88, "Central Data Coordinator", "Clemmie56@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8140), 88 },
                    { 89, "Chief Interactions Engineer", "Flavio.Gislason@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8180), 89 },
                    { 90, "Global Group Architect", "Wade.Kunde15@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8220), 90 },
                    { 91, "Central Factors Associate", "Kaci.Gibson93@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8250), 91 },
                    { 92, "District Mobility Consultant", "Marlen88@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8300), 92 },
                    { 93, "Investor Creative Assistant", "Weldon_Skiles35@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8330), 93 },
                    { 94, "Corporate Directives Executive", "Ardith_McKenzie@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8360), 94 },
                    { 95, "District Program Developer", "Marlin38@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8400), 95 },
                    { 96, "Direct Functionality Representative", "Garth_Mante@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8440), 96 },
                    { 97, "Corporate Solutions Strategist", "Aidan.Berge@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8480), 97 },
                    { 98, "Forward Identity Analyst", "Kristopher.Herman3@hotmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8510), 98 },
                    { 99, "Dynamic Response Coordinator", "Gavin.Muller@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8570), 99 },
                    { 100, "Dynamic Functionality Specialist", "Susie22@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8600), 100 },
                    { 101, "National Web Administrator", "Kariane0@yahoo.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8640), 101 },
                    { 102, "Legacy Data Director", "Jaeden70@gmail.com", new DateTime(2024, 8, 19, 1, 7, 42, 180, DateTimeKind.Local).AddTicks(8670), 102 }
                });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "Description", "Guid", "LastUpdated", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Please help me with my problem.", new Guid("e89e599c-b2bd-432a-92b5-add1fc8b3dca"), new DateTime(2024, 8, 19, 1, 7, 41, 961, DateTimeKind.Local).AddTicks(6720), "How to do some job?", 1 },
                    { 2, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("e2b39264-fb37-40b3-931c-577a787a3e8c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(840), "withdrawal calculate", 23 },
                    { 3, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("5204a96d-1a2b-4beb-9c22-6c085beb6d84"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1570), "bypass system", 17 },
                    { 4, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("5a465239-5155-4697-9761-85c47cd0c5f6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1610), "EXE Specialist", 63 },
                    { 5, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("0e3f3a81-4442-42f9-9171-8aa8670fdf18"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1640), "productivity Fully-configurable", 76 },
                    { 6, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("09de642d-f60e-4dff-8375-5978a5e791b6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1670), "multi-state backing up", 85 },
                    { 7, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("7e520156-a30f-46c1-864a-734ad4540ff3"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1690), "brand channels", 49 },
                    { 8, "The Football Is Good For Training And Recreational Purposes", new Guid("71989b57-fca3-49d3-9a8d-23797b8e12e3"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1710), "Awesome panel", 89 },
                    { 9, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("cb732664-8767-4661-b6d4-19888c17a8a6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1740), "Specialist Fields", 81 },
                    { 10, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("8701c671-b317-4b15-912c-19dd2c89c800"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1810), "action-items iterate", 98 },
                    { 11, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("bd299293-9473-477e-b35a-bb81f1aaa6ac"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1830), "schemas Steel", 99 },
                    { 12, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("cc513ecb-60fe-4159-9777-0310c031afd3"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1840), "open-source Planner", 65 },
                    { 13, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("4fe0980a-033f-41ad-a369-c75c11c94df4"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1870), "Legacy Michigan", 86 },
                    { 14, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("ffecefb7-e550-48fe-b463-3d8cba1e2b07"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1890), "deposit incentivize", 33 },
                    { 15, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("1407dbcd-7a0e-4cdf-943a-efd77c3c560e"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1900), "Nepal Markets", 6 },
                    { 16, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("62a9c5a9-4014-462d-aa83-cd6abb688853"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1920), "capacitor Organized", 16 },
                    { 17, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("83e9495b-6e6b-4df8-b68e-0ed2944a63af"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1940), "Ameliorated Neck", 51 },
                    { 18, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("bd7977a0-961c-4ed3-9210-c94a414aefb7"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1960), "Kip Hungary", 61 },
                    { 19, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("50234678-f032-4282-9cd5-135dc53d4f1a"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(1990), "Brooks Focused", 51 },
                    { 20, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("fd20f3e6-0dee-46d0-b1ba-7d762e1293b1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2040), "calculate Computers & Home", 82 },
                    { 21, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("170c8ae6-f02d-47a9-8b43-ab83585bdf29"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2090), "Christmas Island white", 78 },
                    { 22, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("fc3aa66a-851d-4ae0-9616-1c103e26975d"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2110), "Director e-enable", 32 },
                    { 23, "The Football Is Good For Training And Recreational Purposes", new Guid("6c450948-b351-4a85-b78d-2ad1618f5728"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2130), "Cotton Lesotho", 87 },
                    { 24, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("ff9ac7fd-5f7e-4ed5-b955-4ca959bcf123"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2150), "magnetic gold", 38 },
                    { 25, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("24074855-cf36-4700-9ddf-444a1727fd0c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2160), "virtual primary", 56 },
                    { 26, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("df89ff84-f064-4607-9db1-e2a0ef585834"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2180), "Concrete Ports", 46 },
                    { 27, "The Football Is Good For Training And Recreational Purposes", new Guid("a77f6679-11ef-4a29-99ad-846183c5e281"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2200), "user-centric Silver", 96 },
                    { 28, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("3be0b102-4693-447f-93f7-ae50b234bdba"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2220), "synergy neural", 43 },
                    { 29, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("7b3039a5-623a-4d16-9389-62e75d97e58b"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2240), "Response navigate", 31 },
                    { 30, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("b1b5724c-c3c8-4ede-bac8-e2310531ccaf"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2250), "RAM District", 87 },
                    { 31, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("1fc59cb1-c530-4721-aa99-6223c0effbd5"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2270), "Armenia Unions", 49 },
                    { 32, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("d84b4900-2dad-42cb-9d7c-567ec0acc46c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2290), "e-business motivating", 38 },
                    { 33, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("f421a47a-759e-483a-8338-a92989b1a940"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2310), "Generic Rubber Soap Games, Automotive & Electronics", 33 },
                    { 34, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("9248dc88-7be2-4612-9f20-ef3beb0fa220"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2350), "Supervisor sky blue", 41 },
                    { 35, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("bb3bcc38-195d-4d15-a280-892b2df6a1bd"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2370), "microchip pricing structure", 44 },
                    { 36, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("ed0a695f-ed11-439e-956e-102409797515"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2380), "maroon Generic Wooden Mouse", 61 },
                    { 37, "The Football Is Good For Training And Recreational Purposes", new Guid("8251b5f0-4b6c-4e23-b5ce-87faa021b4aa"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2410), "3rd generation Legacy", 80 },
                    { 38, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("ef45bb7f-4a09-4dfc-bf6a-8a4aca73e1cc"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2420), "District Mills", 81 },
                    { 39, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("05b2408f-f09a-417e-83a6-395805281f33"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2460), "partnerships RSS", 48 },
                    { 40, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("3392dd53-ebbd-4c9e-9a4e-2dda89488460"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2480), "salmon Indiana", 32 },
                    { 41, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("6fa212b7-223f-4e2a-b3b7-b936425986bf"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2490), "next-generation Credit Card Account", 36 },
                    { 42, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("a5f75c42-b65d-4c11-b6a7-44003ed4626c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2510), "Parks digital", 43 },
                    { 43, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("a45381cd-1df2-43df-9015-2fc153c5e766"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2530), "hacking Panama", 99 },
                    { 44, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("5e1e66b2-d749-4231-97ce-9d76929f0947"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2540), "payment Berkshire", 51 },
                    { 45, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("4d412023-aa54-4360-94f3-4509145ee04d"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2560), "Stravenue grey", 94 },
                    { 46, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("999f0b73-903b-4d47-ab75-1938915a027c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2580), "Branding Chief", 85 },
                    { 47, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("74c6c0d3-37f5-48a9-bc5f-9473b412fb2a"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2600), "Solutions Small", 27 },
                    { 48, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("034e5503-8ade-4474-ad51-4ac4f38571b1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2610), "THX Group", 62 },
                    { 49, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("38007b6c-df51-41da-a853-b53262db4764"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2630), "e-commerce Village", 86 },
                    { 50, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("c9778495-467a-4c1a-b02a-ad97bf1b8704"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2650), "orchestrate Assurance", 69 },
                    { 51, "The Football Is Good For Training And Recreational Purposes", new Guid("290b1831-79e2-44d2-b1c4-2f48829c2413"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2670), "Borders Sleek", 9 },
                    { 52, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("cfa2ddab-bc29-4aef-b612-578781699e01"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2680), "copying Fantastic Granite Towels", 5 },
                    { 53, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("ae603134-d0e1-43a3-a5cf-f87af2ff8ff1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2710), "quantify Savings Account", 32 },
                    { 54, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("ae41e25c-819b-4fd7-9f1b-2f7853c7730b"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2730), "Versatile clicks-and-mortar", 55 },
                    { 55, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("2fa1db84-de5c-4dba-8080-9f4dd3fc0a79"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2740), "bricks-and-clicks focus group", 30 },
                    { 56, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("90e6aa5b-b291-4812-8f78-c08728094886"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2760), "Awesome neural-net", 35 },
                    { 57, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("85acf179-838f-4550-8d5b-adc97578cab0"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2780), "Drive neural-net", 11 },
                    { 58, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("15377022-df9e-413e-86ef-dce9a15a3d6b"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2790), "Indiana Fresh", 87 },
                    { 59, "The Football Is Good For Training And Recreational Purposes", new Guid("306925c7-de5f-4260-939d-50c9b2f98107"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2830), "installation Movies", 95 },
                    { 60, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("febd1613-68c5-4128-9c73-95b21d90aa1c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2850), "Branding 1080p", 89 },
                    { 61, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("603ce8fb-4eaf-4e98-b325-4dd03166cae5"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2860), "Berkshire Overpass", 21 },
                    { 62, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("eead76ab-701d-4fd9-bbb0-7750e695cb10"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2880), "open-source withdrawal", 8 },
                    { 63, "The Football Is Good For Training And Recreational Purposes", new Guid("5fc2be70-40f4-4bcc-8c81-c9bb4fd6e705"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2900), "HTTP Rubber", 91 },
                    { 64, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("8b09134d-0079-4336-9fbb-981d851bfa17"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2910), "Consultant Research", 8 },
                    { 65, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("cfb7868d-151e-4c08-b7dc-ece3bab1cf36"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2930), "Springs Manager", 95 },
                    { 66, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("3d68c0b7-fb9f-4c07-b8ca-1aa2bfac8e45"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2950), "Rustic Plastic Bike generate", 30 },
                    { 67, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("7d3c0bbf-bebf-4acb-bcee-4def2640ada6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2970), "revolutionary connecting", 61 },
                    { 68, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("b323fe68-c2da-4da8-bdb0-6cda04c45cd6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(2990), "implement Liaison", 26 },
                    { 69, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("bc9c7510-bcf2-433e-8137-6b6cb136b11d"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3010), "navigating Electronics & Outdoors", 88 },
                    { 70, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("7b98deb4-4a06-4c4e-80de-220f228ff5e2"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3030), "Mountains Configuration", 98 },
                    { 71, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("914f9bac-5b9d-4581-a59c-5a0118ed5ddf"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3050), "Personal Loan Account Managed", 19 },
                    { 72, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("e893f6f1-6b19-43f1-8ff0-c96e6d4a5df2"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3070), "Lithuania generate", 96 },
                    { 73, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("ebdb6fb1-93a7-47ef-82f8-c591ce553eb7"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3090), "Credit Card Account index", 23 },
                    { 74, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("d13c0db9-727e-4df4-933a-607b59a3f382"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3100), "moderator Steel", 14 },
                    { 75, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("666eb85e-c39d-452a-8440-e2532c9319ce"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3120), "Run Ergonomic Metal Bike", 79 },
                    { 76, "The Football Is Good For Training And Recreational Purposes", new Guid("888fc6ce-1245-465f-bfad-f26858fb5d21"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3140), "Ukraine Buckinghamshire", 83 },
                    { 77, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("5f8b1fc5-6c67-47b9-a4bd-ac46070619f8"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3180), "Rapid Lilangeni", 28 },
                    { 78, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("b52d1f06-0786-4209-94ff-ef1b655f0169"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3200), "Highway transform", 41 },
                    { 79, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("9b92e19d-fb1e-4640-a276-ce2a5eaea719"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3210), "Investment Account Washington", 12 },
                    { 80, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("f4575a77-6c09-4c8e-9368-f1ed51ed58b9"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3230), "Integration Generic Concrete Keyboard", 68 },
                    { 81, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("95210136-ac77-41fe-95b3-c833877208e1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3260), "Programmable Music", 87 },
                    { 82, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("606b3d69-00c2-4036-9ff5-3b84083bd675"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3270), "West Virginia software", 76 },
                    { 83, "The Football Is Good For Training And Recreational Purposes", new Guid("b3ffb05e-57e1-4e0b-8a11-e790f773216a"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3290), "Belarus Functionality", 23 },
                    { 84, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("69a5e564-d9ee-4c52-9252-f04d2ef0f543"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3310), "Proactive Bedfordshire", 85 },
                    { 85, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("50853305-8ff3-4f57-9b6c-da25d60c8167"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3320), "Multi-lateral implement", 17 },
                    { 86, "The Football Is Good For Training And Recreational Purposes", new Guid("32c8f92d-7872-47f6-9f4c-c1b7d6325c11"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3340), "Profound parse", 42 },
                    { 87, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("861a56dd-68b7-4b13-93ab-e4b142a3c64a"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3360), "viral generating", 50 },
                    { 88, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("9afe279b-183e-40ef-bd47-f5c5ad0d4f1c"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3380), "Credit Card Account Cambridgeshire", 7 },
                    { 89, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("53105050-a4c5-4f62-83dd-c3a22e65f8cf"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3390), "Awesome Plastic Shirt Gibraltar Pound", 99 },
                    { 90, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("b73a8b76-9f8b-4901-b6be-7424b4302430"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3440), "Home, Electronics & Clothing Personal Loan Account", 65 },
                    { 91, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("554537bd-8097-45c9-958e-3595d62869f3"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3470), "panel Optimization", 78 },
                    { 92, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("e03a78e7-6779-4cd6-83be-47a099f1569e"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3480), "Yuan Renminbi Sleek Fresh Soap", 6 },
                    { 93, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("3e95e1ed-c371-40c9-9ada-6c7f1b6562e1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3510), "Forward International", 78 },
                    { 94, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("73411d86-570e-4fbf-8158-2617c214ff9a"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3530), "Corporate Investment Account", 22 },
                    { 95, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("1188261d-92ce-47af-a422-170af4ab95dd"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3560), "deposit Implementation", 86 },
                    { 96, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("41c03779-d577-45b6-ae30-a0e5d2fec996"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3580), "tan synthesize", 23 },
                    { 97, "The Football Is Good For Training And Recreational Purposes", new Guid("c844f9bb-f25d-41b7-9958-df63e52423f1"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3590), "Turnpike Liaison", 88 },
                    { 98, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("98588c93-526b-402f-ac1b-01da51c019c6"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3610), "primary initiatives", 19 },
                    { 99, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("07cee6c8-6648-45dd-b884-b22847707485"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3630), "teal Azerbaijanian Manat", 43 },
                    { 100, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("f8115cd1-6a54-4795-af9f-389b79c7c9f5"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3670), "Sleek Metal Shoes interactive", 25 },
                    { 101, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("bbda6f54-e9f9-476d-9aa8-2b368f4585db"), new DateTime(2024, 8, 19, 1, 7, 42, 182, DateTimeKind.Local).AddTicks(3690), "Metal Manor", 57 }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "LastUpdated", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 19, 1, 7, 41, 967, DateTimeKind.Local).AddTicks(5020), 1, 1 },
                    { 2, new DateTime(2024, 8, 19, 1, 7, 41, 967, DateTimeKind.Local).AddTicks(5290), 2, 2 },
                    { 3, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6060), 2, 3 },
                    { 4, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6060), 2, 4 },
                    { 5, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6060), 2, 5 },
                    { 6, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 6 },
                    { 7, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 7 },
                    { 8, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 8 },
                    { 9, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 9 },
                    { 10, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 10 },
                    { 11, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 11 },
                    { 12, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 12 },
                    { 13, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6070), 2, 13 },
                    { 14, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 14 },
                    { 15, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 15 },
                    { 16, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 16 },
                    { 17, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 17 },
                    { 18, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 18 },
                    { 19, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 19 },
                    { 20, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 20 },
                    { 21, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 21 },
                    { 22, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6080), 2, 22 },
                    { 23, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 23 },
                    { 24, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 24 },
                    { 25, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 25 },
                    { 26, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 26 },
                    { 27, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 27 },
                    { 28, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 28 },
                    { 29, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 29 },
                    { 30, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 30 },
                    { 31, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 31 },
                    { 32, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6090), 2, 32 },
                    { 33, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 33 },
                    { 34, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 34 },
                    { 35, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 35 },
                    { 36, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 36 },
                    { 37, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 37 },
                    { 38, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 38 },
                    { 39, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 39 },
                    { 40, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 40 },
                    { 41, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6100), 2, 41 },
                    { 42, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 42 },
                    { 43, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 43 },
                    { 44, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 44 },
                    { 45, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 45 },
                    { 46, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 46 },
                    { 47, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 47 },
                    { 48, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 48 },
                    { 49, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 49 },
                    { 50, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 50 },
                    { 51, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6110), 2, 51 },
                    { 52, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 52 },
                    { 53, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 53 },
                    { 54, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 54 },
                    { 55, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 55 },
                    { 56, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 56 },
                    { 57, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 57 },
                    { 58, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 58 },
                    { 59, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 59 },
                    { 60, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6120), 2, 60 },
                    { 61, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6130), 2, 61 },
                    { 62, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6130), 2, 62 },
                    { 63, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6130), 2, 63 },
                    { 64, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6130), 2, 64 },
                    { 65, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6130), 2, 65 },
                    { 66, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 66 },
                    { 67, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 67 },
                    { 68, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 68 },
                    { 69, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 69 },
                    { 70, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 70 },
                    { 71, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 71 },
                    { 72, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 72 },
                    { 73, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6150), 2, 73 },
                    { 74, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 74 },
                    { 75, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 75 },
                    { 76, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 76 },
                    { 77, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 77 },
                    { 78, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 78 },
                    { 79, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 79 },
                    { 80, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 80 },
                    { 81, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 81 },
                    { 82, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 82 },
                    { 83, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6160), 2, 83 },
                    { 84, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 84 },
                    { 85, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 85 },
                    { 86, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 86 },
                    { 87, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 87 },
                    { 88, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 88 },
                    { 89, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 89 },
                    { 90, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 90 },
                    { 91, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 91 },
                    { 92, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6170), 2, 92 },
                    { 93, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 93 },
                    { 94, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 94 },
                    { 95, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 95 },
                    { 96, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 96 },
                    { 97, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 97 },
                    { 98, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 98 },
                    { 99, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 99 },
                    { 100, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 100 },
                    { 101, new DateTime(2024, 8, 19, 1, 7, 42, 185, DateTimeKind.Local).AddTicks(6180), 2, 101 }
                });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "Description", "Guid", "LastUpdated", "QuestionId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "This is how you do that job.", new Guid("157705b5-470f-46b4-aea9-e3f3ee3a4d05"), new DateTime(2024, 8, 19, 1, 7, 41, 961, DateTimeKind.Local).AddTicks(7360), 1, "Answer for doing some job", 2 },
                    { 2, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("ae275d12-359d-4175-8c5d-025e6312d5d8"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8040), 1, "Incredible parse neural-net", 78 },
                    { 3, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("6976bc65-1bec-479c-99ba-62ef6b1cceca"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8590), 1, "Music Peso Uruguayo Armenia", 45 },
                    { 4, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("cb0b5826-8b38-43a4-909c-df155994344b"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8650), 1, "Uzbekistan Applications Fantastic", 56 },
                    { 5, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("5962e9f5-5d27-4b16-9ce7-41b70cf80644"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8680), 1, "dedicated Garden magenta", 76 },
                    { 6, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("f52b066a-2171-483f-9b5f-76756c068317"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8720), 1, "Solutions payment partnerships", 53 },
                    { 7, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("e3f8fa7f-751f-4556-8cc5-a0a0d73b5011"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8740), 1, "Practical Cotton Chief", 87 },
                    { 8, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("0cd37121-faa0-4ca1-a0d8-b0f0fec1ac02"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8770), 1, "bifurcated Colombian Peso Analyst", 34 },
                    { 9, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("93a272b0-f08c-4343-8dfa-2415cd263cac"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8810), 1, "Small Fresh Shirt visionary infomediaries", 27 },
                    { 10, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("c4f58720-565b-4519-bc16-8a655550fe2b"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8850), 1, "Cayman Islands Dollar Greece Concrete", 42 },
                    { 11, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("02bbeac9-d7d3-4366-8453-b0232c7f28ea"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8880), 1, "uniform Product emulation", 88 },
                    { 12, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("2b30f218-585f-4246-8f5a-c99651b4aa7e"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8910), 1, "Accountability Tasty Soft Shoes Specialist", 87 },
                    { 13, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("e2db1608-d4a0-4303-a67d-1f26efc19f71"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8940), 1, "navigate Business-focused hack", 86 },
                    { 14, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("c6202430-9cce-46ed-9bdf-3c2a99b06650"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8960), 1, "Refined Steel Ball 1080p Drive", 5 },
                    { 15, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("7493069c-d68a-460a-aba1-53f7f46ee7a8"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(8990), 1, "Granite Technician bus", 29 },
                    { 16, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("d3cf742a-2027-4890-9afe-93835830bd60"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9020), 1, "Customer Fantastic Rubber Towels Rubber", 67 },
                    { 17, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("73b3f57a-9deb-489c-a233-a3c18efb3bd6"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9070), 1, "Integration Ergonomic Plastic Gloves help-desk", 28 },
                    { 18, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("29cd5c10-87f4-46da-8347-9118ba2f0bf4"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9100), 1, "Administrator New Taiwan Dollar Savings Account", 98 },
                    { 19, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("ff6d1f7d-804e-491f-a8a1-4d714f507ee8"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9130), 1, "hacking Indian Rupee New Mexico", 65 },
                    { 20, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("6ea4634d-1546-478c-bc3c-0cd7dcbc528e"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9160), 1, "user-centric Ergonomic Granite Salad Dynamic", 8 },
                    { 21, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("dd31536b-5032-4b9f-83c3-0d89ab1c7181"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9190), 1, "Savings Account Buckinghamshire Oklahoma", 90 },
                    { 22, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("df3df259-19d8-4b90-bd73-efcd3c99fe03"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9210), 1, "quantify Maine hacking", 22 },
                    { 23, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("e3feec49-5832-4b82-8002-7e3500db3fd9"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9230), 1, "Singapore Bedfordshire encompassing", 65 },
                    { 24, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("97807cf0-e32f-48b0-859f-e8b9712b72cf"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9260), 1, "Bolivar Fuerte content-based New Mexico", 36 },
                    { 25, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("e467bb42-24b3-4a68-bbf1-c58c409d3b8a"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9280), 1, "Buckinghamshire Savings Account bypass", 41 },
                    { 26, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("3ed4aa6b-6265-49d9-9a3c-09fdf3408e82"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9300), 1, "Ways Manager Mississippi", 45 },
                    { 27, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("723be1bd-8af5-4c11-ba9c-2674c38727ec"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9330), 1, "Books & Sports Qatar Credit Card Account", 87 },
                    { 28, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("eeee7203-2945-489a-9d31-fc9e108be118"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9370), 1, "e-enable Automotive & Outdoors e-enable", 80 },
                    { 29, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("840d6e7c-8d88-4a1f-9229-7cfa61c295d2"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9400), 1, "hack Colombia calculate", 16 },
                    { 30, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("33547b2b-c2fb-4264-89a2-ef7c4adc6020"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9430), 1, "salmon Wooden Washington", 97 },
                    { 31, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("49d88df3-6047-49a9-8991-63d8a9adf9e6"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9450), 1, "overriding deposit Administrator", 64 },
                    { 32, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("616b0891-6920-4ad2-9d71-1f2d0367617d"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9490), 1, "Soft sensor embrace", 45 },
                    { 33, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("d1f3f495-afd8-45f6-8d39-9b686ce7632c"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9510), 1, "Licensed Frozen Pants moderator firewall", 98 },
                    { 34, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("b89e5758-a082-40b7-8de0-8850f58b8a25"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9540), 1, "Drive Home Licensed Wooden Bike", 72 },
                    { 35, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("65595c0f-1226-4ac2-919a-f99f4ac85b2d"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9560), 1, "generate PCI Response", 51 },
                    { 36, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("e1448afe-efb1-47ba-a9aa-97b9c0f8004f"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9590), 1, "optical Supervisor Home Loan Account", 58 },
                    { 37, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("d8ff4e1e-1d78-4848-8a39-5ae73af50403"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9610), 1, "system ivory connect", 96 },
                    { 38, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("0960827a-20bf-46d6-98bc-765ef14d5f99"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9620), 1, "parse back up red", 47 },
                    { 39, "The Football Is Good For Training And Recreational Purposes", new Guid("29894da2-29d5-43a0-9e84-ed7190d8fa9b"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9640), 1, "Licensed Wooden Mouse copy Practical Frozen Bike", 40 },
                    { 40, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("d7531434-bc11-4ad2-b3c7-f424929b5bc5"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9680), 1, "copy e-tailers Nevada", 80 },
                    { 41, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("ccdb427f-26dc-4fc1-8f26-1a21f17ba19d"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9700), 1, "parsing Unbranded Plastic Shoes Tactics", 65 },
                    { 42, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("739ad6c2-8885-4dfd-a876-070181416616"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9720), 1, "methodology Lilangeni migration", 38 },
                    { 43, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("f9b97912-509c-4442-9fd9-7f78569ddbb4"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9760), 1, "Books & Movies Intuitive interface", 62 },
                    { 44, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("b109e18b-b423-4506-b955-a503edd7e27e"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9790), 1, "Credit Card Account Central cross-platform", 35 },
                    { 45, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("cfd7e347-0095-4837-acbe-1b5a2843ad99"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9810), 1, "visualize content Savings Account", 30 },
                    { 46, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("066e2616-a50a-4a61-9956-055df6af3e77"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9830), 1, "Tasty Metal Soap backing up revolutionary", 88 },
                    { 47, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("feca4937-c575-4e92-9998-09adfcd58204"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9860), 1, "Intuitive Bulgaria Hryvnia", 60 },
                    { 48, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("05e7aa4f-691f-4b05-a7e3-b3a013365681"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9900), 1, "Intelligent Gorgeous Rubber Pants Granite", 27 },
                    { 49, "The Football Is Good For Training And Recreational Purposes", new Guid("b6320117-5523-448a-9c30-fd916ad44e2b"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9920), 1, "tan systems Sleek", 12 },
                    { 50, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("248a4a8d-1130-4583-a82d-c5c782d8236e"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9940), 1, "Cape budgetary management Circle", 15 },
                    { 51, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("9280de11-fd62-47ea-b649-b7edd68ac97c"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9960), 1, "bypass benchmark mobile", 24 },
                    { 52, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("5f0b62ba-d16c-4da7-b6b5-912b97ed0c7e"), new DateTime(2024, 8, 19, 1, 7, 42, 174, DateTimeKind.Local).AddTicks(9980), 1, "multi-byte invoice parsing", 86 },
                    { 53, "Ergonomic executive chair upholstered in bonded black leather and PVC padded seat and back for all-day comfort and support", new Guid("23e7ef9d-f002-4298-ae87-33e1c13eb078"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local), 1, "bypassing synthesize Licensed Granite Car", 16 },
                    { 54, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("ab04abea-0cfb-48b2-b990-1266a0bcd93e"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(30), 1, "Intranet Swedish Krona Arizona", 67 },
                    { 55, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("64684649-aa2a-4b8c-991a-3e7317de9000"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(60), 1, "SDD efficient XSS", 68 },
                    { 56, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("48fcbe37-267f-49eb-86a2-68727cbc80ba"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(90), 1, "navigate Small Soft Shirt Home Loan Account", 44 },
                    { 57, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("b0a50908-bddf-4150-b574-b1805030e2b4"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(110), 1, "Glens Investment Account Personal Loan Account", 13 },
                    { 58, "The Football Is Good For Training And Recreational Purposes", new Guid("af12fcfd-cfb8-4f08-9a07-004d0c841454"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(130), 1, "withdrawal Tasty Metal Pants Tools & Jewelery", 76 },
                    { 59, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("dae6ef30-1f49-4c33-a659-7f395cae491c"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(160), 1, "Borders Expanded monitor", 71 },
                    { 60, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("1db6b8d3-44ac-4ec4-a735-101bdb633520"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(190), 1, "Refined Soft Bacon emulation circuit", 26 },
                    { 61, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("0bc74d1e-a95d-46bc-96dc-ffc8107841c3"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(210), 1, "microchip reinvent Computers", 16 },
                    { 62, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("ea884f42-5063-45e6-a22f-a77b13c902a2"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(230), 1, "Dynamic wireless Cambridgeshire", 50 },
                    { 63, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("1e221e6d-b925-4a38-bbfa-b146558937f0"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(250), 1, "orchid Berkshire Manager", 30 },
                    { 64, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("03779367-7c30-4f88-80b1-18f0b8ce3546"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(270), 1, "azure transmitting Unbranded", 84 },
                    { 65, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("e88b41d1-64ac-45d3-8d1a-a81c99e84901"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(290), 1, "convergence Argentina CSS", 78 },
                    { 66, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("e4c91aa9-22e4-4d51-bd49-0865c6353ab1"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(320), 1, "Louisiana protocol Convertible Marks", 93 },
                    { 67, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("c7be3d83-3d1a-4590-bd0d-d4ad96561a49"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(350), 1, "PNG generate Coordinator", 33 },
                    { 68, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("0acedd61-da03-4a5e-b30a-42627050a99f"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(370), 1, "Refined Steel Cheese compelling Assistant", 98 },
                    { 69, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("383b41d0-dd19-45bb-8cff-46b772a410f9"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(400), 1, "technologies Practical Granite Ball Outdoors, Automotive & Shoes", 54 },
                    { 70, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("5c2f60a2-92a2-45b0-87a0-fe0250979977"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(470), 1, "bypass initiatives salmon", 58 },
                    { 71, "The Football Is Good For Training And Recreational Purposes", new Guid("83bb8654-5c72-4fde-a7b0-2a19f571bc3e"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(490), 1, "magenta Graphic Interface Nepalese Rupee", 24 },
                    { 72, "The Football Is Good For Training And Recreational Purposes", new Guid("1056bedd-af3e-4ded-bce3-b55fbb0cbd57"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(520), 1, "Kids, Sports & Movies Director Berkshire", 76 },
                    { 73, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("9e97dd04-e19d-4caa-b417-86d8f2e7a851"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(550), 1, "Analyst superstructure Steel", 73 },
                    { 74, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("1022bf8a-7602-40cc-ba82-8216ab469083"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(570), 1, "South Carolina Ergonomic Rubber Chair Borders", 57 },
                    { 75, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("fffe561c-f34f-4757-bacc-2ae9a7164fb6"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(600), 1, "Ergonomic Soft Hat primary Indiana", 76 },
                    { 76, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("9c020e92-f4e3-449c-b335-1db821cafd76"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(620), 1, "Mobility alarm users", 73 },
                    { 77, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("0eec3ca9-f830-42f5-bd03-e95c87cc4fd3"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(660), 1, "24/7 payment Tactics", 18 },
                    { 78, "The Apollotech B340 is an affordable wireless mouse with reliable connectivity, 12 months battery life and modern design", new Guid("10bc1669-f60a-459e-90ac-6e43ade25187"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(680), 1, "Cambridgeshire holistic JBOD", 63 },
                    { 79, "The automobile layout consists of a front-engine design, with transaxle-type transmissions mounted at the rear of the engine and four wheel drive", new Guid("8c3a9f53-074f-4b20-96c0-bb96a14c97cb"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(700), 1, "Utah Bedfordshire firewall", 72 },
                    { 80, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("5ff5d6b2-98bc-4711-953a-a8ed59c6a611"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(720), 1, "Principal utilisation Liaison", 82 },
                    { 81, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("3afb7628-2d1f-4e44-83b4-92822d6147df"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(740), 1, "ADP Mandatory Generic", 13 },
                    { 82, "The Football Is Good For Training And Recreational Purposes", new Guid("45931c8d-c9a2-4ca7-8f21-b5b17636449a"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(760), 1, "Mississippi Factors Licensed Metal Chair", 74 },
                    { 83, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("1259b8b6-2cb6-4973-ac38-294c9d7cfa5c"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(790), 1, "Frozen Somoni Associate", 70 },
                    { 84, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("4d469f0a-670f-40f7-ab37-6db04717dc1e"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(810), 1, "Palestinian Territory Refined redefine", 86 },
                    { 85, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("5240c785-8bad-4e54-9ca6-d4cd602ff7e3"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(830), 1, "cyan Dam Mills", 10 },
                    { 86, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("cd0b63f8-e695-48db-96f3-b211703ee430"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(860), 1, "Representative Credit Card Account Awesome Plastic Chips", 55 },
                    { 87, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("612e1847-8dfb-4a0c-badc-3f6ccc99a09f"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(880), 1, "Myanmar niches Awesome Concrete Tuna", 3 },
                    { 88, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("d46778d4-5e9d-4db2-a11b-9710b08e77b7"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(910), 1, "Liberian Dollar Assurance Fantastic", 25 },
                    { 89, "Andy shoes are designed to keeping in mind durability as well as trends, the most stylish range of shoes & sandals", new Guid("d225a7a9-c9d1-4581-970f-b69ee3ac5e99"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(930), 1, "red Brook Guernsey", 37 },
                    { 90, "The beautiful range of Apple Naturalé that has an exciting mix of natural ingredients. With the Goodness of 100% Natural Ingredients", new Guid("3ed8ba12-8595-4e06-8740-7586bcfb1b92"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(960), 1, "Tasty Computers Extended", 32 },
                    { 91, "New range of formal shirts are designed keeping you in mind. With fits and styling that will make you stand apart", new Guid("e77f1b82-fb2e-4e7e-af06-41217a961bb5"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(970), 1, "Ergonomic Gorgeous Soft Towels Supervisor", 83 },
                    { 92, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("caeabceb-9cf4-4014-bc5e-b9a6661f4897"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1000), 1, "Walk rich back-end", 3 },
                    { 93, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("6d6d85f6-60e0-49e5-9f23-5064974c1023"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1040), 1, "Illinois application invoice", 77 },
                    { 94, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("371286b4-4aa8-4cda-9ca0-2bf694eafdd0"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1060), 1, "Organized auxiliary District", 91 },
                    { 95, "Boston's most advanced compression wear technology increases muscle oxygenation, stabilizes active muscles", new Guid("a524aafd-7893-4dd1-908b-b8ea01daed3a"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1080), 1, "Steel black red", 62 },
                    { 96, "New ABC 13 9370, 13.3, 5th Gen CoreA5-8250U, 8GB RAM, 256GB SSD, power UHD Graphics, OS 10 Home, OS Office A & J 2016", new Guid("9d2eafdd-7830-4319-933c-47c78e07fa6c"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1100), 1, "payment District Representative", 83 },
                    { 97, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("e0303c5f-2f17-4a69-8bb8-81e3293b536d"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1120), 1, "neural enhance violet", 24 },
                    { 98, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("f995a1ad-f2fb-404b-8d85-b866784198fb"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1140), 1, "program bluetooth instruction set", 63 },
                    { 99, "Carbonite web goalkeeper gloves are ergonomically designed to give easy fit", new Guid("69300303-57c5-4ceb-8368-0e3c897d64e6"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1160), 1, "Incredible Concrete Shoes Illinois PCI", 25 },
                    { 100, "The slim & simple Maple Gaming Keyboard from Dev Byte comes with a sleek body and 7- Color RGB LED Back-lighting for smart functionality", new Guid("038bd0ff-706d-4578-bc43-517fde9162d9"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1190), 1, "Trinidad and Tobago deposit Hong Kong", 15 },
                    { 101, "The Nagasaki Lander is the trademarked name of several series of Nagasaki sport bikes, that started with the 1984 ABC800J", new Guid("483ce464-053c-4893-a3a6-4ffb7c546d2e"), new DateTime(2024, 8, 19, 1, 7, 42, 175, DateTimeKind.Local).AddTicks(1210), 1, "XML Handmade Plastic Bacon Swedish Krona", 91 }
                });

            migrationBuilder.InsertData(
                table: "Bookmarks",
                columns: new[] { "Id", "IsBookmarked", "LastUpdated", "QuestionId", "UserId" },
                values: new object[,]
                {
                    { 1, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(7790), 1, 1 },
                    { 2, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8310), 2, 2 },
                    { 3, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8320), 3, 3 },
                    { 4, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8330), 4, 4 },
                    { 5, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8330), 5, 5 },
                    { 6, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8340), 6, 6 },
                    { 7, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8340), 7, 7 },
                    { 8, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8350), 8, 8 },
                    { 9, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8350), 9, 9 },
                    { 10, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8380), 10, 10 },
                    { 11, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8390), 11, 11 },
                    { 12, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8390), 12, 12 },
                    { 13, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8400), 13, 13 },
                    { 14, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8400), 14, 14 },
                    { 15, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8410), 15, 15 },
                    { 16, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8410), 16, 16 },
                    { 17, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8420), 17, 17 },
                    { 18, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8420), 18, 18 },
                    { 19, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8430), 19, 19 },
                    { 20, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8430), 20, 20 },
                    { 21, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8440), 21, 21 },
                    { 22, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8440), 22, 22 },
                    { 23, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8450), 23, 23 },
                    { 24, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8450), 24, 24 },
                    { 25, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8450), 25, 25 },
                    { 26, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8460), 26, 26 },
                    { 27, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8460), 27, 27 },
                    { 28, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8470), 28, 28 },
                    { 29, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8470), 29, 29 },
                    { 30, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8480), 30, 30 },
                    { 31, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8480), 31, 31 },
                    { 32, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8490), 32, 32 },
                    { 33, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8490), 33, 33 },
                    { 34, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8500), 34, 34 },
                    { 35, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8500), 35, 35 },
                    { 36, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8500), 36, 36 },
                    { 37, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8510), 37, 37 },
                    { 38, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8510), 38, 38 },
                    { 39, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8520), 39, 39 },
                    { 40, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8520), 40, 40 },
                    { 41, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8530), 41, 41 },
                    { 42, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8530), 42, 42 },
                    { 43, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8540), 43, 43 },
                    { 44, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8540), 44, 44 },
                    { 45, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8540), 45, 45 },
                    { 46, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8550), 46, 46 },
                    { 47, true, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8550), 47, 47 },
                    { 48, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8560), 48, 48 },
                    { 49, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8560), 49, 49 },
                    { 50, false, new DateTime(2024, 8, 19, 1, 7, 42, 177, DateTimeKind.Local).AddTicks(8570), 50, 50 }
                });

            migrationBuilder.InsertData(
                table: "QuestionVotes",
                columns: new[] { "Id", "Kind", "LastUpdated", "QuestionId" },
                values: new object[,]
                {
                    { 1, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(6290), 1 },
                    { 2, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7240), 2 },
                    { 3, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7250), 3 },
                    { 4, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7260), 4 },
                    { 5, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7260), 5 },
                    { 6, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7270), 6 },
                    { 7, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7270), 7 },
                    { 8, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7270), 8 },
                    { 9, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7280), 9 },
                    { 10, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7290), 10 },
                    { 11, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7290), 11 },
                    { 12, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7300), 12 },
                    { 13, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7300), 13 },
                    { 14, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7300), 14 },
                    { 15, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7310), 15 },
                    { 16, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7310), 16 },
                    { 17, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7320), 17 },
                    { 18, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7320), 18 },
                    { 19, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7330), 19 },
                    { 20, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7330), 20 },
                    { 21, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7330), 21 },
                    { 22, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7340), 22 },
                    { 23, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7340), 23 },
                    { 24, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7350), 24 },
                    { 25, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7350), 25 },
                    { 26, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7350), 26 },
                    { 27, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7360), 27 },
                    { 28, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7360), 28 },
                    { 29, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7370), 29 },
                    { 30, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7370), 30 },
                    { 31, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7370), 31 },
                    { 32, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7380), 32 },
                    { 33, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7380), 33 },
                    { 34, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7390), 34 },
                    { 35, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7390), 35 },
                    { 36, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7390), 36 },
                    { 37, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7400), 37 },
                    { 38, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7400), 38 },
                    { 39, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7400), 39 },
                    { 40, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7410), 40 },
                    { 41, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7410), 41 },
                    { 42, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7420), 42 },
                    { 43, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7420), 43 },
                    { 44, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7420), 44 },
                    { 45, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7430), 45 },
                    { 46, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7430), 46 },
                    { 47, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7440), 47 },
                    { 48, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7440), 48 },
                    { 49, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7440), 49 },
                    { 50, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7450), 50 },
                    { 51, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7450), 51 },
                    { 52, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7450), 52 },
                    { 53, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7460), 53 },
                    { 54, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7460), 54 },
                    { 55, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7470), 55 },
                    { 56, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7470), 56 },
                    { 57, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7470), 57 },
                    { 58, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7480), 58 },
                    { 59, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7480), 59 },
                    { 60, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7490), 60 },
                    { 61, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7490), 61 },
                    { 62, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7490), 62 },
                    { 63, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7500), 63 },
                    { 64, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7500), 64 },
                    { 65, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7510), 65 },
                    { 66, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7530), 66 },
                    { 67, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7530), 67 },
                    { 68, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7540), 68 },
                    { 69, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7540), 69 },
                    { 70, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7540), 70 },
                    { 71, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7550), 71 },
                    { 72, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7550), 72 },
                    { 73, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7560), 73 },
                    { 74, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7560), 74 },
                    { 75, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7560), 75 },
                    { 76, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7570), 76 },
                    { 77, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7570), 77 },
                    { 78, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7580), 78 },
                    { 79, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7580), 79 },
                    { 80, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7590), 80 },
                    { 81, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7590), 81 },
                    { 82, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7590), 82 },
                    { 83, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7600), 83 },
                    { 84, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7600), 84 },
                    { 85, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7610), 85 },
                    { 86, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7610), 86 },
                    { 87, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7620), 87 },
                    { 88, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7620), 88 },
                    { 89, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7630), 89 },
                    { 90, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7630), 90 },
                    { 91, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7640), 91 },
                    { 92, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7640), 92 },
                    { 93, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7650), 93 },
                    { 94, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7650), 94 },
                    { 95, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7650), 95 },
                    { 96, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7660), 96 },
                    { 97, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7660), 97 },
                    { 98, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7670), 98 },
                    { 99, false, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7670), 99 },
                    { 100, true, new DateTime(2024, 8, 19, 1, 7, 42, 183, DateTimeKind.Local).AddTicks(7680), 100 }
                });

            migrationBuilder.InsertData(
                table: "AnswerVotes",
                columns: new[] { "Id", "AnswerId", "Kind", "LastUpdated" },
                values: new object[,]
                {
                    { 1, 1, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(3790) },
                    { 2, 2, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5260) },
                    { 3, 3, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5270) },
                    { 4, 4, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5280) },
                    { 5, 5, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5280) },
                    { 6, 6, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5290) },
                    { 7, 7, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5290) },
                    { 8, 8, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5290) },
                    { 9, 9, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5300) },
                    { 10, 10, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5300) },
                    { 11, 11, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5310) },
                    { 12, 12, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5310) },
                    { 13, 13, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5320) },
                    { 14, 14, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5320) },
                    { 15, 15, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5330) },
                    { 16, 16, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5330) },
                    { 17, 17, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5330) },
                    { 18, 18, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5340) },
                    { 19, 19, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5340) },
                    { 20, 20, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5350) },
                    { 21, 21, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5350) },
                    { 22, 22, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5360) },
                    { 23, 23, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5360) },
                    { 24, 24, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5370) },
                    { 25, 25, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5370) },
                    { 26, 26, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5370) },
                    { 27, 27, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5380) },
                    { 28, 28, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5380) },
                    { 29, 29, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5390) },
                    { 30, 30, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5390) },
                    { 31, 31, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5400) },
                    { 32, 32, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5400) },
                    { 33, 33, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5400) },
                    { 34, 34, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5410) },
                    { 35, 35, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5420) },
                    { 36, 36, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5420) },
                    { 37, 37, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5430) },
                    { 38, 38, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5430) },
                    { 39, 39, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5430) },
                    { 40, 40, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5440) },
                    { 41, 41, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5440) },
                    { 42, 42, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5450) },
                    { 43, 43, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5450) },
                    { 44, 44, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5450) },
                    { 45, 45, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5460) },
                    { 46, 46, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5460) },
                    { 47, 47, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5470) },
                    { 48, 48, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5470) },
                    { 49, 49, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5480) },
                    { 50, 50, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5480) },
                    { 51, 51, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5480) },
                    { 52, 52, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5490) },
                    { 53, 53, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5490) },
                    { 54, 54, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5500) },
                    { 55, 55, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5500) },
                    { 56, 56, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5500) },
                    { 57, 57, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5510) },
                    { 58, 58, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5510) },
                    { 59, 59, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5520) },
                    { 60, 60, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5520) },
                    { 61, 61, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5520) },
                    { 62, 62, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5530) },
                    { 63, 63, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5530) },
                    { 64, 64, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5540) },
                    { 65, 65, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5540) },
                    { 66, 66, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5550) },
                    { 67, 67, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5550) },
                    { 68, 68, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5550) },
                    { 69, 69, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5560) },
                    { 70, 70, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5560) },
                    { 71, 71, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5570) },
                    { 72, 72, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5570) },
                    { 73, 73, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5580) },
                    { 74, 74, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5580) },
                    { 75, 75, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5580) },
                    { 76, 76, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5590) },
                    { 77, 77, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5590) },
                    { 78, 78, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5600) },
                    { 79, 79, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5600) },
                    { 80, 80, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5600) },
                    { 81, 81, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5610) },
                    { 82, 82, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5610) },
                    { 83, 83, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5620) },
                    { 84, 84, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5620) },
                    { 85, 85, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5630) },
                    { 86, 86, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5650) },
                    { 87, 87, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5650) },
                    { 88, 88, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5660) },
                    { 89, 89, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5660) },
                    { 90, 90, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5670) },
                    { 91, 91, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5670) },
                    { 92, 92, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5670) },
                    { 93, 93, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5680) },
                    { 94, 94, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5680) },
                    { 95, 95, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5690) },
                    { 96, 96, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5690) },
                    { 97, 97, false, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5690) },
                    { 98, 98, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5700) },
                    { 99, 99, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5700) },
                    { 100, 100, true, new DateTime(2024, 8, 19, 1, 7, 42, 176, DateTimeKind.Local).AddTicks(5710) }
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
