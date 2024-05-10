using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Contexts
{
    public interface IContext
    {
        List<Product> GetAll();

        public Product Get(int id);

        public Product GetName(string name);

        public void Update(int id, Product product);

        public void Add(Product product);

        public void Delete(int id);

        public int NextId();
    }
}
