using System.Web.Mvc;
using NorthwindData;

namespace TabTest.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/Add

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                // add a new model here

                RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
