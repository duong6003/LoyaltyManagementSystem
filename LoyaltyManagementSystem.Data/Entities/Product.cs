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
    [Table("Products")]
    public class Product : DomainEntity<int>, IDateTracking, ISwitchable
    {
        public Product() { }
        public Product(string name,
            string imageList, string description,
            string content, string sKU,
            Status status, DateTime dateCreated,
            DateTime dateModified)
        {
            Name = name;
            ImageList = imageList;
            Description = description;
            Content = content;
            SKU = sKU;
            Status = status;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string ImageList { get; set; }
        [Required]
        [StringLength(250)]
        public string Description { get; set; }
        public string Content { get; set; }
        [Required]
        [StringLength(250)]
        public string SKU { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
