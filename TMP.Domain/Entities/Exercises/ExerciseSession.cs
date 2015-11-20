namespace TMP.Domain.Entities.Exercises
{
    using System;
    using System.Collections.Generic;
    using Identity;

    public class ExerciseSession
    {
        public int ExerciseSessionId { get; set; }
        public User User { get; set; }
        public List<ExerciseSet> ExerciseSets { get; set; }

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
