namespace TMP.Domain.Entities.Exercises
{
    using System;
    using Metrics;

    public class Exercise
    {       
        public int ExerciseId { get; set; }
        public int ExerciseSetId { get; set; }          
        public DateTime Performed { get; set; }       
        public BaseMetric Value { get; set; } 

        public virtual ExerciseSet ExerciseSet { get; set; }
    }
}
