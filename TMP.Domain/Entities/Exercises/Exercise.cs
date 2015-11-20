namespace TMP.Domain.Entities.Exercises
{
    using System;
    using Metrics;

    public class Exercise
    {       
        public int ExerciseId { get; set; }
        public int SetId { get; set; }          
        public DateTime Performed { get; set; }       
        public BaseMetric Value { get; set; } 
    }
}
