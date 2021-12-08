using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Interfaces
{
    public interface ISwitchable
    {
        Status Status { get; set; }
    }
}
