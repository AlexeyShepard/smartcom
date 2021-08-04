
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется код продукта")]
        [RegularExpression(@"\d{2}-\d{4}-[A-Z]{2}\d{2}", ErrorMessage = "Неверный формат кода продукта")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Требуется имя продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Требуется цена продукта")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Требуется категория продукта")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<OrderElement> OrderElements { get; set; }

        public Product()
        {
            OrderElements = new List<OrderElement>();
        }
    }
}
