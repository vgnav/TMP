namespace TMP.Domain.Entities.Metrics
{
    public abstract class BaseMetric
    {        
        public int MetricId { get; set; }        
        public int ExerciseId { get; set; }        
    }
}
