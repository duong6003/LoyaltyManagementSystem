using LoyaltyManagementSystem.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace LoyaltyManagementSystem.Application.ViewModels.System
{
    public class FunctionViewModel
    {
        public string Id { get; set; }
        public int SortOrder { get; set; }
        public Status Status { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Url { get; set; }

        [StringLength(128)]
        public string ParentId { get; set; }

    }
}