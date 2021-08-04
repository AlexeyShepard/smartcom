
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required (ErrorMessage = "Не указана роль")]
        public int RoleId { get; set; }

        public Role Role { get; set; }
        
        //public Customer Customer { get; set; }
    }
}
