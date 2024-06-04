using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class ProductDTO
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

        public ProductDTO(int code, string description, double salevalue, string name, string supplier, double value, string category, DateTime expirationDate, int amount)
        {
            Code = code;
            Description = description;
            SaleValue = salevalue;
            Name = name;
            Supplier = supplier;
            Value = value;
            Category = category;
            ExpirationDate = expirationDate;
            Amount = amount;
        }

        public Product ToEntity()
        {
            return new Product(Code, Description, SaleValue, Name, Supplier, Value, Category, ExpirationDate, Amount);
        }

        public static List<Product> ToEntityList(List<ProductDTO> products)
        {
            List<Product> list = new List<Product>();
            foreach (ProductDTO product in products)
            {
                list.Add(product.ToEntity());
            }

            return list;
        }

    }
}
