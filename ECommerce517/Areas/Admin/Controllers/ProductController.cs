using ECommerce517.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ECommerce517.Areas.Admin.Controllers
{
    [Area(SD.AdminArea)]
    public class ProductController : Controller
    {
        ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var products = _context.Products.Include(e => e.Category)/*.OrderBy(e=>e.Quantity)*/;

            return View(products.ToList());
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categories = _context.Categories;
            var brands = _context.Brands;

            CategoryWithBrandVM categoryWithBrandVM = new()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList()
            };

            return View(categoryWithBrandVM);
        }

        [HttpPost]
        public IActionResult Create(Product product, IFormFile MainImg)
        {
            if (MainImg is not null && MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName); 
                // 0924fdsfs-d429-fskdf-jsd230-423.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                // Save Img in wwwroot
                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }

                // Sava img name in DB
                product.MainImg = fileName;

                // Save in DB
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id);

            if (product is null)
                return RedirectToAction(SD.NotFoundPage, SD.HomeController);

            var categories = _context.Categories;
            var brands = _context.Brands;

            CategoryWithBrandVM categoryWithBrandVM = new()
            {
                Categories = categories.ToList(),
                Brands = brands.ToList(),
                Product = product
            };

            return View(categoryWithBrandVM);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile? MainImg)
        {
            var productInDB = _context.Products.AsNoTracking().FirstOrDefault(e => e.Id == product.Id);

            if (productInDB is null)
                return BadRequest();

            if(MainImg is not null && MainImg.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(MainImg.FileName);
                // 0924fdsfs-d429-fskdf-jsd230-423.png

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", fileName);

                // Save Img in wwwroot
                using (var stream = System.IO.File.Create(filePath))
                {
                    MainImg.CopyTo(stream);
                }

                // Delete old img from wwwroot
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", productInDB.MainImg);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                // Update img name in DB
                product.MainImg = fileName;
            }
            else
            {
                product.MainImg = productInDB.MainImg;
            }

            // Update in DB
            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
