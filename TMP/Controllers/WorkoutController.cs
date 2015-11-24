namespace TMP.Controllers
{
    using System.Web.Mvc;
    using Domain.Entities.Exercises;
    using Common;

    public class WorkoutController : Controller
    {
        // GET: Workout
        public ActionResult Index()
        {
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
    }
}