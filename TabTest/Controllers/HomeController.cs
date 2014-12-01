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
                Tab1Model = MakeTab1Model(),
                Tab2Model = new Tab2Model()
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
                    Model = model.Tab1Model,
                    TabHeader = "Tab 1",
                    TemplatePath = "_tab1"
                },
                new TabListItem
                {
                    IsPartial = true,
                    Model = model.Tab2Model,
                    TabHeader = "Tab 2",
                    TemplatePath = "_tab2"
                }
            };
        }

        private static Tab1Model MakeTab1Model()
        {
            var tab1Model = new Tab1Model();

            tab1Model.Departments = new List<Department>
            {
                new Department { Name = "Human Resources", Code = "HR"},
                new Department { Name = "Executive", Code = "EXC"},
                new Department { Name = "Engineering", Code = "ENG"},
            };

            tab1Model.Items = tab1Model.Departments
                                .MakeSelectList(
                                    t => t.Code,
                                    t => string.Format("{0} - {1}", t.Code, t.Name), 
                                    t => false);
                
            tab1Model.SomeText = "Text Here";

            return tab1Model;
        }

        [HttpPost]
        public ActionResult Home(Tab1Model model)
        {
            return RedirectToAction("Index");
        }

    }
}
