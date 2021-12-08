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
    [Table("Collections")]
    public class Collection : DomainEntity<int>, IDateTracking, ISwitchable
    {
        public Collection()
        {
        }

        public Collection(string name, string description,
            Status status, DateTime dateCreated, DateTime dateModified, DateTime startDate, DateTime endDate)
        {
            Name = name;
            Description = description;
            Status = status;
            DateCreated = dateCreated;
            DateModified = dateModified;
            StartDate = startDate;
            EndDate = endDate;
        }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public Status Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<CollectionDetail> CollectionDetails { get; set; }
    }
}
