using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class ProductDao
    {
        public int Code { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ProductDao(int code, string name, string description) {
            Code = code;
            Name = name;
            Description = description;
        }
    }
}
