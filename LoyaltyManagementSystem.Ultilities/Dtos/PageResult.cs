using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Ultilities.Dtos
{
    public class PageResult<T> : PageResultBase
    {
        public PageResult()
        {
            Results = new List<T>();
        }
        public List<T> Results { get; set; }
    }
}
