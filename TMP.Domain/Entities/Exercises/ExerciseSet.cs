namespace TMP.Domain.Entities.Exercises
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ExerciseSet
    {
        public int ExerciseSetId { get; set; }

        [ForeignKey("ExerciseSession")]
        public int ExerciseSessionId { get; set; }
        public virtual ExerciseSession ExerciseSession { get; set; }
                
        public int ExerciseTypeId { get; set; }
        public virtual ExerciseType ExerciseType { get; set; }

        public virtual List<Exercise> Exercises { get; set; }        

        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }

        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }
}
