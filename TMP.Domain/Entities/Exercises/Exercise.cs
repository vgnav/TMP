namespace TMP.Domain.Entities.Exercises
{
    using System;
    using Metrics;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Exercise
    {       
        public int ExerciseId { get; set; }

        [ForeignKey("ExerciseSet")]
        public int ExerciseSetId { get; set; }
        public virtual ExerciseSet ExerciseSet { get; set; }
                
        public int MetricId { get; set; }
        public virtual BaseMetric Metric { get; set; }

        public DateTime Performed { get; set; }       
    }
}
