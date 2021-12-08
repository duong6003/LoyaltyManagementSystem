using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Data.Interfaces;
using LoyaltyManagementSystem.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("Inventories")]
    public class Inventory : DomainEntity<int>, ISwitchable, IDateTracking
    {
        public Inventory()
        {
        }

        public Inventory(int wareHouseId,
            int productId,
            int quantity, Status status,
            DateTime dateCreated, DateTime dateModified, State productState)
        {
            WareHouseId = wareHouseId;
            ProductId = productId;
            Quantity = quantity;
            Status = status;
            DateCreated = dateCreated;
            DateModified = dateModified;
            ProductState = productState;
        }

        [Column(Order = 1)]
        public int WareHouseId { get; set; }

        [Column(Order = 2)]
        public int ProductId { get; set; }


        public int Quantity { get; set; }
        public State ProductState { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual WareHouse WareHouse { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
    }
}