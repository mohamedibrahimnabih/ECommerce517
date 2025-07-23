using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ECommerce517.Models;
using ECommerce517.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ECommerce517.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ApplicationDbContext _context = new();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        var products = _context.Products.Include(e => e.Category);

        //

        return View(products.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public ViewResult Welcome()
    {
        return View();
    }

    public ViewResult PersonalInfo()
    {
        string name = "Mohamed";
        int age = 27;
        double balance = 2000;

        List<string> skills = new()
        {
            "C",
            "C++",
            "C#"
        };

        PersonalInfoVM personalInfoVM = new PersonalInfoVM()
        {
            Name = name,
            Age = age,
            Balance = balance,
            Skills = skills
        };

        return View(model: personalInfoVM);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
