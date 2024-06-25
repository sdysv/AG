using System.ComponentModel.DataAnnotations;
using System;

namespace AG.Application.Products.Models
{
    public class ProductReponseModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime FabricationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public long SupplierId { get; set; }
        public string SupplierDescription { get; set; }
        public string SupplierCNPJ { get; set; }
    }
}
