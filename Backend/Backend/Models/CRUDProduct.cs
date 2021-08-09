using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class CRUDProduct
    {
        public int Id { get; set; }

        public string Code { get; set; }

        [Required(ErrorMessage = "Требуется имя продукта")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Требуется цена продукта")]
        public float Price { get; set; }

        [Required(ErrorMessage = "Требуется категория продукта")]
        public int CategoryId { get; set; }

        /// <summary>
        /// Category
        /// </summary>

        [Required(ErrorMessage = "Требуется наименование категории продукта")]
        public string CategoryName { get; set; }
    }
}
