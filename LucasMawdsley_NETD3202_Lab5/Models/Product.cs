using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LucasMawdsley_NETD3202_Lab5.Models
{
    public class Product
    {
        public int ProductId { get; set; }      // Product primary key
        public string Name { get; set; }        // Name of product
        public string Make { get; set; }        // Product manufacturer
        public string Model { get; set; }       // Product model name/identifier
        public string Description { get; set; } // Short product description
        public int Quantity { get; set; }       // Quantity of the product available
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }   // Price per unit of product
        
        // Foreign Key from Vendors table
        [ForeignKey("Vendor")]
        [DisplayName("Vendor ID")]
        public int VendorId { get; set; }   // ID of product vendor
        // **COULDN'T GET VENDOR REFERENCE OBJECT WORKING
        public Vendor Vendor { get; set; }  // Product vendor

        // For Vendor dropdown
        [NotMapped]
        public List<SelectListItem> VendorList { get; set; }

        /// <summary>
        /// TotalPrice method for Product class
        /// Calculates the total price of a product from the quantity and unit price.
        /// </summary>
        /// <returns>Total price of the product in stock</returns>
        public double TotalPrice()
        {
            return Quantity * UnitPrice;
        }
    }
}
