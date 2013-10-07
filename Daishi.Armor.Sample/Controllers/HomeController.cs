#region Includes

using System.Web.Mvc;

#endregion

namespace Daishi.Armor.Sample.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
    }
}