
namespace TMP.Controllers
{
    using System.Web.Mvc;
    using Attributes;
    using Domain.Entities.Exercises;
    using BLL.Services.Exercise;
    using Domain.Exceptions.Exercise;

    public class ExerciseTypesController : Controller
    {
        // GET: ExerciseTypes
        public ActionResult Index()
        {
            var service = new ExerciseTypeService(Common.Constants.Database.DBKey);
            var exerciseTypes = service.GetAll();
            return View(exerciseTypes);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAJAXRequest]
        public ActionResult Add(ExerciseType exerciseType)
        {
            if(exerciseType != null)
            {
                var service = new ExerciseTypeService(Common.Constants.Database.DBKey);
                try
                {
                    service.AddNewExercise(exerciseType);
                }
                catch(ExerciseTypeAlreadyExists)
                {
                    return new HttpStatusCodeResult(400, "Exercise type already exists");
                }                
                return new HttpStatusCodeResult(200);
            }                
            return new HttpStatusCodeResult(500);
        }
    }
}