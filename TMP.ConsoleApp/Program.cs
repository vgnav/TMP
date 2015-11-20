

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

            // var ex = (new ExerciseTypeRepository()).Read(12);
            // var repo = new ExerciseSessionRepository();

            //var session = new ExerciseSession
            //{
            //    Created = DateTime.Now,
            //    Modified = DateTime.Now,
            //    Start = DateTime.Now,
            //    Stop = DateTime.Now,
            //    User = null,
            //    ExerciseSets = new List<ExerciseSet>
            //    {
            //        new ExerciseSet
            //        {
            //            Created = DateTime.Now,
            //            Exercises = new List<Exercise>
            //            {
            //                new Exercise
            //                {
            //                    Performed = DateTime.Now,
            //                    Metric = new WeightMetric
            //                    {
            //                        Number = 12,
            //                        WeightKG = 40
            //                    }
            //                }
            //            },
            //            Start = DateTime.Now,
            //            Stop = DateTime.Now,
            //            Modified = DateTime.Now,
            //            ExerciseType = new ExerciseType
            //            {
            //                Created = DateTime.Now,
            //                CreatedBy = null,
            //                ExerciseName = "Overhead Press",
            //                MetricType = Domain.Entities.MetricType.WEIGHT,
            //                Modified = DateTime.Now
            //            },
            //        }
            //    },
            //};


            //repo.Create(session);
            //repo.Save();

            //var se = repo.Read(3);

            //se.ExerciseSets.Add(new ExerciseSet
            //{
            //    Created = DateTime.Now,
            //    //ExerciseType = new ExerciseType
            //    //{
            //    //    Created = DateTime.Now,
            //    //    CreatedBy = null,
            //    //    ExerciseName = "Squats",
            //    //    MetricType = Domain.Entities.MetricType.WEIGHT,
            //    //    Modified = DateTime.Now
            //    //},
            //    ExerciseTypeId = ex.ExerciseTypeId,
            //    Modified = DateTime.Now,
            //    Start = DateTime.Now,
            //    Stop = DateTime.Now,
            //    Exercises = new List<Exercise>
            //    {
            //        new Exercise
            //        {
            //            Performed = DateTime.Now,
            //            Metric = new WeightMetric
            //            {
            //                Number = 16,
            //                WeightKG = 80
            //            }
            //        },
            //        new Exercise
            //        {
            //            Performed = DateTime.Now,
            //            Metric = new WeightMetric
            //            {
            //                Number = 16,
            //                WeightKG = 80
            //            }
            //        },
            //    }
            //});

            //repo.Update(se);
            //repo.Save();

            // var se2 = repo.Read(3);

            var concreteRepo = new ExerciseSessionRepository("Console");
            
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
                        ExerciseType = new ExerciseType
                        {
                            Created = DateTime.Now,
                            CreatedBy = null,
                            ExerciseName = "Overhead Press New",
                            MetricType = Domain.Entities.MetricType.WEIGHT,
                            Modified = DateTime.Now
                        },
                    }
                },
            };
            concreteRepo.Create(session);
            concreteRepo.Save();
            // var session = concreteRepo.Read(3);

            Console.Write("Hello");
            Console.Read();
        }
    }
}
