using Desafio.Domain.Dtos;
using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Testes.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace Testes.Domain
{
    public class TestsDomain
    {

        public Product Product { get; set; }
        public ProductDto ProductDto { get; set; }

        public TestsDomain()
        {

        }

        public void Execute()
        {
            EntityTest();
            Console.WriteLine();
            DtoTest();
            Console.WriteLine("\nNow, conversions:");
            ConversionEntityTest();
            ConversionDtoTest();

            Console.WriteLine();
        }
        public void EntityTest()
        {
            Product product = ProductHelper.GetProduct();

            string message = $"Produto!\nCódigo: {product.Code}\nNome:{product.Name}\nDescrição:{product.Description}\nPreço:{product.SaleValue}\nFornecedor:{product.Supplier}\nCategoria:{product.Category}\nData de validade:{product.ExpirationDate}\nValor de compra:{product.Value}";
            Console.WriteLine(message);
        }

        public void DtoTest()
        {
            ProductDto product = ProductDtoHelper.GetProductDto();

            string message = $"ProdutoDto!\nCódigo: {product.Code}\nNome:{product.Name}\nDescrição:{product.Description}\nPreço:{product.SaleValue}\nFornecedor:{product.Supplier}\nCategoria:{product.Category}\nData de validade:{product.ExpirationDate}\nValor de compra:{product.Value}";
            Console.WriteLine(message);

        }

        public void ConversionDtoTest()
        {
            Console.WriteLine("To DTO!!!");
            Console.WriteLine(ProductHelper.GetProduct().ToDto().GetType());
        }

        public void ConversionEntityTest()
        {
            Console.WriteLine("TO ENTITY!!!");
            Console.WriteLine(ProductDtoHelper.GetProductDto().ToEntity().GetType());
        }

    }
}
