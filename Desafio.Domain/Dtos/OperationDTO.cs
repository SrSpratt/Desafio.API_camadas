using Desafio.Domain.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class OperationDTO
    {
        public string OperationType { get; set; }

        public DateTime OperationDate { get; set; }

        public string OperationUser { get; set; }

        public int OperationAmount { get; set; }

        public OperationDTO(OperationDAO operation) 
        {
            OperationType = operation.OperationType;
            OperationDate = operation.OperationDate;
            OperationUser = operation.OperationUser;
            OperationAmount = operation.OperationAmount;
        }
    }
}
