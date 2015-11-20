

namespace TMP.ConsoleApp
{
    using System;
    using DAL.Repositories;
    using Domain.Entities.Exercises;
    using Domain.Entities.Identity;

    class Program
    {
        static void Main(string[] args)
        {          

            var repo = new ExerciseTypeRepository();
            
            var exc = new ExerciseType
            {
                Created = DateTime.Now,
                CreatedBy = null,
                ExerciseName = "Test",
                MetricType = Domain.Entities.MetricType.REP,
                Modified = DateTime.Now
            };

            repo.Create(exc);
            repo.Save();

            Console.Write("Hello");
            Console.Read();
        }
    }
}
