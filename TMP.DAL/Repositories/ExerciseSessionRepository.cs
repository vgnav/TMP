
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
    }
}
