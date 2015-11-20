namespace TMP.Domain.Entities.Exercises
{
    using System;
    using Identity;

    public class ExerciseType
    {
        public int ExerciseTypeId { get; set; }
        public string ExerciseName { get; set; }   
        public MetricType MetricType { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public User CreatedBy { get; set; }
    }
}
