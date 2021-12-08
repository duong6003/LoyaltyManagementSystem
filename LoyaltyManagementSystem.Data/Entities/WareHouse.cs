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
    [Table("WareHouses")]
    public class WareHouse : DomainEntity<int>, ISwitchable
    {
        public WareHouse()
        {
        }

        public WareHouse(string name, string addresss, 
            string country, Status status, int storeId)
        {
            Name = name;
            Addresss = addresss;
            Country = country;
            Status = status;
            StoreId = storeId;
        }

        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [StringLength(250)]
        public string Addresss { get; set; }
        [Required]
        [StringLength(50)]
        public string Country { get; set; }
        public Status Status { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }

        public int StoreId { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }
    }
}
