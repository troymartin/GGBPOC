using System;
using System.Web.Mvc;


namespace GGBDemo.Controllers
{
    public class FormsController : BaseController
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetModel(string modelName)
        {
            try
            {
                var type = Type.GetType("GGBDemo.Models." + modelName, true);
                var model = Activator.CreateInstance(type);
                return JsonWithAttribute(model);
            }
            catch(Exception)
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult PostModel(object model)
        {
          //read model and save and return result
            return null;
        }
    }
}