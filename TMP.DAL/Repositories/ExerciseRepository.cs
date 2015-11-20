namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    internal class ExerciseContext : DbContext
    {
        internal ExerciseContext(string connectionString) : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Exercise>().ToTable("Exercises");
        }
    }

    public class ExerciseRepository : BaseRepository<Exercise>
    {
        public ExerciseRepository(string connectionString)
        {
            Context = new ExerciseContext(connectionString);
        }

        public override Exercise Read(int id)
        {
            return Context.Set<Exercise>().FirstOrDefault(exercise => exercise.ExerciseId == id);
        }
    }
}
