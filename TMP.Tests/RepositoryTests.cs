
namespace TMP.Tests
{

    using System;
    using System.Text;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using DAL.Repositories;
    using Domain.Entities.Exercises;
    using System.Linq;


    /// <summary>
    /// Summary description for RepositoryTests
    /// </summary>
    [TestClass]
    public class RepositoryTests
    {

        #region Auto generated stuff

        public RepositoryTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        #endregion

        [TestMethod]
        public void CanCreateNewExerciseType()
        {
            using (var repository = new ExerciseTypeRepository())
            {
                var allBeforeItems = repository.FindAll();
                var beforeCount = allBeforeItems.Count();

                var exerciseType = new ExerciseType
                {
                    Created = DateTime.Now,
                    CreatedBy = null,
                    ExerciseName = "CanCreateNewExerciseType",
                    MetricType = Domain.Entities.MetricType.WEIGHT,
                    Modified = DateTime.Now
                };

                repository.Create(exerciseType);
                repository.Save();

                var allAfterItems = repository.FindAll();
                var afterCount = allAfterItems.Count();
                Assert.IsTrue(beforeCount == (afterCount - 1));

                var item = allAfterItems.Last();
                repository.Delete(item);
                repository.Save();

                allAfterItems = repository.FindAll();
                afterCount = allAfterItems.Count();
                Assert.IsTrue(beforeCount == (afterCount));
            }
        }
    }
}
