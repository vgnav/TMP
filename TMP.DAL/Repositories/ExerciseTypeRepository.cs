
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    internal class ExerciseTypeContext : DbContext
    {
        public ExerciseTypeContext(string connectionString) : base(connectionString)
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseType>().ToTable("ExerciseTypes");
        }
    }

    public class ExerciseTypeRepository : BaseRepository<ExerciseType>
    {
        public ExerciseTypeRepository(string connectionString)
        {
            Context = new ExerciseTypeContext(connectionString);
        }

        public override ExerciseType Read(int id)
        {
            return Context.Set<ExerciseType>().FirstOrDefault(excType => excType.ExerciseTypeId == id);
        }
    }
}
