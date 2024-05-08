using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;
using Desafio.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testes.Services
{
    public class TestProductService
    {
        private readonly IService _service;

        public TestProductService(IService service)
        {
            _service = service;
        }

        public void Execute()
        {
            try
            {
                ValidateProductsListing();
                ValidateProductSearch();
                ValidateProductRegister();
                ValidateProductUpdate();
                ValidateProductRemoval();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        private void ValidateProductsListing()
        {
            var products = _service.ReadAll();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        private void ValidateProductSearch()
        {
            var product = _service.Read(1);
            Console.WriteLine(product);
        }

        private void ValidateProductRegister()
        {
            int id = 35;
            Product product = new Product(id,
                "description",
                180.5,
                "água sanitária",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                );
            _service.Create(product);

            Console.WriteLine(_service.Read(id));
        }

        private void ValidateProductUpdate()
        {
            int id = 1;
            Product product = _service.Read(id);
            product.Name = "NEWNAME";
            _service.Update(id, product);

            Console.WriteLine(_service.Read(id));
        }

        private void ValidateProductRemoval()
        {
            int id = 1;
            _service.Delete(id);

            string found = _service.Read(id) == null ? "deletado" : "não deletado";
            Console.WriteLine(found);
        }
    }
}
