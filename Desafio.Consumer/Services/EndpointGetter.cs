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
            ENDPOINT = _configuration["App_url"];
            BaseUrl = ENDPOINT;
        }


        public string GenerateEndpoint(string metadata)
        {
            return ENDPOINT + metadata;
        }
    }
}
