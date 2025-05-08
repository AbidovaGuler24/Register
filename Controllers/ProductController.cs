using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DAL;
using Pronia.Models;

namespace Pronia.Controllers
{
    public class ProductController : Controller
    {
         AppDbContext _context {  get; set; }   

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        

        public IActionResult Index()
        {
            var products = _context.Products.Include(x=>x.Catagory).Include(x=>x.TagProducts).ThenInclude(tg=>tg.Tag).ToList();
            return View(products);
        }
    }
}
