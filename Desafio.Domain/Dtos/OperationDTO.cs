using Desafio.Domain.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Desafio.Domain.Dtos
{
    public class OperationDTO
    {
        public string OperationType { get; set; }

        public DateTime OperationDate { get; set; }

        public string OperationUser { get; set; }

        public int OperationAmount { get; set; }

        [JsonConstructor]
        public OperationDTO(string operationType, DateTime operationDate, string operationUser, int operationAmount)
        {
            OperationType = operationType;
            OperationDate = operationDate;
            OperationUser = operationUser;
            OperationAmount = operationAmount;
        }
        public OperationDTO(OperationDAO operation) 
        {
            OperationType = operation.OperationType;
            OperationDate = operation.OperationDate;
            OperationUser = operation.OperationUser;
            OperationAmount = operation.OperationAmount;
        }
    }
}
