namespace Desafio.Consumer.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public string? Method {  get; set; }

        public string? TraceParent { get; set; }

        public string? StatusCode { get; set; }

        public string? ReasonPhrase { get; set; }
        public string Message { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
