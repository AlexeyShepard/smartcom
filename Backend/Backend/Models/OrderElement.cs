
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class OrderElement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется указать кол-во продуктов")]
        public int Items_Count { get; set; }

        [Required(ErrorMessage = "Требуется указать цену продукта")]
        public float Item_Price { get; set; }

        [Required(ErrorMessage = "Требуется указать заказ")]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Required(ErrorMessage = "Требуется указать продукт")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
