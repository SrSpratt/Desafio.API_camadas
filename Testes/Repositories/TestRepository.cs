using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desafio.Domain.Entities;
using Desafio.Infrastructure.Repository;

namespace Testes.Repositories
{
    public class TestRepository
    {
        private readonly IRepository _repository;

        public TestRepository(IRepository repository)
        {
            _repository = repository;
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
            var products = _repository.ReadAll();
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
        }

        private void ValidateProductSearch()
        {
            var product = _repository.Read(1);
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
                "2025-1-1"
                ); //ProductHelper é uma possibilidade
            _repository.Create(product);

            Console.WriteLine(_repository.Read(id));
        }

        private void ValidateProductUpdate()
        {
            int id = 1;
            Product product = _repository.Read(id);
            product.Name = "NEWNAME";
            _repository.Update(id, product);

            Console.WriteLine(_repository.Read(id));
        }

        private void ValidateProductRemoval()
        {
            int id = 1;
            _repository.Delete(id);

            string found = _repository.Read(id) == null ? "deletado" : "não deletado";
            Console.WriteLine(found);
        }


    }
}

