using Desafio.Consumer.Models.Enums;

namespace Desafio.Consumer.Services
{
    public class EndpointGetter
    {
        private readonly string ENDPOINT = "";
        private readonly IConfiguration _configuration;
        public string BaseUrl { get; set;}

        public EndpointGetter(IConfiguration configuration)
        {
            _configuration = configuration;
            ENDPOINT = _configuration["App_url:Base"];
            BaseUrl = ENDPOINT;
        }

        public void SetBaseUrl(int number)
        {
            switch (number)
            {
                case 1:
                    BaseUrl += _configuration["App_url:Product"];
                    break;
                case 2:
                    BaseUrl += _configuration["App_url:User"];
                    break;
            }
        }


        public string GenerateEndpoint(string metadata)
        {
            return BaseUrl + metadata;
        }

        public string GenerateCrossEndpoint(string metadata, int apiController)
        {
            /*  1 -> Product
                2 -> Category
                3 -> User
                4 -> Operation
            */
            string endpoint = ENDPOINT;
            switch (apiController)
            {
                case 1:
                    endpoint += _configuration["App_url:Product"];
                    break;
                case 2:
                    endpoint += _configuration["App_url:Category"];
                    break;
                case 3:
                    endpoint += _configuration["App_url:User"];
                    break;
                case 4:
                    endpoint += _configuration["App_url:Operation"];
                    break;
            }
            return endpoint;
        }
    }
}
