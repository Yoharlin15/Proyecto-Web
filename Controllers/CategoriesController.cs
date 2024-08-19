using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Constants;
using Proyecto_Web.Data;
using Proyecto_Web.Models;
using Proyecto_Web.ViewModels;
using Proyecto_Web.ViewModels.Categories;

namespace Proyecto_Web.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class CategoriesController : Controller
    {
        private readonly NewsContext _newsContext;

        public CategoriesController(NewsContext newsContext)
        {
            this._newsContext = newsContext;
        }
        // GET: CategoryController
        public ActionResult Index(int page = 1, int pageSize = 5, string search = null)
        {
            var viewModel = new CategoryListViewModel { Search = search };
            var query = _newsContext.Categories.AsQueryable();

            var pageInfo = Pagination.GetInfo(page, pageSize);

            int total = 0;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => EF.Functions.Like(a.CategoryName, $"%{search}%"));

                total = query.Count();

                viewModel.Categories = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();

            }
            else
            {
                total = query.Count();
                viewModel.Categories = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();
            }

            viewModel.Pagination = new Pagination
            {
                Page = page,
                PageSize = pageSize,
                Total = total
            };

            return View(viewModel);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View(new CreateCategoryViewModel());
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = new Category();
                    category.CategoryName = viewModel.CategoryName;
                    category.Description = viewModel.Description;
                    _newsContext.Categories.Add(category);
                    _newsContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    viewModel.Exception = ex;
                    return View(viewModel);
                }

            }

            return View(viewModel);
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {

            var Category = _newsContext.Categories.Find(id);
            var viewModel = new UpdateCategoryViewModel();
            viewModel.CategoryId = Category.CategoryId;
            viewModel.CategoryName = Category.CategoryName;
            viewModel.Description = Category.Description;

            return View(viewModel);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateCategoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var category = _newsContext.Categories.Find(id);
                    category.CategoryName = viewModel.CategoryName;
                    category.Description = viewModel.Description;
                    _newsContext.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    viewModel.Exception = ex;
                    return View(viewModel);
                }
            }
            return View(viewModel);
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var Category = _newsContext.Categories.Find(id);
            var viewModel = new DeleteCategoryViewModel();
            viewModel.CategoryId = Category.CategoryId;
            viewModel.CategoryName = Category.CategoryName;
            viewModel.Description = Category.Description;
            return View(viewModel);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection _)
        {
            var Category = _newsContext.Categories.Find(id);
            var viewModel = new DeleteCategoryViewModel();
            viewModel.CategoryId = Category.CategoryId;
            viewModel.CategoryName = Category.CategoryName;
            viewModel.Description = Category.Description;

            try
            {
                _newsContext.Categories.Remove(_newsContext.Categories.Find(id));
                _newsContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                viewModel.Exception = ex;
                return View(viewModel);
            }
        }
    }
}
