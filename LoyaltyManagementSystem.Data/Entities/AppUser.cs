using LoyaltyManagementSystem.Data.Enums;
using LoyaltyManagementSystem.Data.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracking, ISwitchable
    {
        public AppUser() { }

        public AppUser(Guid id, string fullName, 
            DateTime? birthDay,string avatar,
            string address,Status status,
            DateTime dateCreated, DateTime dateModified)
        {
            Id = id;
            FullName = fullName;
            BirthDay = birthDay;
            Avatar = avatar;
            Address = address;
            Status = status;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }

        public string FullName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
