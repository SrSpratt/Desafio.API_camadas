namespace Desafio.Consumer.Models.Dtos
{
    public class ProductViewDto
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public double SaleValue { get; set; }
        public string Name { get; set; }

        public string Supplier { get; set; }

        public double Value { get; set; }

        public string Category { get; set; }

        public string ExpirationDate { get; set; }
    }
}
