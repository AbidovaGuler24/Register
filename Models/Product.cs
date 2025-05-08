using Pronia.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace Pronia.Models
{
    public class Product:BaseEntity
    {
      public string Name { get; set; }
        [Required,MaxLength(25,ErrorMessage=" Max uzunluq 25 ola biler ")]
        public string Description { get; set; }

        public double  Price  { get; set; }

       public List<ProductImage> Images { get; set; }

        public int? CatagoryId { get; set; }

        public Catagory? Catagory { get; set; }  
        
        public List<TagProducts>? TagProducts { get; set; }

    }
}
