namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig_AboutEdit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Abouts", "Activation", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Abouts", "Activation");
        }
    }
}
