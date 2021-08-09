using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CRUDOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Требуется дата создания заказа")]
        public DateTime Order_Date { get; set; }

        public DateTime? Shipment_Date { get; set; }

        [Required(ErrorMessage = "Требуется номер заказа")]
        public int Order_Number { get; set; }

        [Required(ErrorMessage = "Требуется указать заказчика")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Требуется указать статус")]
        public int StatusId { get; set; }

        /// <summary>
        /// Customer
        /// </summary>
        
        public string UserName { get; set; }

        /// <summary>
        /// Status
        /// </summary>

        public string StatusName { get; set; }
      
    }
}
