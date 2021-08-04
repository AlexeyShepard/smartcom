
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Требуется код заказчика")]
        [RegularExpression(@"\d{4}-\d{4}", ErrorMessage = "Неверный формат кода продукта")]
        public string Code { get; set; }

        [Required (ErrorMessage = "Требуется адресс заказчика")]
        public string Adress { get; set; }

        public float? Discount { get; set; }

        public User User { get; set; }

        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }

    }
}
