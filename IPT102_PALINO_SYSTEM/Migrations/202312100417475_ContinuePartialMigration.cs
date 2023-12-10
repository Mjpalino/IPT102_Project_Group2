namespace IPT102_PALINO_SYSTEM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContinuePartialMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.Users", "RoleId");
            AddForeignKey("dbo.Users", "RoleId", "dbo.Roles", "Id", cascadeDelete: true);
        }
    }
}
