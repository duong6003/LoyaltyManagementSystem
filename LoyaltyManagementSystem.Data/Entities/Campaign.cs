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
    [Table("Campaigns")]
    public class Campaign : DomainEntity<int>, ISwitchable, IDateTracking 
    {
        public Campaign()
        {
        }

        public Campaign(string name, string description,
            Status status, DateTime dateCreated, DateTime dateModified, DateTime startDate, DateTime endDate = default)
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
        public  string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(128)]
        public string OrganizerName { get; set; }
        [StringLength(128)]
        public string OrganizerPhone { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual ICollection<CampaignDetail> CampaignDetails { get; set; }
    }
}
