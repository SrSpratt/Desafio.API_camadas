using Desafio.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class ProductDto
    {
        public int Code { get; set; }
        public string Description { get; set; }

        public double SaleValue { get; set; }
        public string Name { get; set; }

        public string Supplier { get; set; }

        public double Value { get; set; }

        public string Category { get; set; }

        public DateOnly ExpirationDate { get; set; }

        public ProductDto(int code, string description, double salevalue, string name, string supplier, double value, string category, DateOnly expirationDate)
        {
            Code = code;
            Description = description;
            SaleValue = salevalue;
            Name = name;
            Supplier = supplier;
            Value = value;
            Category = category;
            ExpirationDate = expirationDate;
        }

        public Product ToEntity()
        {
            return new Product(Code, Description, SaleValue, Name, Supplier, Value, Category, ExpirationDate);
        }

        public static List<Product> ToEntityList(List<ProductDto> products)
        {
            List<Product> list = new List<Product>();
            foreach (ProductDto product in products)
            {
                list.Add(product.ToEntity());
            }

            return list;
        }

    }
}
