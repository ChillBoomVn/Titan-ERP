using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.API.Models
{
   
        public class SalesOrder
        {
            [Key]
            public int Id { get; set; }
            public required string Code { get; set; }

            public int? CustomerId { get; set; } // Khách lẻ có thể Null
            [ForeignKey("CustomerId")]
            public virtual Partner? Customer { get; set; }

            public DateTime OrderDate { get; set; } = DateTime.UtcNow;
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = "Completed";

            public int CreatedById { get; set; }
            [ForeignKey("CreatedById")]
            public virtual User? CreatedBy { get; set; }

            public virtual ICollection<SalesOrderItem> Items { get; set; } = new List<SalesOrderItem>();
        }

        public class SalesOrderItem
        {
            [Key]
            public int Id { get; set; }

            public int SalesOrderId { get; set; }
            [ForeignKey("SalesOrderId")]
            public virtual SalesOrder? SalesOrder { get; set; }

            public int ProductDetailId { get; set; }
            [ForeignKey("ProductDetailId")]
            public virtual ProductDetail? ProductDetail { get; set; }

            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Discount { get; set; }
        }
    
}
