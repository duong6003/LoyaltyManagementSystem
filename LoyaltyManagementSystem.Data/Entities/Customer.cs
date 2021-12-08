using LoyaltyManagementSystem.Data.Interfaces;
using LoyaltyManagementSystem.Infrastructure.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("Customers")]
    public class Customer : DomainEntity<int>, IDateTracking
    {
        public Customer()
        {
        }

        public Customer(int storeId, string customerName,
            string customerAddress, string customerPhone,
            string redemptionCode, string gift,
            DateTime dayOfRedemption, DateTime dateCreated,
            DateTime dateModified)
        {
            StoreId = storeId;
            CustomerName = customerName;
            CustomerAddress = customerAddress;
            CustomerPhone = customerPhone;
            RedemptionCode = redemptionCode;
            Gift = gift;
            DayOfRedemption = dayOfRedemption;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public int StoreId { get; set; }

        [Required]
        [StringLength(128)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(250)]
        public string CustomerAddress { get; set; }

        [Required]
        [StringLength(10)]
        public string CustomerPhone { get; set; }

        [Required]
        [StringLength(50)]
        public string RedemptionCode { get; set; }

        [StringLength(128)]
        public string Gift { get; set; }

        public DateTime DayOfRedemption { get; set; }

        [ForeignKey("StoreId")]
        public virtual Store Store { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}