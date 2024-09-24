using FinalProjectORM.ContextDBApp;
using FinalProjectORM.Models.UserPanel.ModelsForUser;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectORM.Models.UserPanel.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string PassWord { get; set; } = null!;
        public ICollection<ShopCard> ShopCards { get; set; }
        public ICollection<History> Histories { get; set; }

        #region Methods

        ContextMarketApp Context = new();
        Servis servis = new();
        public void Register(string mail, string name, string passWord)
        {
            var check = Context.Users.FirstOrDefault(u => u.Mail == mail || u.Name == name);
            if (check is null)
            {
                if (!string.IsNullOrWhiteSpace(mail) && !string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(passWord))
                {
                    Context.Users.Add(new User { Mail = mail, Name = name, PassWord = passWord });
                    Context.SaveChanges();
                    Console.WriteLine("Successfully Registration!");
                }
                else
                {
                    Console.WriteLine("Mail Or Name Or Pass Can't Null Or Empty");
                    throw new Exception("Mail Or Name Or Pass Can't Null Or Empty");
                }
            }
            else
            {
                Console.WriteLine("This User Is Already Registered");
            }
        }
        //public void Login(string name, string passWord)
        //{
        //    var findUser = Context.Users.FirstOrDefault(u => u.Name == name && u.PassWord == passWord);
        //    if (findUser is not null)
        //    {
        //        Console.Clear();
        //        Console.WriteLine($"Welcome {findUser.Name}");
        //        servis.MenuForUser();
        //        //type: "nvarchar(100) COLLATE Latin1_General_BIN" 
        //        //case -sensitive
        //    }
        //}

        #endregion
        
    }
}
