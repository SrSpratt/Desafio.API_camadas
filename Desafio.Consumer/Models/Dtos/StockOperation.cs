using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Desafio.Consumer.Models.Dtos
{
    public class StockOperation
    {
        [Required]
        [DisplayName("Type")]
        public string OperationType { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime OperationDate { get; set; }

        [Required]
        [DisplayName("Made By")]
        public string OperationUser { get; set; }

        [Required]
        [DisplayName("Amount modified")]
        public int OperationAmount { get; set; }

    }
}

