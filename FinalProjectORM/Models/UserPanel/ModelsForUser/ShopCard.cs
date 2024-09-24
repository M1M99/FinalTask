using FinalProjectORM.Models.UserPanel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectORM.Models.UserPanel.ModelsForUser
{
    public class ShopCard
    {
        public int Id { get; set; }
        public string ProdName { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime DateTime { get; set; }
        public ICollection<History> Histories { get; set; }
    }

}
