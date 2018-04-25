namespace MusicStream.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "PlayListId", "dbo.Playlists");
            DropIndex("dbo.Songs", new[] { "PlayListId" });
            DropColumn("dbo.Songs", "PlayListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "PlayListId", c => c.Int(nullable: false));
            CreateIndex("dbo.Songs", "PlayListId");
            AddForeignKey("dbo.Songs", "PlayListId", "dbo.Playlists", "PlaylistID", cascadeDelete: true);
        }
    }
}
