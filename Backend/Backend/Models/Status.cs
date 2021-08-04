
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Status
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Требуется имя")]
        public string Name { get; set; }

        public List<Order> Orders { get; set; }

        public Status()
        {
            Orders = new List<Order>();
        }

    }
}
