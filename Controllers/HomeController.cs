using System.Web.Mvc;
using Website.Models;

namespace Website.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Duration = 0)]
        public ActionResult List()
        {
            var manager = new NoteManager();
            var model = manager.GetAll();
            return PartialView(model);
        }

        [OutputCache(Duration = 0)]
        public ActionResult Create()
        {
            var model = new Note();
            return PartialView("NoteForm", model);
        }

        [OutputCache(Duration = 0)]
        public ActionResult Edit(int id)
        {
            var manager = new NoteManager();
            var model = manager.GetById(id);
            return PartialView("NoteForm", model);
        }

        [HttpPost]
        public ActionResult Save(Note note)
        {
            var manager = new NoteManager();
            manager.Save(note);
            var model = manager.GetAll();
            return PartialView("List", model);
        }
    }
}
