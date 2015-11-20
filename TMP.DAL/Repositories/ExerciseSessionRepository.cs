
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;
    using Domain.Entities.Metrics;

    public class ExerciseSessionContext : DbContext
    {
        public ExerciseSessionContext() : base("TMPApp")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseSession>().ToTable("ExerciseSessions");
        }
    }

    public class ExerciseSessionRepository : GenericRepository<ExerciseSessionContext, ExerciseSession>
    {
        public override ExerciseSession Read(int id)
        {
            return Context.Set<ExerciseSession>().FirstOrDefault(session => session.ExerciseSessionId == id);
        }

        public override void Create(ExerciseSession entity)
        {            
            base.Create(entity);
            // Ensure that ExerciseType objects are not added anew every time            
            //foreach (var set in entity.ExerciseSets)
            //{
            //    if (set.ExerciseType != null && set.ExerciseType.ExerciseTypeId > 0)
            //        Context.Entry(set.ExerciseType).State = EntityState.Unchanged;
            //}
        }

        public override void Update(ExerciseSession entity)
        {
            base.Update(entity);
            // Ensure that ExerciseType objects are not added anew every time
            //foreach (var set in entity.ExerciseSets)
            //{
            //    if (set.ExerciseType != null && set.ExerciseType.ExerciseTypeId > 0)
            //        Context.Entry(set.ExerciseType).State = EntityState.Unchanged;
            //}
        }
    }
}
