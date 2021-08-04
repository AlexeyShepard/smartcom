
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Требуется имя")]
        public string Name { get; set; }

        public List<Product> Products { get; set; }

        public Category()
        {
            Products = new List<Product>();
        }
    }
}
