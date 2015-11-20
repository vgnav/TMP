namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    public class ExerciseContext : DbContext
    {
        public ExerciseContext() : base("TMPApp")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().ToTable("Exercises");
        }
    }

    public class ExerciseRepository : GenericRepository<ExerciseContext, Exercise>
    {
        public override Exercise Read(int id)
        {
            return Context.Set<Exercise>().FirstOrDefault(exercise => exercise.ExerciseId == id);
        }
    }
}
