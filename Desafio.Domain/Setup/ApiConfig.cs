using System.Security.Cryptography.X509Certificates;

namespace Desafio.Domain.Setup
{

    public interface IApiConfig
    {
        ConnectionStrings ConnectionStrings { get; set; }
    }
    public class ApiConfig : IApiConfig
    {
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }

    }
}
