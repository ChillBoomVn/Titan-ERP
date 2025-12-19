using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.API.Models
{
    
        public class Warehouse
        {
            [Key]
            public int Id { get; set; }
            public required string Name { get; set; }
            public string? Location { get; set; }
        }

        public class InventoryStock
        {
            [Key]
            public int Id { get; set; }

            public int WarehouseId { get; set; }
            [ForeignKey("WarehouseId")]
            public virtual Warehouse? Warehouse { get; set; }

            public int ProductDetailId { get; set; }
            [ForeignKey("ProductDetailId")]
            public virtual ProductDetail? ProductDetail { get; set; }

            public int Quantity { get; set; }
        }

        public class StockTransaction
        {
            [Key]
            public int Id { get; set; }
            public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

            public int WarehouseId { get; set; }
            [ForeignKey("WarehouseId")]
            public virtual Warehouse? Warehouse { get; set; }

            public int ProductDetailId { get; set; }
            [ForeignKey("ProductDetailId")]
            public virtual ProductDetail? ProductDetail { get; set; }

            public required string Type { get; set; } // IMPORT, EXPORT
            public int QuantityChanged { get; set; }
            public string? ReferenceCode { get; set; }
            public string? Note { get; set; }

            public int CreatedById { get; set; }
            [ForeignKey("CreatedById")]
            public virtual User? CreatedBy { get; set; }
        }
    
}
