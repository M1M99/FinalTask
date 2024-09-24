using FinalProjectORM.Models.Admin_Panel.Servis;
using FinalProjectORM.Models.UserPanel.ModelsForUser;

namespace FinalProjectORM.Accessary
{
    public class Menu
    {
        public void MenuAccessary()
        {
            Begin:
            Console.Write($"1. Admin Panel\n2. User Panel\nMake Choise : ");
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Functions functions = new Functions();
                    Console.Write("Name : ");
                    var name = Console.ReadLine();
                    Console.Write("PassWord : ");
                    var passWord = Console.ReadLine();
                    if (passWord is not null && name is not null)
                    {
                        functions.Login(name, passWord);
                    }
                    goto Begin;
                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                    Servis servis = new();
                    servis.MenuDB();
                    break;
                default:
                    Console.Clear();
                    goto Begin; //Or MenuAccesary()
            }
        }
    }
}
