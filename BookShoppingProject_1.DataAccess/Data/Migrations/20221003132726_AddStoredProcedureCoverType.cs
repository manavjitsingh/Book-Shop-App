using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShoppingProject_1.Data.Migrations
{
    public partial class AddStoredProcedureCoverType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverTypes
                                    As
                                    Select * from CoverTypes");
            migrationBuilder.Sql(@"CREATE PROCEDURE GetCoverType
                                        @Id int
                                    As
                                    Select * from CoverTypes where Id=@Id");
            migrationBuilder.Sql(@"CREATE PROCEDURE CreateCoverType
                                        @Name varchar(50)
                                    As
                                    insert CoverTypes values (@Name)");
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateCoverType
                                        @Id int,
                                        @Name varchar(50)
                                    As
                                    update CoverTypes set Name=@Name where Id=@Id");
            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteCoverType
                                        @Id int
                                    As
                                    delete CoverTypes where Id=@Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
