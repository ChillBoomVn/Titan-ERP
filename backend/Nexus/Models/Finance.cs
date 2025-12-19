using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nexus.API.Models
{
    
        public class Payment
        {
            [Key]
            public int Id { get; set; }
            public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
            public string? Code { get; set; }

            public int? PartnerId { get; set; }
            [ForeignKey("PartnerId")]
            public virtual Partner? Partner { get; set; }

            public decimal Amount { get; set; }
            public string PaymentMethod { get; set; } = "Cash";
            public required string Type { get; set; } // IN, OUT
            public string? ReferenceCode { get; set; }
            public string? Note { get; set; }

            public int CreatedById { get; set; }
            [ForeignKey("CreatedById")]
            public virtual User? CreatedBy { get; set; }
        }
    
}
