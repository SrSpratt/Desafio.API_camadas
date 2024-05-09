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
        private readonly ConnectionTest _connectionTest;
        public MainAppTest(TestProductService testProductService, ConnectionTest connectionTest)
        {
            _testProductService = testProductService;
            _connectionTest = connectionTest;
        }

        public void Execute()
        {
            //ValidateDomainLayer();
            //ValidateInfrastructureLayer();
            //ValidateServiceLayer();
            ValidateConnection();
        }

        private void ValidateInfrastructureLayer()
        {
            TestMockedContext test = new TestMockedContext();
            test.Execute();
        }

        private void ValidateDomainLayer()
        {
            TestsDomain test = new TestsDomain();
            test.Execute();
        }

        private void ValidateRepositoryLayer()
        {
            _testRepository.Execute();
        }

        private void ValidateServiceLayer()
        {
            _testProductService.Execute();
        }

        private void ValidateConnection()
        {
            _connectionTest.Execute();
        }
    }
}
