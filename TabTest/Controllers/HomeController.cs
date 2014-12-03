using System.Collections.Generic;
using System.Web.Mvc;
using KnockoutHelpers;
using TabTest.Helpers;
using TabTest.Models;

namespace TabTest.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var model = new HomeModel
            {
                ProductTabModel = new ProductTabModel(),
                ProductCategoryTabModel = MakeTab2Model()
            };

            model.TabList = GetTabList(model);

            return View(model);
        }

        private static List<TabListItem> GetTabList(HomeModel model)
        {
            return new List<TabListItem>
            {
                new TabListItem
                {
                    IsPartial = true,
                    Model = model.ProductTabModel,
                    TabHeader = "Tab 1",
                    TemplatePath = "_tab1"
                },
                new TabListItem
                {
                    IsPartial = true,
                    Model = model.ProductCategoryTabModel,
                    TabHeader = "Tab 2",
                    TemplatePath = "_tab2"
                }
            };
        }

        private static ProductCategoryTabModel MakeTab2Model()
        {
            var model = new ProductCategoryTabModel
            {
                Departments = new List<Department>
                {
                    new Department {Name = "Human Resources", Code = "HR"},
                    new Department {Name = "Executive", Code = "EXC"},
                    new Department {Name = "Engineering", Code = "ENG"},
                }
            };

            model.Items = model.Departments
                                .MakeSelectList(
                                    t => t.Code,
                                    t => string.Format("{0} - {1}", t.Code, t.Name), 
                                    t => false);
                
            model.SomeText = "Text Here";

            return model;
        }

        [HttpPost]
        public ActionResult Home(ProductTabModel model)
        {
            return RedirectToAction("Index");
        }

    }
}
