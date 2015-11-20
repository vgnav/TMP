namespace TMP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseSessions",
                c => new
                    {
                        ExerciseSessionId = c.Int(nullable: false, identity: true),
                        Start = c.DateTime(nullable: false),
                        Stop = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ExerciseSessionId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.ExerciseSets",
                c => new
                    {
                        ExerciseSetId = c.Int(nullable: false, identity: true),
                        ExerciseSessionId = c.Int(nullable: false),
                        ExerciseTypeId = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        Stop = c.DateTime(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseSetId)
                .ForeignKey("dbo.ExerciseSessions", t => t.ExerciseSessionId, cascadeDelete: true)
                .ForeignKey("dbo.ExerciseTypes", t => t.ExerciseTypeId, cascadeDelete: true)
                .Index(t => t.ExerciseSessionId)
                .Index(t => t.ExerciseTypeId);
            
            CreateTable(
                "dbo.Exercises",
                c => new
                    {
                        ExerciseId = c.Int(nullable: false, identity: true),
                        ExerciseSetId = c.Int(nullable: false),
                        MetricId = c.Int(nullable: false),
                        Performed = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseId)
                .ForeignKey("dbo.ExerciseSets", t => t.ExerciseSetId, cascadeDelete: true)
                .ForeignKey("dbo.BaseMetrics", t => t.MetricId, cascadeDelete: true)
                .Index(t => t.ExerciseSetId)
                .Index(t => t.MetricId);
            
            CreateTable(
                "dbo.BaseMetrics",
                c => new
                    {
                        MetricId = c.Int(nullable: false, identity: true),
                        CaloriesBurnt = c.Decimal(precision: 18, scale: 2),
                        Distance = c.Int(),
                        MilliSeconds = c.Long(),
                        Number = c.Int(),
                        WeightKG = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.MetricId);
            
            CreateTable(
                "dbo.ExerciseTypes",
                c => new
                    {
                        ExerciseTypeId = c.Int(nullable: false, identity: true),
                        ExerciseName = c.String(),
                        MetricType = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Modified = c.DateTime(nullable: false),
                        CreatedBy_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.ExerciseTypeId)
                .ForeignKey("dbo.Users", t => t.CreatedBy_UserId)
                .Index(t => t.CreatedBy_UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        IdnetityProviderId = c.String(),
                        Name = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(),
                        Joined = c.DateTime(nullable: false),
                        LastLoggedIn = c.DateTime(),
                        SubscriptionLevel = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseSessions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.ExerciseSets", "ExerciseTypeId", "dbo.ExerciseTypes");
            DropForeignKey("dbo.ExerciseTypes", "CreatedBy_UserId", "dbo.Users");
            DropForeignKey("dbo.ExerciseSets", "ExerciseSessionId", "dbo.ExerciseSessions");
            DropForeignKey("dbo.Exercises", "MetricId", "dbo.BaseMetrics");
            DropForeignKey("dbo.Exercises", "ExerciseSetId", "dbo.ExerciseSets");
            DropIndex("dbo.ExerciseTypes", new[] { "CreatedBy_UserId" });
            DropIndex("dbo.Exercises", new[] { "MetricId" });
            DropIndex("dbo.Exercises", new[] { "ExerciseSetId" });
            DropIndex("dbo.ExerciseSets", new[] { "ExerciseTypeId" });
            DropIndex("dbo.ExerciseSets", new[] { "ExerciseSessionId" });
            DropIndex("dbo.ExerciseSessions", new[] { "User_UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.ExerciseTypes");
            DropTable("dbo.BaseMetrics");
            DropTable("dbo.Exercises");
            DropTable("dbo.ExerciseSets");
            DropTable("dbo.ExerciseSessions");
        }
    }
}
