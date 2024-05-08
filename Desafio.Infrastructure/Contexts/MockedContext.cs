using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Contexts
{
    public class MockedContext : IContext
    {
        private List<Product> _products;

        public MockedContext()
        {
            LoadData();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(int id)
        {
            Product product = Get(id);
            if (product != null)
                _products.Remove(product);
        }

        public Product Get(int id)
        {
            return _products.FirstOrDefault(index => index.Code == id);
        }

        public List<Product> GetAll()
        {
            return _products.OrderBy(index => index.Code).ToList();
        }

        public void Update(int id, Product product)
        {
            var selected = Get(id);
            if (selected != null)
            {
                _products.Remove(selected);
                selected = new Product(
                    product.Code,
                    product.Description,
                    product.SaleValue,
                    product.Name,
                    product.Supplier,
                    product.Value,
                    product.Category,
                    product.ExpirationDate
                    );
                _products.Add(selected);
            } else
            {
                Console.WriteLine("Não existe um produto com o id {0}", id);
            }
        }

        private void LoadData()
        {
            _products = new List<Product>();

            for (int i = 0; i < 5; i++)
            {
                _products.Add(
                    new Product(i,
                        (i*2).ToString(),
                        180+(i+(0.5)),
                        (i*7).ToString(),
                        (i*3).ToString(),
                        200.0+(i + (0.5)),
                        (i*10).ToString(),
                        new DateOnly(2020+i, 10-i, 10+i)
                        )
                    );
            }
        }
    }
}
