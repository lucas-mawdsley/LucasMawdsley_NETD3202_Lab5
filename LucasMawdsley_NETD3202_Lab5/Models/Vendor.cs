using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace LucasMawdsley_NETD3202_Lab5.Models
{
    public class Vendor
    {
        [DisplayName("Vendor ID")]
        public int VendorId { get; set; }           // Vendor primary key
        public string Name { get; set; }            // Name of vendor
        public string Address { get; set; }         // Vendor street address
        [DisplayName("Sales Rep. Last Name")]
        public string RepLastName { get; set; }     // Last name of vendor sales representative
        [DisplayName("Sales Rep. First Name")]
        public string RepFirstName { get; set; }    // First name of vendor sales representative
        [DisplayName("Sales Rep. Phone Number")]
        public string RepPhoneNumber { get; set; }  // Phone number of vendor sales representative
        [DisplayName("Sales Rep. Email Address")]
        public string RepEmail { get; set; }        // Email address of vendor sales representative

        // ICollection of related products (vendor-to-products, one-to-many)
        public ICollection<Product> Products { get; set; }
    }
}
