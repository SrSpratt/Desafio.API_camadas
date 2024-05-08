using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testes.Context;
using Testes.Domain;
using Testes.Repositories;
using Testes.Services;

namespace Testes.Main
{
    public class MainAppTest
    {
        private readonly TestRepository _testRepository;
        private readonly TestProductService _testProductService;
        public MainAppTest(TestProductService testProductService)
        {
            _testProductService = testProductService;
        }

        public void Execute()
        {
            ValidateServiceLayer();
        }

        private void ValidateInfrastructureLayer()
        {
            TestMockedContext test = new TestMockedContext();
            //test.ListingTest();
            Console.WriteLine();
            //test.GetTest();
            Console.WriteLine();
            test.UpdateTest();
            Console.WriteLine();
            test.AddTest();
            Console.WriteLine();
            test.Deletetest();
        }

        private void ValidateDomainLayer()
        {
            TestsDomain test = new TestsDomain();
            test.EntityTest();
            Console.WriteLine();
            test.DtoTest();
            Console.WriteLine("\nNow, conversions:");
            test.ConversionEntityTest();
            test.ConversionDtoTest();

            Console.WriteLine();


        }

        private void ValidateRepositoryLayer()
        {
            _testRepository.Execute();
        }

        private void ValidateServiceLayer()
        {
            _testProductService.Execute();
        }
    }
}
