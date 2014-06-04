using System.Web.Mvc;
using FrontEnd;

namespace Core.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/

        [HttpGet]
        public JsonResult GetSignInArea()
        {
            return Json(Common.RenderViewToString(this, "_SignInPartial", null), JsonRequestBehavior.AllowGet);
        }

    }
}
