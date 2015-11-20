namespace TMP.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Domain.Entities.Exercises;

    internal sealed class Configuration : DbMigrationsConfiguration<TMP.DAL.Repositories.ExerciseSessionContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TMP.DAL.Repositories.ExerciseSessionContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var exerciseType = new ExerciseType
            {
                Created = DateTime.Now,
                ExerciseName = "Bench Press",
                CreatedBy = null,
                MetricType = Domain.Entities.MetricType.WEIGHT,
                Modified = DateTime.Now
            };

            context.Set<ExerciseType>().AddOrUpdate(exerciseType);
            context.SaveChanges();
        }
    }
}
