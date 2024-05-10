namespace Desafio.Consumer.Models.Dtos
{
    public class ProductViewModel
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public string SaleValue { get; set; } //double
        public string Name { get; set; }

        public string Supplier { get; set; }

        public string Value { get; set; } //double

        public string Category { get; set; }

        public string ExpirationDate { get; set; }
    }
}
