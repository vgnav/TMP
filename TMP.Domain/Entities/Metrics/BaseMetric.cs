namespace TMP.Domain.Entities.Metrics
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using TMP.Domain.Entities.Exercises;

    public abstract class BaseMetric
    {   
        [Key, ForeignKey("Exercise")]             
        public int MetricId { get; set; }        
        public int ExerciseId { get; set; }        
        public virtual Exercise Exercise { get; set; } 
    }
}
