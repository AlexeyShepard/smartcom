using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Требуется имя")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Требуется Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required(ErrorMessage = "Требуется Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Требуется пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
