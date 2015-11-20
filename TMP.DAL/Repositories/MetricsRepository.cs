

namespace TMP.DAL.Repositories
{
    using System.Data.Entity;
    using Domain.Entities.Metrics;
    using System.Linq;

    #region Database Contexts
    //public abstract class BaseMetricContext : DbContext
    //{
    //    public BaseMetricContext() : base("TMPApp")
    //    { }
    //}

    public class WeightMetricContext : DbContext
    {
        public WeightMetricContext() : base("TMPApp")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeightMetric>().ToTable("WeightMetrics");
        }
    }
    #endregion

    public class WeightMetricRepository : GenericRepository<WeightMetricContext, WeightMetric>
    {
        public override WeightMetric Read(int id)
        {
            return Context.Set<WeightMetric>().FirstOrDefault(metric => metric.MetricId == id);
        }
    }
}
