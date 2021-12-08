using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Promotion
{
    public class CampaignViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
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
        public ICollection<CampaignDetailViewModel> CampaignDetails { get; set; }
    }
}
