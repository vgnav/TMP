
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    internal class ExerciseSessionContext : DbContext
    {
        public ExerciseSessionContext() : base("TMPApp")
        { }
        public ExerciseSessionContext(string connectionString) : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<ExerciseSession>().ToTable("ExerciseSessions");
        }
    }

    public class ExerciseSessionRepository : BaseRepository<ExerciseSession>
    {
        public ExerciseSessionRepository(string connectionString)
        {
            Context = new ExerciseSessionContext(connectionString);
        }

        public override ExerciseSession Read(int id)
        {
            return Context.Set<ExerciseSession>().FirstOrDefault(session => session.ExerciseSessionId == id);
        }        
    }
}
