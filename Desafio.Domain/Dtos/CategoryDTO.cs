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

        public CategoryDTO(CategoryDAO category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }

    }
}
