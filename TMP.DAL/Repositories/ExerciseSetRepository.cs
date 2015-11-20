
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    public class ExerciseSetContext : DbContext
    {
        public ExerciseSetContext() : base("TMPApp")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseSet>().ToTable("ExerciseSets");
        }
    }

    public class ExerciseSetRepository : GenericRepository<ExerciseSetContext, ExerciseSet>    
    {
        public override ExerciseSet Read(int id)
        {
            return Context.Set<ExerciseSet>().FirstOrDefault(exerciseSet => exerciseSet.ExerciseSetId == id);
        }
    }
}
