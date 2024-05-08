using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Domain
{
    public class TestsDomain
    {

        public Product Product { get; set; }
        public ProductDto ProductDto { get; set; }

        public void EntityTest()
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
            Product = product;

            string message = $"Produto!\nCódigo: {product.Code}\nNome:{product.Name}\nDescrição:{product.Description}\nPreço:{product.SaleValue}\nFornecedor:{product.Supplier}\nCategoria:{product.Category}\nData de validade:{product.ExpirationDate}\nValor de compra:{product.Value}";
            Console.WriteLine(message);
        }

        public void DtoTest()
        {
            ProductDto product = new ProductDto(1,
                "description",
                180.5,
                "água sanitária",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                );

            ProductDto = product;

            string message = $"ProdutoDto!\nCódigo: {product.Code}\nNome:{product.Name}\nDescrição:{product.Description}\nPreço:{product.SaleValue}\nFornecedor:{product.Supplier}\nCategoria:{product.Category}\nData de validade:{product.ExpirationDate}\nValor de compra:{product.Value}";
            Console.WriteLine(message);

        }

        public void ConversionDtoTest()
        {
            Console.WriteLine("To DTO!!!");
            Console.WriteLine(Product.ToDto().GetType());
        }

        public void ConversionEntityTest()
        {
            Console.WriteLine("TO ENTITY!!!");
            Console.WriteLine(ProductDto.ToEntity().GetType());
        }

    }
}
