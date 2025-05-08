using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pronia.Models;

namespace Pronia.DAL
{
    public class AppDbContext: IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions):base(contextOptions) { }
       
        public DbSet<Product > Products { get; set; }

        public DbSet<Catagory> Catagories { get; set; } 

        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<Tag> Tags { get; set; }    

        public DbSet<TagProducts> TagsProducts { get; set; }

    }
   
}
