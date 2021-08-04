
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Backend.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Требуется имя")]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}
