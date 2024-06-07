using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Daos
{
    public class OperationDAO
    {
        public int OperationId { get; set; }
        public int StockId { get; set; }
        public string OperationType { get; set; }

        public DateTime OperationDate { get; set; }

        public string OperationUser {  get; set; }

        public int OperationAmount { get; set; }
    }
}
