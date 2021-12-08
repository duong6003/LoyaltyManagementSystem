using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Data.Interfaces
{
    public interface ISortable
    {
        public int SortOrder { get; set; }
    }
}
