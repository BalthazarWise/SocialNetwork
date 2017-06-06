namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GroupPosts", "Post", c => c.String(maxLength: 300));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GroupPosts", "Post", c => c.String());
        }
    }
}
