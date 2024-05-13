namespace Desafio.Consumer.Models.Dtos
{
    public class Product
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public double SaleValue { get; set; }
        public string Name { get; set; }

        public string Supplier { get; set; }

        public double Value { get; set; }

        public string Category { get; set; }

        public int Amount { get; set; }

        public DateTime ExpirationDate { get; set; }

        public ProductViewModel toProduct()
        {
            ProductViewModel product = new ProductViewModel();
            product.Code = Code;
            product.Description = Description;
            product.SaleValue = castValues(0);
            product.Name = Name;
            product.Supplier = Supplier;
            product.Value = castValues(1);
            product.Category = Category;
            product.ExpirationDate = ExpirationDate;
            product.Amount = Amount;
            //product.ExpirationDate = castDate();
            return product;
        }

        /*
        public DateTime castDate()
        {
            return DateTime.Parse(ExpirationDate);
        }
        */

        public string castValues(int option)
        {
            if (option == 0)
                return SaleValue.ToString();
            else
                return Value.ToString();
        }
    }
}
