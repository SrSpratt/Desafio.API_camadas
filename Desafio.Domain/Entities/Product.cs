using Desafio.Domain.Daos;
using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Entities
{
    public class Product : IEntity
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

        public Product(int code, string description, double saleValue, string name, string supplier, double value, string category, DateTime expirationDate, int amount)
        {
            SaleValue = saleValue;
            Code = code;
            Description = description;
            Name = name;
            Supplier = supplier;
            Value = value;
            Category = category;
            ExpirationDate = expirationDate;
            Amount = amount;
        }

        public Product(CategoryDao categoryInfo, ProductDao productInfo, StockDao stockInfo )
        {
            SaleValue = stockInfo.SaleValue;
            Code = productInfo.Code;
            Description = productInfo.Description;
            Name = productInfo.Name;
            Supplier = stockInfo.Supplier;
            Value = stockInfo.PurchaseValue;
            Category = categoryInfo.Name;
            ExpirationDate = stockInfo.ExpirationDate;
            Amount = stockInfo.Amount;
        }

        public ProductDto ToDto()
        {
            return new ProductDto(Code, Description, SaleValue, Name, Supplier, Value, Category, ExpirationDate, Amount);
        }

        public static List<ProductDto> ToDtoList(List<Product> products)
        {
            List<ProductDto> list = new List<ProductDto>();
            foreach(Product product in products)
            {
                list.Add(product.ToDto());
            }

            return list;
        }

        public override string ToString()
        {
            return $"Produto!\nCódigo: {this.Code}\nNome:{this.Name}\nDescrição:{this.Description}\nPreço:{this.SaleValue}\nFornecedor:{this.Supplier}\nCategoria:{this.Category}\nData de validade:{this.ExpirationDate}\nValor de compra:{this.Value}";
        }
    }
}
