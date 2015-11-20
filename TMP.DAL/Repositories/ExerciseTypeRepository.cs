
namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Exercises;
    using System.Linq;

    public class ExerciseTypeContext : DbContext
    {
        public ExerciseTypeContext() : base("ExerciseTypeContext")
        { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseType>().ToTable("ExerciseTypes");
        }
    }

    public class ExerciseTypeRepository : GenericRepository<ExerciseTypeContext, ExerciseType>
    {
        public override ExerciseType Read(int id)
        {
            return Context.Set<ExerciseType>().FirstOrDefault(excType => excType.ExerciseTypeId == id);
        }
    }
}
