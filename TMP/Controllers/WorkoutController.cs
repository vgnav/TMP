namespace TMP.Controllers
{
    using System.Web.Mvc;
    using Domain.Entities.Exercises;
    using Common;
    using BLL.Services.Exercise;
    using Newtonsoft.Json;

    public class WorkoutController : Controller
    {
        // GET: Workout
        public ActionResult Index()
        {
            var service = new ExerciseTypeService(Constants.Database.DBKey);
            ViewBag.AllWorkoutTypes = service.GetAll();
            return View();
        }

        [HttpGet]
        public ActionResult GetWorkoutType(string exerciseName)
        {
            var exercise = new ExerciseType {
                ExerciseName = exerciseName,
                MetricType = Domain.Entities.MetricType.DISTANCE,
                ExerciseTypeId = 1
            };
            return new JsonActionResult(exercise, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllWorkouts()
        {
            var service = new ExerciseTypeService(Constants.Database.DBKey);
            return new JsonActionResult(service.GetAll(), JsonRequestBehavior.AllowGet);
        }
    }
}