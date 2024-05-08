using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Helpers
{
    public static class ProductDtoHelper
    {
        public static ProductDto GetProductDto()
        {
            ProductDto product = new ProductDto(1,
               "description",
               180.5,
               "água sanitária",
               "loja da esquina",
               200.0,
               "limpeza",
               "2025-2-1"
               );
            return product;
        }
    }
}
