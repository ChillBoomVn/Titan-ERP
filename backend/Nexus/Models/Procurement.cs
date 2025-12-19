using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Nexus.API.Models
{

        public class PurchaseOrder
        {
            [Key]
            public int Id { get; set; }
            public required string Code { get; set; }

            public int SupplierId { get; set; }
            [ForeignKey("SupplierId")]
            public virtual Partner? Supplier { get; set; }

            public DateTime OrderDate { get; set; } = DateTime.UtcNow;
            public decimal TotalAmount { get; set; }
            public string Status { get; set; } = "Draft";

            public int CreatedById { get; set; }
            [ForeignKey("CreatedById")]
            public virtual User? CreatedBy { get; set; }

            public virtual ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();
        }

        public class PurchaseOrderItem
        {
            [Key]
            public int Id { get; set; }

            public int PurchaseOrderId { get; set; }
            [ForeignKey("PurchaseOrderId")]
            public virtual PurchaseOrder? PurchaseOrder { get; set; }

            public int ProductDetailId { get; set; }
            [ForeignKey("ProductDetailId")]
            public virtual ProductDetail? ProductDetail { get; set; }

            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }

            public DateTime? ExpiryDate { get; set; } // Hạn sử dụng
        }
    
}
