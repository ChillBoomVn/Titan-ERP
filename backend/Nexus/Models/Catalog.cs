using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexus.API.Models
{
    
        public class Product
        {
            [Key]
            public int Id { get; set; }
            public required string Name { get; set; }
            public string? Category { get; set; }
            public string? Description { get; set; }
        }

        public class ProductDetail
        {
            [Key]
            public int Id { get; set; }

            public int ProductId { get; set; }
            [ForeignKey("ProductId")]
            public virtual Product? Product { get; set; }

            public required string Barcode { get; set; } // Mã vạch (Unique)
            public string? SKU { get; set; }
            public required string ProductName { get; set; } // Tên in hóa đơn
            public required string Unit { get; set; } // Gói, Thùng, Lon...

            public decimal ImportPrice { get; set; }
            public decimal SalePrice { get; set; }
            public string? Thumbnail { get; set; }
        }
    
}
