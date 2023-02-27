using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace project_managment_hu.Migrations
{
    public partial class Labeldbsss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Issuseslabels");

            migrationBuilder.CreateTable(
                name: "IssueLabels",
                columns: table => new
                {
                    IssueId = table.Column<int>(type: "int", nullable: false),
                    LabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueLabels", x => new { x.IssueId, x.LabelId });
                    table.ForeignKey(
                        name: "FK_IssueLabels_issuses_IssueId",
                        column: x => x.IssueId,
                        principalTable: "issuses",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueLabels_labels_LabelId",
                        column: x => x.LabelId,
                        principalTable: "labels",
                        principalColumn: "labelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IssueLabels_LabelId",
                table: "IssueLabels",
                column: "LabelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueLabels");

            migrationBuilder.CreateTable(
                name: "Issuseslabels",
                columns: table => new
                {
                    issusesIssueId = table.Column<int>(type: "int", nullable: false),
                    labelslabelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issuseslabels", x => new { x.issusesIssueId, x.labelslabelId });
                    table.ForeignKey(
                        name: "FK_Issuseslabels_issuses_issusesIssueId",
                        column: x => x.issusesIssueId,
                        principalTable: "issuses",
                        principalColumn: "IssueId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Issuseslabels_labels_labelslabelId",
                        column: x => x.labelslabelId,
                        principalTable: "labels",
                        principalColumn: "labelId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Issuseslabels_labelslabelId",
                table: "Issuseslabels",
                column: "labelslabelId");
        }
    }
}
