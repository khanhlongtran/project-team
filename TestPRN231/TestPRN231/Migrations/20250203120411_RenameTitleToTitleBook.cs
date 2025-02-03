using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestPRN231.Migrations
{
    public partial class RenameTitleToTitleBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Author",
                columns: table => new
                {
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Author", x => x.AuthorID);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreID);
                });

            migrationBuilder.CreateTable(
                name: "Library",
                columns: table => new
                {
                    LibraryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LibraryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Library", x => x.LibraryID);
                });

            migrationBuilder.CreateTable(
                name: "Publisher",
                columns: table => new
                {
                    PublisherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublisherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publisher", x => x.PublisherID);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleBook = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "date", nullable: true),
                    PublisherID = table.Column<int>(type: "int", nullable: true),
                    GenreID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                    table.ForeignKey(
                        name: "FK__Book__GenreID__4222D4EF",
                        column: x => x.GenreID,
                        principalTable: "Genre",
                        principalColumn: "GenreID");
                    table.ForeignKey(
                        name: "FK__Book__PublisherI__4316F928",
                        column: x => x.PublisherID,
                        principalTable: "Publisher",
                        principalColumn: "PublisherID");
                });

            migrationBuilder.CreateTable(
                name: "BookAuthor",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false),
                    AuthorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookAuth__6AED6DE6900A193A", x => new { x.BookID, x.AuthorID });
                    table.ForeignKey(
                        name: "FK__BookAutho__Autho__440B1D61",
                        column: x => x.AuthorID,
                        principalTable: "Author",
                        principalColumn: "AuthorID");
                    table.ForeignKey(
                        name: "FK__BookAutho__BookI__44FF419A",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                });

            migrationBuilder.CreateTable(
                name: "LibraryBook",
                columns: table => new
                {
                    LibraryID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LibraryB__C2E84B9D330632F0", x => new { x.LibraryID, x.BookID });
                    table.ForeignKey(
                        name: "FK__LibraryBo__BookI__45F365D3",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID");
                    table.ForeignKey(
                        name: "FK__LibraryBo__Libra__46E78A0C",
                        column: x => x.LibraryID,
                        principalTable: "Library",
                        principalColumn: "LibraryID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_GenreID",
                table: "Book",
                column: "GenreID");

            migrationBuilder.CreateIndex(
                name: "IX_Book_PublisherID",
                table: "Book",
                column: "PublisherID");

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthor_AuthorID",
                table: "BookAuthor",
                column: "AuthorID");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryBook_BookID",
                table: "LibraryBook",
                column: "BookID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthor");

            migrationBuilder.DropTable(
                name: "LibraryBook");

            migrationBuilder.DropTable(
                name: "Author");

            migrationBuilder.DropTable(
                name: "Book");

            migrationBuilder.DropTable(
                name: "Library");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Publisher");
        }
    }
}
