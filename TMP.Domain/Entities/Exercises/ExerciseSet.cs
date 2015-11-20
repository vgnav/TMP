namespace TMP.Domain.Entities.Exercises
{
    using System;
    using System.Collections.Generic;

    public class ExerciseSet
    {
        public int ExerciseSetId { get; set; }
        public int ExerciseSessionId { get; set; }
        
        public ExerciseType ExerciseType { get; set; }
        public List<Exercise> Exercises { get; set; }

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
