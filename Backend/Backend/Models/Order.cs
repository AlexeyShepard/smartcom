
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется дата создания заказа")]
        public DateTime Order_Date { get; set; }

        public DateTime? Shipment_Date { get; set; }

        [Required(ErrorMessage = "Требуется номер заказа")]
        public int Order_Number { get; set; }

        [Required(ErrorMessage = "Требуется указать заказчика")]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        
        [Required(ErrorMessage = "Требуется указать статус")]
        public int StatusId { get; set; }

        public Status Status { get; set; }

        public List<OrderElement> OrderElements { get; set; }
        
        public Order()
        {
            OrderElements = new List<OrderElement>(); 
        }
    }
}
