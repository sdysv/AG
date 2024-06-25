using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Application.Products.Models
{
    public class ProductRequestModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime FabricationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCNPJ { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
    }
}
