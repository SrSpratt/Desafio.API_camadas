using Desafio.Domain.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public CategoryDTO() { }
        public CategoryDTO(CategoryDAO category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }

        public static Dictionary<string, string> DAOMap = new Dictionary<string, string>
        {
            {"category_id", nameof(Id) },
            {"category_name", nameof(Name) },
            {"category_description", nameof(Description) }
        };

        public static Dictionary<string, string> DBMap = new Dictionary<string, string>
        {
            {nameof(Id), "category_id"},
            {nameof(Name), "category_name"},
            {nameof(Description), "category_description"}
        };

    }
}
