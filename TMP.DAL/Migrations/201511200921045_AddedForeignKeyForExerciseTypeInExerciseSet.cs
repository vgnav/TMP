namespace TMP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedForeignKeyForExerciseTypeInExerciseSet : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseSets", "ExerciseType_ExerciseTypeId", "dbo.ExerciseTypes");
            DropIndex("dbo.ExerciseSets", new[] { "ExerciseType_ExerciseTypeId" });
            RenameColumn(table: "dbo.ExerciseSets", name: "ExerciseType_ExerciseTypeId", newName: "ExerciseTypeId");
            AlterColumn("dbo.ExerciseSets", "ExerciseTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExerciseSets", "ExerciseTypeId");
            AddForeignKey("dbo.ExerciseSets", "ExerciseTypeId", "dbo.ExerciseTypes", "ExerciseTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseSets", "ExerciseTypeId", "dbo.ExerciseTypes");
            DropIndex("dbo.ExerciseSets", new[] { "ExerciseTypeId" });
            AlterColumn("dbo.ExerciseSets", "ExerciseTypeId", c => c.Int());
            RenameColumn(table: "dbo.ExerciseSets", name: "ExerciseTypeId", newName: "ExerciseType_ExerciseTypeId");
            CreateIndex("dbo.ExerciseSets", "ExerciseType_ExerciseTypeId");
            AddForeignKey("dbo.ExerciseSets", "ExerciseType_ExerciseTypeId", "dbo.ExerciseTypes", "ExerciseTypeId");
        }
    }
}
