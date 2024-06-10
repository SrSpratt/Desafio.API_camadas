using Desafio.Domain.Daos;
using System.Text.Json.Serialization;

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

        public int StockId { get; set; } 

        public DateTime ExpirationDate { get; set; }

        public OperationDTO Operation { get; set; }

        public ProductDTO() { }

        //[JsonConstructor] //desativar isso aqui para testar os erros no navegador
        public ProductDTO(int code, string description, double salevalue, string name, string supplier, double value, string category, int amount, int stockId, DateTime expirationDate, OperationDTO operation)
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
            StockId = stockId;
            Operation = operation;
        }

        public ProductDTO(ProductDAO productInfo, CategoryDAO categoryInfo, StockDAO stockInfo)
        {
            Code = productInfo.Code;
            Description = productInfo.Description;
            Name = productInfo.Name;
            SaleValue = stockInfo.SaleValue;
            Supplier = stockInfo.Supplier;
            Value = stockInfo.PurchaseValue;
            ExpirationDate = stockInfo.ExpirationDate;
            Amount = stockInfo.Amount;
            StockId = stockInfo.ID;
            Category = categoryInfo.Name;
        }


    }
}
