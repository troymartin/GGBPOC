
using System.Web.Mvc;
using GGBDemo.ActionResults;

namespace GGBDemo.Controllers
{
    public class BaseController : Controller
    {
        protected JsonAttributesResult JsonWithAttribute(object model)
        {
            return new JsonAttributesResult(model);
        }
    }
}