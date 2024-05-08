using Desafio.Domain.Entities;
using Desafio.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Testes.Context
{
    class TestMockedContext
    {
        private readonly IContext _context;

        public TestMockedContext()
        {
            _context = new MockedContext();
        }

        public void Execute()
        {
            //test.ListingTest();
            Console.WriteLine();
            //test.GetTest();
            Console.WriteLine();
            UpdateTest();
            Console.WriteLine();
            AddTest();
            Console.WriteLine();
            Deletetest();
        }

        public void ListingTest()
        {
            foreach (Product product in _context.GetAll())
            {
                Console.WriteLine(product);
            }
        }

        public void GetTest()
        {
            Console.WriteLine(_context.Get(1).ToString());
        }

        public void AddTest()
        {
            _context.Add(new Product(
                1000,
                "description",
                180.5,
                "água sanitária",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                ));
            foreach (Product product in _context.GetAll())
            {
                Console.WriteLine(product);
            }
        }

        public void UpdateTest()
        {
            _context.Update(3, new Product(
                1050,
                "NEWNEWNEW",
                180.5,
                "NEWNEWNEW",
                "loja da esquina",
                200.0,
                "limpeza",
                new DateOnly(2025, 1, 1)
                ));
            foreach (Product product in _context.GetAll())
            {
                Console.WriteLine(product);
            }
        }

        public void Deletetest()
        {
            _context.Delete(3);
            foreach (Product product in _context.GetAll())
            {
                Console.WriteLine(product);
            }
        }
    }
}
