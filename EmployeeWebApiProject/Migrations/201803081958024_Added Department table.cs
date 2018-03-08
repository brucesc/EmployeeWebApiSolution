namespace EmployeeWebApiProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDepartmenttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        Budget = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Departments", new[] { "EmployeeId" });
            DropTable("dbo.Departments");
        }
    }
}
