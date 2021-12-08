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
    [Table("Functions")]
    public class Function : DomainEntity<string>, ISortable, ISwitchable
    {
        public Function(){}

        public Function(string name, string url, string parentId, string iconCss, int sortOrder)
        {
            Name = name;
            Url = url;
            ParentId = parentId;
            SortOrder = sortOrder;
            this.Status = Status.Active;
        }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public int SortOrder { get; set; }
        [StringLength(128)]
        public string ParentId { get; set; }
        public Status Status { get; set; }
        [Required]
        [StringLength(250)]
        public string Url { get; set; }
    }
}
