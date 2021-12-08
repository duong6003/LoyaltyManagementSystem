using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Enums
{
    public enum State
    {
        [Description("Out Of Stock")]
        OutOfStock,
        [Description("In Stock")]
        InStock
    }
}
