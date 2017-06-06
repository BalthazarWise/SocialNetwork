namespace SocialNetwork.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Post = c.String(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.GroupPostApplicationUsers",
                c => new
                    {
                        GroupPost_Id = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.GroupPost_Id, t.ApplicationUser_Id })
                .ForeignKey("dbo.GroupPosts", t => t.GroupPost_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.GroupPost_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupPostApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupPostApplicationUsers", "GroupPost_Id", "dbo.GroupPosts");
            DropForeignKey("dbo.GroupPosts", "Group_Id", "dbo.Groups");
            DropIndex("dbo.GroupPostApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GroupPostApplicationUsers", new[] { "GroupPost_Id" });
            DropIndex("dbo.GroupPosts", new[] { "Group_Id" });
            DropTable("dbo.GroupPostApplicationUsers");
            DropTable("dbo.GroupPosts");
        }
    }
}
