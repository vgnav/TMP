

namespace TMP.ConsoleApp
{
    using System;
    using DAL.Repositories;
    using Domain.Entities.Exercises;
    using Domain.Entities.Identity;
    using Domain.Entities.Metrics;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {

            //var exerciseTypeRepo = new ExerciseTypeRepository();

            //var exc = new ExerciseType
            //{
            //    Created = DateTime.Now,
            //    CreatedBy = null,
            //    ExerciseName = "Test",
            //    MetricType = Domain.Entities.MetricType.REP,
            //    Modified = DateTime.Now
            //};

            //exerciseTypeRepo.Create(exc);
            //exerciseTypeRepo.Save();

            var ex = (new ExerciseTypeRepository()).Read(1);

            //var sesion = new ExerciseSession
            //{
            //    Created = DateTime.Now,
            //    Modified = DateTime.Now,
            //    User = null,
            //    Start = DateTime.Now,
            //    Stop = DateTime.Now
            //};
            //var set = new ExerciseSet
            //{
            //    Created = DateTime.Now,
            //    Modified = DateTime.Now,
            //    ExerciseType = ex,
            //    Start = DateTime.Now,
            //    Stop = DateTime.Now,
            //    ExerciseSession = sesion
            //};
            //var exercise = new Exercise
            //{
            //    Performed = DateTime.Now,
            //    ExerciseSet = set                
            //};            
            //exercise.Metric = new WeightMetric
            //{
            //    Exercise = exercise,
            //    Number = 10,
            //    WeightKG = 10
            //};
            //set.Exercises = new List<Exercise> { exercise };
            //sesion.ExerciseSets = new List<ExerciseSet> { set };


            var session = new ExerciseSession
            {
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Start = DateTime.Now,
                Stop = DateTime.Now,
                User = null,
                ExerciseSets = new List<ExerciseSet>
                {
                    new ExerciseSet
                    {
                        Created = DateTime.Now,
                        Exercises = new List<Exercise>
                        {
                            new Exercise
                            {
                                Performed = DateTime.Now,
                                Metric = new WeightMetric
                                {
                                    Number = 12,
                                    WeightKG = 40
                                }
                            }
                        },
                        Start = DateTime.Now,
                        Stop = DateTime.Now,
                        Modified = DateTime.Now,
                        ExerciseType = ex
                    }
                },
            };

            var repo = new ExerciseSessionRepository();
            repo.Create(session);
            repo.Save();

            var se = repo.Read(2);

            Console.Write("Hello");
            Console.Read();
        }
    }
}
