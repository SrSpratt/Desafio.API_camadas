using Desafio.Consumer.Validations;
using Desafio.Consumer.Validations.ForProduct;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Models.Dtos
{
    public class Product
    {
        public int Code { get; set; }
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2} R$")]
        [Display(Name = "Sale Value")]
        [ValidateValues(0.01f, 100.0f)]
        public double SaleValue { get; set; }
        public string Name { get; set; }

        public string Supplier { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2} R$")]

        [Display(Name = "Purchase Value")]
        [ValidateValues(0.01f, 100.0f)]
        public double Value { get; set; }

        public string Category { get; set; }

        public int Amount { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        public StockOperation Operation { get; set; }

        public ProductViewModel toProduct()
        {
            ProductViewModel product = new ProductViewModel();
            product.Code = Code;
            product.Description = Description;
            product.Value = castValues(1);
            product.Name = Name;
            product.Supplier = Supplier;
            product.SaleValue = castValues(0);
            product.Category = Category;
            product.ExpirationDate = ExpirationDate;
            product.Amount = Amount;
            product.Operation = Operation;
            return product;
        }

        public string castValues(int option)
        {
            if (option == 0)
                return SaleValue.ToString();
            else
                return Value.ToString();
        }
    }
}
