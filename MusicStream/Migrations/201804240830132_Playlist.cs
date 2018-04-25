namespace MusicStream.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playlist : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "PlayListId", c => c.Int());
            CreateIndex("dbo.Songs", "PlayListId");
            AddForeignKey("dbo.Songs", "PlayListId", "dbo.Playlists", "PlaylistID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "PlayListId", "dbo.Playlists");
            DropIndex("dbo.Songs", new[] { "PlayListId" });
            DropColumn("dbo.Songs", "PlayListId");
        }
    }
}
