using FinalProjectORM.Models.UserPanel.Models;

namespace FinalProjectORM.Models.UserPanel.ModelsForUser
{
    public class History
    {
        public int Id { get; set; }
        public string ProdName1 { get; set; }
        public int ShopCardId { get; set; }
        public ShopCard ShopCard { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public decimal Price1 { get; set; }
        public int Quantity1 { get; set; }
        public DateTime DateTime1 { get; set; }
    }
}
