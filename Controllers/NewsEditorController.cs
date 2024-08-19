using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Web.Constants;
using Proyecto_Web.Data;
using Proyecto_Web.Models;

namespace Proyecto_Web.Controllers
{
    [Authorize(Roles = RoleNames.Editor)]
    public class NewsEditorController : Controller
    {
        private readonly NewsContext _newsContext;

        public NewsEditorController(NewsContext newsContext) 
        {
            this._newsContext = newsContext;
        }
        // GET: NewsEditorController
        public ActionResult Index()
        {
            return View();
        }

        // GET: NewsEditorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsEditorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NewsEditorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsEditorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: NewsEditorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: NewsEditorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NewsEditorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
