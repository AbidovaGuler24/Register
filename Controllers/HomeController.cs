using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context {  get; set; } 

        public HomeController(AppDbContext context)
        {
            _context = context;
        }



        public async  Task<IActionResult> Index()

        {
            List<Product > products =await _context.Products.Include(x=>x.Images).ToListAsync();
            return View(products);
        }
    }
}
