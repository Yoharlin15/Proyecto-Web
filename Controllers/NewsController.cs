using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Data;
using Proyecto_Web.ViewModels.Countries;
using Proyecto_Web.ViewModels;
using Proyecto_Web.ViewModels.News;
using Proyecto_Web.Models;
using Proyecto_Web.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Proyecto_Web.Constants;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
namespace Proyecto_Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly NewsContext _newsContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public NewsController(NewsContext newsContext, IWebHostEnvironment webHostEnvironment)
        {
            this._newsContext = newsContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        public ActionResult Index(int page = 1, int pageSize = 1, string search = null)
        {
            var viewModel = new NewsListViewModel { Search = search };
            var query = _newsContext.News.AsQueryable();
            int total = 0;
            var pageInfo = Pagination.GetInfo(page, pageSize);

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(RoleNames.Editor))
                {

                    var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);


                    if (int.TryParse(userIdString, out int userId))
                    {
                        query = query.Where(n => n.UserId == userId);
                    }
                }
            }
            total = query.Count();

            viewModel.News = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();

            viewModel.Pagination = new Pagination
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
            };

            // Devuelve la vista con el viewModel
            return View(viewModel);
        }
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: NewsController/Create
        public ActionResult Create()
        {
            return View(new CreateNewsViewModel());
        }

        private string UploadFile(IFormFile pictureFile)
        {
            var pictureFileName = $"{Guid.NewGuid()}{Path.GetExtension(pictureFile.FileName)}";
            var uploadPath = Path.Combine(
            _webHostEnvironment.ContentRootPath
            , "Uploads", "News",
            pictureFileName
            );
            using var fileStream = System.IO.File.OpenWrite(uploadPath);
            pictureFile.CopyTo(fileStream);

            return pictureFileName;
        }
        private byte[] GetUploadBytes(IFormFile pictureFile)
        {
            using var memoryStream = new MemoryStream();
            pictureFile.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
        public IActionResult picturePath(int id)
        {
            var Country = _newsContext.News.Find(id);

            var uploadPath = Path.Combine(
                   _webHostEnvironment.ContentRootPath,
                   "Uploads", "News",
                   Country.PicturePath
                   );

            var fileString = System.IO.File.OpenRead(uploadPath);
            return File(fileString, "image/jpg");
        }
        public IActionResult pictureBytes(int id)
        {
            var country = _newsContext.News.Find(id);
            return File(country.Picture, "image/jpg");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateNewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var picturePath = UploadFile(viewModel.PictureFile);

                    byte[] pictureBytes = GetUploadBytes(viewModel.PictureFile);

                    var news = new YohaNews();
                    news.Title = viewModel.Name;
                    news.PicturePath = picturePath;
                    news.Picture = pictureBytes;
                    news.Description = viewModel.Description;
                    news.PublicationDate = viewModel.PubishedDate;
                    news.Author = viewModel.Author;
                    news.UserId = viewModel.UserId;

                    _newsContext.News.Add(news);
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

        // GET: NewsController/Edit/5
        public ActionResult Edit(int id)
        {

            var News = _newsContext.News.Find(id);
            var viewModel = new UpdateNewsViewModel();
            viewModel.Title = News.Title;
            viewModel.PicturePath = News.PicturePath;
            viewModel.Description = News.Description;
            viewModel.PubishedDate = News.PublicationDate.Value;
            viewModel.Author = News.Author;

            return View(viewModel);
        }

        // POST: NewsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateNewsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var picturePath = UploadFile(viewModel.PictureFile);
                    byte[] pictureBytes = GetUploadBytes(viewModel.PictureFile);

                    var country = _newsContext.News.Find(id);
                    country.Title = viewModel.Title;
                    country.PicturePath = picturePath;
                    country.Picture = pictureBytes;
                    country.Description = viewModel.Description;
                    country.Author = viewModel.Author;
                    country.PublicationDate = viewModel.PubishedDate;

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
        public ActionResult Delete(int id)
        {
            var Country = _newsContext.News.Find(id);
            var viewModel = new DeleteNewsViewModel();
            viewModel.Title = Country.Title;
            viewModel.Author = Country.Author;
            viewModel.Description = Country.Description;
            viewModel.PubishedDate = Country.PublicationDate;

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection _)
        {
            var country = _newsContext.News.Find(id);
            var viewModel = new DeleteNewsViewModel();
            viewModel.Title = country.Title;
            viewModel.Author = country.Author;
            viewModel.Description = country.Description;
            viewModel.PubishedDate = country.PublicationDate;

            try
            {
                _newsContext.News.Remove(_newsContext.News.Find(id));
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
