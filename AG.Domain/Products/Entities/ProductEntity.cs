using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Domain.Products.Entities
{
    public class ProductEntity
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string Description { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime FabricationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCNPJ { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }

        public bool IsValid()
        {
            return this.FabricationDate < this.ExpirationDate;
        }
    }
}
