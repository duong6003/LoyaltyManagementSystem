using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Data.Interfaces;
using LoyaltyManagementSystem.Infrastructure.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("Stores")]
    public class Store : DomainEntity<int>, ISwitchable
    {
        public Store()
        {
            Customers = new List<Customer>();
        }

        public Store(string name, string address, string country, Status status)
        {
            Name = name;
            Address = address;
            Country = country;
            Status = status;
            Customers = new List<Customer>();
        }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(128)]
        public string Address { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
