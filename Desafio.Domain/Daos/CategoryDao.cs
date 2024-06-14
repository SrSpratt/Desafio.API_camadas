using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class CategoryDAO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? Description { get; set; }

        public CategoryDAO()
        {

        }

        public CategoryDAO(int id, string name, string description) {
            Id = id;
            Name = name;
            Description = description;
        }

        public CategoryDAO(string name, string description) {
            Name = name;
            Description = description;
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
