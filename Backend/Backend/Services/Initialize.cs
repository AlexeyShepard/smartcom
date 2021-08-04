
using Backend.Models;
using System.Linq;

namespace Backend.Services
{
    public class Initialize
    {
        public static void Run(SmartcomContext context)
        {
            if (!context.Roles.Any() && !context.Statuses.Any() && !context.Categories.Any())
            {
                context.AddRange(
                    new Role
                    {
                        Name = "Заказчик"
                    },
                    new Role
                    {
                        Name = "Менеджер"
                    });

                context.AddRange(
                    new Status
                    {
                        Name = "Новый"
                    },
                    new Status
                    {
                        Name = "Выполняется"
                    },
                    new Status
                    {
                        Name = "Выполнен"
                    });
                context.AddRange(
                    new Category
                    {
                        Name = "Молочные"
                    },
                    new Category
                    {
                        Name = "Мясо"
                    },
                    new Category
                    {
                        Name = "Крупы"
                    },
                    new Category
                    {
                        Name = "Выпечка"
                    });
                context.SaveChanges();
            }
        }
    }
}
