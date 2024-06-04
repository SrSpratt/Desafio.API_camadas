using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class CategoryDAO
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public CategoryDAO(string name, string description) {
            Name = name;
            Description = description;
        }
    }
}
