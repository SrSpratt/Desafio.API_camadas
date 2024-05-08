using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Helpers
{
    public static class ProductHelper
    {
        public static Product GetProduct()
        {
            Product product = new Product(1,
                "description",
                180.5,
                "água sanitária",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                );
            return product;
        }

        public static Product GetNewProduct()
        {
            Product product = new Product(1,
                "description",
                180.5,
                "água sanitária",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                );
            return product;
        }
    }
}
