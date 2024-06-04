using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class StockDAO
    {
        public int Amount { get; set; }

        public double SaleValue { get; set; }

        public double PurchaseValue { get; set; }

        public string Supplier {  get; set; }

        public DateTime ExpirationDate { get; set; }

        public StockDAO(int amount, double saleValue, double purchaseValue, string supplier, DateTime expirationDate) {
            this.Amount = amount;
            this.SaleValue = saleValue;
            this.PurchaseValue = purchaseValue;
            this.Supplier = supplier;
            this.ExpirationDate = expirationDate;
        }
            
    }
}
