using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_Web.Constants;
using Proyecto_Web.Data;
using Proyecto_Web.Models;
using Proyecto_Web.ViewModels;
using Proyecto_Web.ViewModels.Categories;
using Proyecto_Web.ViewModels.Countries;

namespace Proyecto_Web.Controllers
{
    [Authorize(Roles = RoleNames.Administrator)]
    public class CountriesController : Controller
	{
		private readonly NewsContext _newsContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CountriesController(NewsContext newsContext, IWebHostEnvironment webHostEnvironment) 
		{
			this._newsContext = newsContext;
            this._webHostEnvironment = webHostEnvironment;
        }

        

        public ActionResult Index(int page = 1, int pageSize = 5, string search = null)
        {
            var viewModel = new CountryListViewModel { Search = search };
            var query = _newsContext.Countries.AsQueryable();
            int total = 0;
            var pageInfo = Pagination.GetInfo(page, pageSize);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(a => EF.Functions.Like(a.CountryName, $"%{search}%"));

                total = query.Count();

                viewModel.Countries = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();

            }
            else
            {
                total = query.Count();
                viewModel.Countries = query.Skip(pageInfo.Skip).Take(pageInfo.Take).ToList();
            }

			viewModel.Pagination = new Pagination
            {
                Page = page,
                PageSize = pageSize,
                Total = total,
            };

            return View(viewModel);
        }

        // GET: CountriesController/Details/5
        public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CountriesController/Create
		public ActionResult Create()
		{
			return View(new CreateCountryViewModel());
		}

        private string UploadFile(IFormFile pictureFile)
        {
			var pictureFileName = $"{Guid.NewGuid()}{Path.GetExtension(pictureFile.FileName)}";
			var uploadPath = Path.Combine(
			_webHostEnvironment.ContentRootPath
			, "Uploads", "Countries",
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
			var Country = _newsContext.Countries.Find(id);

			var uploadPath = Path.Combine(
				   _webHostEnvironment.ContentRootPath,
				   "Uploads", "Countries",
				   Country.PicturePath
				   );

			var fileString = System.IO.File.OpenRead(uploadPath);
			return File(fileString, "image/jpg");
		}
        public IActionResult pictureBytes(int id)
        {
            var country = _newsContext.Countries.Find(id);
            return File(country.PictureFlag, "image/jpg");
        }
                
		// POST: CountriesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateCountryViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
                try
                {
                    var picturePath = UploadFile(viewModel.PictureFile);

                    byte[] pictureBytes = GetUploadBytes(viewModel.PictureFile);

                    var country = new Country();
					country.CountryName = viewModel.CountryName;
					country.Capital = viewModel.Capital;
					country.Population = viewModel.Population;
					country.Currency = viewModel.Currency;
					country.Continent = viewModel.Continent;
					country.Language = viewModel.Language;
                    country.PicturePath = picturePath;
                    country.PictureFlag = pictureBytes;

					_newsContext.Countries.Add(country);
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
        // GET: CountriesController/Edit/5
        public ActionResult Edit(int id)
		{
            var Country = _newsContext.Countries.Find(id);
            var viewModel = new UpdateCountryViewModel();
            viewModel.CountryName = Country.CountryName;
			viewModel.Capital = Country.Capital;
			viewModel.Population = Country.Population;	
			viewModel.Currency = Country.Currency;
			viewModel.Continent = Country.Continent;
			viewModel.Language = Country.Language;
            viewModel.PicturePath = Country.PicturePath;
            return View(viewModel);
        }

		// POST: CountriesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UpdateCountryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var picturePath = UploadFile(viewModel.PictureFile);
                    byte[] pictureBytes = GetUploadBytes(viewModel.PictureFile);

					var country = _newsContext.Countries.Find(id);
                    country.CountryName = viewModel.CountryName;
                    country.Capital = viewModel.Capital;
					country.Population = viewModel.Population;
					country.Currency = viewModel.Currency;
					country.Continent = viewModel.Continent;
					country.Language = viewModel.Language;
                    country.PicturePath = picturePath;
                    country.PictureFlag = pictureBytes;

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

        // GET: CountriesController/Delete/5
        public ActionResult Delete(int id)
		{
            var Country = _newsContext.Countries.Find(id);
            var viewModel = new DeleteCountryViewModel();
            viewModel.CountryName = Country.CountryName;
            viewModel.Capital = Country.Capital;
            viewModel.Population = Country.Population;
            viewModel.Currency = Country.Currency;
            viewModel.Continent = Country.Continent;
            viewModel.Language = Country.Language;
            return View(viewModel);
        }

		// POST: CountriesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection _)
        {
            var country = _newsContext.Countries.Find(id);
            var viewModel = new DeleteCountryViewModel();
            viewModel.CountryName = country.CountryName;
            viewModel.Capital = country.Capital;
            viewModel.Population = country.Population;
            viewModel.Currency = country.Currency;
            viewModel.Continent = country.Continent;
            viewModel.Language = country.Language;

            try
            {
                _newsContext.Countries.Remove(_newsContext.Countries.Find(id));
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
