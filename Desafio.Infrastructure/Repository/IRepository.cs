using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IRepository
    {
        public void Create(Product product);

        public void Update(int id, Product product);

        public void Delete(int id);

        public Product Read(int id);

        public string ReadCategory(int id);

        public List<Product> ReadAll();
    }
}
