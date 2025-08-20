using Microsoft.AspNetCore.Mvc;

namespace ECommerce517.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var categories = _context.Categories;

            return View(categories.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Category());
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(!ModelState.IsValid)
            {
                //var errors = ModelState.Values.SelectMany(e => e.Errors);
                //TempData["error-notification"] = String.Join(", ", errors.Select(e=>e.ErrorMessage));

                return View(category);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Add Category Successfully";
            Response.Cookies.Append("success-notification", "Add Category Successfully", new()
            {
                Secure = true,
                Expires = DateTime.Now.AddDays(1)
            });

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);

            if (category is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if(!ModelState.IsValid)
            {
                return View(category);
            }

            _context.Categories.Update(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Update Category Successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(e => e.Id == id);

            if(category is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            _context.Categories.Remove(category);
            _context.SaveChanges();

            TempData["success-notification"] = "Delete Category Successfully";

            return RedirectToAction(nameof(Index));
        }
    }
}
