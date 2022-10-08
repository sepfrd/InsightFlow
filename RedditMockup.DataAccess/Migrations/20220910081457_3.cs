using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedditMockup.DataAccess.Migrations
{
    public partial class _3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Roles");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Answers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "Family", "LastUpdated", "Name" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3440), "Foroughi Rad", new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3440), "Sepehr" });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "Family", "LastUpdated", "Name" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3447), "BooAzaar", new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3447), "Abbas" });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3902), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3902) });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3907), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3907) });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreationDate", "Description", "LastUpdated", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 10, 12, 44, 56, 519, DateTimeKind.Local).AddTicks(9866), "ebfruvunaaourimuidtiouituaaidi", new DateTime(2022, 9, 10, 12, 44, 56, 519, DateTimeKind.Local).AddTicks(9866), "eeedaeit", 2 },
                    { 2, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(772), "oirsuuitlaeopgustasnaleutcfnet", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(772), "vmsmeeln", 1 },
                    { 3, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1149), "sidiemionttiicueeaeoiaieunusqr", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1149), "ttuetaix", 2 },
                    { 4, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1500), "ilqeaunfooootaaircnsacrestusxe", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1500), "isutrvte", 1 },
                    { 5, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1824), "ieexaaauuviecicneirabspsiideie", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(1824), "casmmtqe", 1 },
                    { 6, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2144), "amdiclstmicaeaaiunqeuseuittade", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2144), "nsseqate", 1 },
                    { 7, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2481), "souqlcvtpeiieieeqapinpvnpoibus", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2481), "rcemibfu", 1 },
                    { 8, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2796), "urcivnoeturrretitesstutdqtgrmm", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(2796), "uooattab", 1 },
                    { 9, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(3111), "ciuouinuatuallenosoatbuiesovur", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(3111), "lodilimd", 2 },
                    { 10, new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(3436), "rtmbtpmiqtomgslcpeeiiovlmsniit", new DateTime(2022, 9, 10, 12, 44, 56, 520, DateTimeKind.Local).AddTicks(3436), "seoirtxv", 1 }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3372), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3372) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3413), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3413) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3979), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3979) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3984), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3984) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated", "Password", "Username" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3467), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3467), "7f13d2c6d8191aaec19c5bb484deb750d7c79ce6d546815acbde63cbd857053310492804a674a16bfb18550e3e16df3c4bbd9801288e735057eb5010caa37ab8", "sepehr_frd" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated", "Password", "Username" },
                values: new object[] { new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3815), new DateTime(2022, 9, 10, 12, 44, 56, 516, DateTimeKind.Local).AddTicks(3815), "7cabe918dbd1e1ddbf57e0c17c636f6ce8153bbbd7c460edc774b09dfde1e42fa63501e088c6df3bb630fba5e92781aa0997aca1d2a4aee63c93348bf61f2576", "abbas_booazaar" });

            migrationBuilder.InsertData(
                table: "Answers",
                columns: new[] { "Id", "CreationDate", "Description", "LastUpdated", "QuestionId", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(2430), "tqirnrastseqiocnsoemisrmhdapie", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(2430), 1, "inosefaa", 1 },
                    { 2, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(3332), "qsiiauiuteeotouuteliutgerqneen", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(3332), 1, "silnafem", 2 },
                    { 3, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(3676), "iaquterntmtutuqsmshdutttliqmeo", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(3676), 1, "atufeiui", 2 },
                    { 4, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4001), "qtuderegeeieitnsueqeecosetmecs", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4001), 1, "siamitle", 2 },
                    { 5, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4318), "qnntueeifduluuuisavaienuueltsl", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4318), 1, "epltueol", 1 },
                    { 6, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4647), "qmuueidtiueelattinosnaulqmiate", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4647), 1, "nuvesett", 2 },
                    { 7, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4955), "tteetooageveeaaauoalaimeapucom", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(4955), 1, "hadhlmtn", 2 },
                    { 8, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5255), "rsunsluqoenpneqtseonmreamcrasa", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5255), 1, "mansaoia", 2 },
                    { 9, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5562), "exsidoaeniirgonueooeetsnamntmi", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5562), 1, "eiolatlm", 2 },
                    { 10, new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5884), "mtneauostiauqxsaciiosqtnnabpve", new DateTime(2022, 9, 10, 12, 44, 56, 523, DateTimeKind.Local).AddTicks(5884), 1, "sitmaini", 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers");

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Answers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Answers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "Family", "LastUpdated", "Name" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6770), "Hoorbakht", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6770), "Mahyar" });

            migrationBuilder.UpdateData(
                table: "Persons",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "Family", "LastUpdated", "Name" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6780), "Foroughi Rad", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6780), "Sepehr" });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Profiles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7110) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "Description", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6700), "Admin of Application", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "Description", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6750), "User of Application", new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6750) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7120), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7130), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7130) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreationDate", "LastUpdated", "Password", "Username" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6800), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(6800), "519651f64e305662a4a9e64d516ccafc06442a3cc3e61dbec98ffe5b407e4daeb8df1612c22c94f0c2e737618a3944c4e8864d07e8524d4109942f4a9747c472", "admin_admin" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreationDate", "LastUpdated", "Password", "Username" },
                values: new object[] { new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7060), new DateTime(2022, 7, 22, 15, 55, 9, 264, DateTimeKind.Local).AddTicks(7060), "7f13d2c6d8191aaec19c5bb484deb750d7c79ce6d546815acbde63cbd857053310492804a674a16bfb18550e3e16df3c4bbd9801288e735057eb5010caa37ab8", "sepehr_frd" });

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Users_UserId",
                table: "Answers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
