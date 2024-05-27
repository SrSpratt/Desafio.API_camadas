using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Desafio.Consumer.Models.Dtos
{ //criado para facilitar a exibição dos valores, tem um conversor de produto para cá e de cá para produto
    public class ProductViewModel
    {
        public int Code { get; set; }
        public string Description { get; set; }
        [Display(Name = "Sale Value")]
        public string SaleValue { get; set; } //double
        public string Name { get; set; }

        public string Supplier { get; set; }

        [Display(Name = "Purchase Value")]
        public string Value { get; set; } //double

        public string Category { get; set; }

        [Display(Name = "Expiration Date")]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Qty.")]
        public int Amount { get; set; }

        public List<Category> categories { get; set; }

        public Product toProduct()
        {
            Product product = new Product();
            product.Code = Code;
            product.Description = Description;
            product.Value = castValues(1);
            product.Name = Name;
            product.Supplier = Supplier;
            product.SaleValue = castValues(0);
            product.Category = Category;
            product.ExpirationDate = ExpirationDate;
            product.Amount = Amount;
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
