
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    internal class ExerciseSetContext : DbContext
    {
        public ExerciseSetContext(string connectionString) : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseSet>().ToTable("ExerciseSets");
        }
    }

    public class ExerciseSetRepository : BaseRepository<ExerciseSet>    
    {
        public ExerciseSetRepository(string connectionString)
        {
            Context = new ExerciseSetContext(connectionString);
        }

        public override ExerciseSet Read(int id)
        {
            return Context.Set<ExerciseSet>().FirstOrDefault(exerciseSet => exerciseSet.ExerciseSetId == id);
        }
    }
}
