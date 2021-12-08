using LoyaltyManagementSystem.Data.Entities;
using LoyaltyManagementSystem.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyManagementSystem.Application.ViewModels.Product
{
    public class InventoryViewModel
    {
        public int Id { get; set; }
        public int WareHouseId { get; set; }

        public int ProductId { get; set; }


        public int Quantity { get; set; }
        public State ProductState { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
