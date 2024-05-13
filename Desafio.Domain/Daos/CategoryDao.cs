using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class CategoryDao
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryDao(string name, string description) {
            Name = name;
            Description = description;
        }
    }
}
