namespace TMP.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedMetric : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exercises", "Metric_MetricId", "dbo.BaseMetrics");
            DropIndex("dbo.Exercises", new[] { "Metric_MetricId" });
            RenameColumn(table: "dbo.Exercises", name: "Metric_MetricId", newName: "MetricId");
            AlterColumn("dbo.Exercises", "MetricId", c => c.Int(nullable: false));
            CreateIndex("dbo.Exercises", "MetricId");
            AddForeignKey("dbo.Exercises", "MetricId", "dbo.BaseMetrics", "MetricId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exercises", "MetricId", "dbo.BaseMetrics");
            DropIndex("dbo.Exercises", new[] { "MetricId" });
            AlterColumn("dbo.Exercises", "MetricId", c => c.Int());
            RenameColumn(table: "dbo.Exercises", name: "MetricId", newName: "Metric_MetricId");
            CreateIndex("dbo.Exercises", "Metric_MetricId");
            AddForeignKey("dbo.Exercises", "Metric_MetricId", "dbo.BaseMetrics", "MetricId");
        }
    }
}
