using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Data;
using Proyecto_Web.ViewModels.Categories;
using Proyecto_Web.ViewModels;
using Proyecto_Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Constants;
using Proyecto_Web.Models;

namespace Proyecto_Web.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]

    public class UsersController : Controller
    {
        private readonly NewsContext _newsContext;

        public UsersController(NewsContext newsContext)
        {
            this._newsContext = newsContext;
        }

		public ActionResult Index(int page = 1, int pageSize = 5, string search = null)
		{
			var viewModel = new UsersListViewModel {Search = search };
			var query = _newsContext.Users.Include(u => u.Role).AsQueryable();														

			var pageInfo = Pagination.GetInfo(page, pageSize);

			int total = 0;

			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(a => EF.Functions.Like(a.FirstName, $"%{search}%"));

				total = query.Count();

				viewModel.User = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();

			}
			else
			{
				total = query.Count();
				viewModel.User = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();
			}

			viewModel.Pagination = new Pagination
			{
				Page = page,
				PageSize = pageSize,
				Total = total
			};

			return View(viewModel);
		}

		// GET: UsersController/Details/5
		public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UsersController/Create
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

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
			var user= _newsContext.Users.Find(id);
			var viewModel = new UpdateUsersViewModel();

			viewModel.UserId = user.UserId;
			viewModel.FirtName = user.FirstName;
			viewModel.LastName = user.LastName;
            viewModel.Email = user.Email;
            viewModel.Password = user.Password;
			user.RoleId = _newsContext.Roles.FirstOrDefault(r => r.RoleName == viewModel.Role)?.RoleD;

			return View(viewModel);
		}

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateUsersViewModel viewModel)
        {
			if (ModelState.IsValid)
			{
				try
				{
					var user = _newsContext.Users.Find(id);
					user.FirstName = viewModel.FirtName;
					user.LastName = viewModel.LastName;
                    user.Email = viewModel.Email;
                    user.Password = viewModel.Password;
					user.RoleId = _newsContext.Roles.FirstOrDefault(r => r.RoleName == viewModel.Role)?.RoleD;
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

		// GET: UsersController/Delete/5
		public ActionResult Delete(int id)
		{
			var User = _newsContext.Users.Find(id);
			var viewModel = new DeleteUserViewModel();
            viewModel.UserId = User.UserId;
            viewModel.FirstName = User.FirstName;
            viewModel.LastName = User.LastName;
            viewModel.Email = User.Email;
            viewModel.Password = User.Password;
            User.RoleId = _newsContext.Roles.FirstOrDefault(r => r.RoleName == viewModel.Role)?.RoleD;


            return View(viewModel);
		}

		// POST: UsersController/Delete/5
		[HttpPost]
        [ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection _)
		{
			var User = _newsContext.Users.Find(id);
			var viewModel = new DeleteUserViewModel();
			viewModel.UserId = User.UserId;
			viewModel.FirstName = User.FirstName;
			viewModel.LastName = User.LastName;
			viewModel.Email = User.Email;
			viewModel.Password = User.Password;
            User.RoleId = _newsContext.Roles.FirstOrDefault(r => r.RoleName == viewModel.Role)?.RoleD;


            try
            {
				_newsContext.Users.Remove(_newsContext.Users.Find(id));
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
