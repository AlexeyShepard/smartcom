using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CRUDCustomer
    {       
     
        public int Id { get; set; }
        
        public string Code { get; set; }

        [Required(ErrorMessage = "Требуется адресс заказчика")]
        public string Adress { get; set; }

        public float? Discount { get; set; }

        [Required(ErrorMessage = "Требуется идентификатор пользователя")]
        public int UserId { get; set; }

        /// <summary>
        /// User
        /// </summary>

        [Required(ErrorMessage = "Требуется имя заказчика")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана почта")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        public string Password { get; set; }
    }
}
