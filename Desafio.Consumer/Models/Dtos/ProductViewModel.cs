using System.Globalization;

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

        public Product toProduct()
        {
            Product product = new Product();
            product.Code = Code;
            product.Description = Description;
            product.SaleValue = castValues(0);
            product.Name = Name;
            product.Supplier = Supplier;
            product.Value = castValues(1);
            product.Category = Category;
            product.ExpirationDate = ExpirationDate;
            return product;
        }

        public double castValues(int option)
        {
            if ( option == 0 )
                return double.Parse(SaleValue, CultureInfo.InvariantCulture);
            else 
                return double.Parse(Value, CultureInfo.InvariantCulture);
        }
    }
}
