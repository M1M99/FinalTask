using FinalProjectORM.Accessary;
using FinalProjectORM.ContextDBApp;
using FinalProjectORM.Models.Admin_Panel.Servis;
using FinalProjectORM.Models.UserPanel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProjectORM.Models.UserPanel.ModelsForUser
{
    public class Servis
    {
        ContextMarketApp Context = new();
        Menu menu = new Menu();
        User? userCurrent = null;
        int Idd;
        public void MenuDB()
        {
            User user = new User();
        j:
            if (userCurrent == null)
            {
                Console.Write("1. Register\n2. Login\n3. Main Menu\nMake Choise : ");
                var checkChoise = Console.ReadKey();
                switch (checkChoise.Key)
                {
                    case ConsoleKey.D1:
                        Console.Clear();
                        Console.WriteLine("\t\tRegister");
                        Console.Write("\nEnter Your Mail : ");
                        var mail = Console.ReadLine();
                        Console.Write("Enter Your Name : ");
                        var name = Console.ReadLine();
                        Console.Write("Enter Your PassWord : ");
                        var passWord = Console.ReadLine();
                        if (mail is not null && name is not null && passWord is not null)
                        {
                            user.Register(mail, name, passWord);
                        }
                        goto j;
                    case ConsoleKey.D2:
                        Console.Clear();
                    Log:
                        Console.Write("Enter Your Name : ");
                        var nameForLogin = Console.ReadLine();
                        Console.Write("Enter Your PassWord : ");
                        var passWordForLogin = Console.ReadLine();
                        try
                        {
                            if (!string.IsNullOrWhiteSpace(nameForLogin) && !string.IsNullOrWhiteSpace(passWordForLogin))
                            {
                                Login(nameForLogin, passWordForLogin);
                            }
                            else { Console.Clear(); Console.WriteLine("Invalid Name Or Password"); goto Log; }
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            goto j;
                        }
                        break;
                    case ConsoleKey.D3:
                        Console.Clear();
                        menu.MenuAccessary();
                        break;
                    default:
                        Console.Clear();
                        goto j;
                }
            }
            else if (userCurrent != null)
            {
                while (true)
                {
                    MenuForUser();
                }
            }
        }

        public void ReturnAllCategoryForProducts()
        {
            var TakeAllCategories = Context.Categories.ToList();
            Console.WriteLine("0. Back To Menu");
            for (int i = 0; i < TakeAllCategories.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {TakeAllCategories[i].Name}");
            }
            if (Context.Categories.Count() > 0)
            {
                Console.Write("Enter The Category Number which Products You Want To View or Go To Back : ");

                int selectCategoryIndex;

                while (!int.TryParse(Console.ReadLine(), out selectCategoryIndex) ||
                       selectCategoryIndex < 0 || selectCategoryIndex > TakeAllCategories.Count)
                {
                    Console.WriteLine("Invalid Category Number. Try Enter Valid Category Number:");
                }

                if (selectCategoryIndex == 0)
                {
                    Console.Clear();
                    MenuDB();
                }
                var selectedCategoryId = TakeAllCategories[selectCategoryIndex - 1].Id;

                var productsInCategory = Context.Products.Where(p => p.CategoryId == selectedCategoryId && p.Quantity > 0).ToList();

                if (productsInCategory.Any())
                {
                    Console.Clear();
                    Console.WriteLine($"Products In Category ({TakeAllCategories[selectCategoryIndex - 1].Name}) : ");

                    for (int i = 0; i < productsInCategory.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {productsInCategory[i].Name}");
                    }

                    Console.Write("Enter Product Number For To Buy : ");
                    int makeChoise = int.Parse(Console.ReadLine());
                    var selectId = productsInCategory[makeChoise - 1].Id;
                    var selectProd = Context.Products.FirstOrDefault(p => p.Id == selectId);



                    if (selectProd != null)
                    {
                        ShopCard shopCard = new ShopCard { ProdName = selectProd.Name, Price = selectProd.Price, Quantity = selectProd.Quantity, DateTime = DateTime.Now, UserId = Idd };
                        Context.ShopCards.Add(shopCard);
                        Console.WriteLine($"{selectProd.Name} - ${selectProd.Price}");
                        Context.SaveChanges();
                    }

                }
                else
                {
                    Console.WriteLine("No Product Found İn Selected Category.");
                }
            }
            else
            {
                Console.WriteLine("Admin Didn't Added Category! Category Not Available");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter - For Continue");
            Console.ReadKey();
            Console.ResetColor();
            Console.Clear();
        }


        public void MenuForUser()
        {
        Start:
            Console.Write("1. Categories\n2. Card\n3. Profil\n4. Log Out\nMake Choise : ");
            var checkChoise = Console.ReadKey();
            switch (checkChoise.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    Functions functions = new();
                    ReturnAllCategoryForProducts();
                    break;
                case ConsoleKey.D2:
                Card:
                    var Card = Context.ShopCards.Where(sc => sc.UserId == Idd).ToList();
                    Console.Clear();
                    if (Card.Count > 0)
                    {
                        Console.WriteLine("\tCard : ");
                        for (int i = 0; i < Card.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {Card[i].ProdName}");
                        }
                        Console.Write("\n1. Remove Product\n2. Confirm Cart\n3. Return Menu\nMake Choise : ");
                        var choise = Console.ReadKey();

                        switch (choise.Key)
                        {
                            case ConsoleKey.D1:
                                Console.Write("\nEnter Number Of The İtem When To Be Delete : ");
                                int removeProdId = int.Parse(Console.ReadLine());
                                int selecedId = Card[removeProdId - 1].Id;
                                var check = Context.ShopCards.FirstOrDefault(sc => sc.UserId == userCurrent.Id && sc.Id == selecedId);
                                if (check is not null)
                                {
                                    Context.ShopCards.Remove(check);
                                    Context.SaveChanges();
                                    Console.WriteLine("Success");
                                    Console.ReadKey();
                                    Console.Clear();
                                }
                                break;
                            case ConsoleKey.D2:
                            amount:
                                Console.Clear();
                                var amount = Card.Sum(s => s.Price);
                                Console.Write($"Total Amount - ${amount}\nEnter Amount : $");
                                int totalAmount = int.Parse(Console.ReadLine());
                                if (totalAmount < amount) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Insufficient Amount For Card"); Console.ResetColor(); Console.ReadKey(); goto amount; }
                                else if (totalAmount > amount) { Console.WriteLine($"Successfully Card  Residue - {totalAmount - amount}"); Console.ReadKey();Console.Clear(); }
                                else if (totalAmount == amount) { Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine($"Successfully! Thanks For Shop."); }

                                Console.ResetColor();
                                Console.ReadKey();
                                break;
                            case ConsoleKey.D3:
                                Console.Clear();
                                goto Start;
                            default:
                                Console.Clear();
                                Console.WriteLine("False Choise!\nClick Enter For Try Again");
                                Console.ReadKey();
                                goto Card;
                        }
                    }
                    else { Console.Clear(); ; Console.WriteLine("Card Is Empty!\nClick Enter For Return To Back"); Console.ReadKey(); }
                    break;
                case ConsoleKey.D3:
                startCase:
                    Console.Clear();
                    if (userCurrent is not null)
                    {
                        Console.Write($"\tProfil :\nName - {userCurrent.Name}\nMail - {userCurrent.Mail}\nPassWord - {userCurrent.PassWord}\n\n1. Change Name\n2. Change PassWord\n3. Back To Menu\nMake Choise : ");
                        var choiseMake = Console.ReadKey();
                        switch (choiseMake.Key)
                        {
                            case ConsoleKey.D1:
                            updatename:
                                Console.Clear();
                                Console.WriteLine("\tUpdate Name : ");
                                Console.Write("Enter Your Password For Safe : ");
                                var password = Console.ReadLine();
                                var check = Context.Users.FirstOrDefault(u => u.Id == userCurrent.Id && u.PassWord == password);
                                if (check is not null)
                                {
                                    Console.Write("\tChange Name : \nEnter Your New Name : ");
                                    var name = Console.ReadLine();
                                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
                                    {
                                        userCurrent.Name = name;
                                        Context.Users.Update(userCurrent);
                                        Context.SaveChanges();
                                        Console.WriteLine("Successfully Changed");
                                        Console.ReadKey();
                                        Console.Clear();
                                        MenuDB();
                                    }
                                }
                                else { Console.Clear(); Console.WriteLine("Try Again. False Password"); ; goto updatename; }
                                break;
                            case ConsoleKey.D2:
                            changepass:
                                Console.Clear();
                                Console.Write("\tUpdate Password : \nEnter Your Current Password : ");
                                var passCurrent = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(passCurrent))
                                {
                                    var takeCurrentPass = Context.Users.FirstOrDefault(u => u.Id == userCurrent.Id && u.PassWord == passCurrent);
                                    if (takeCurrentPass is not null)
                                    {
                                        Console.Write("\tChange Password\nEnter New Password : ");
                                        var pass = Console.ReadLine();
                                        if (!string.IsNullOrWhiteSpace(pass))
                                        {
                                            userCurrent.PassWord = pass;
                                            Context.Users.Update(userCurrent);
                                            Context.SaveChanges();
                                            Console.WriteLine("Changed Successfully!\nEnter - Continue");
                                            Console.ReadKey();
                                            Console.Clear();
                                            MenuDB();
                                        }
                                    }
                                    else { Console.Clear(); Console.WriteLine("False Password. Try Again!"); goto changepass; }
                                }
                                else goto changepass;
                                break;
                            case ConsoleKey.D3:
                                Console.Clear();
                                MenuDB();
                                break;
                            default:
                                goto startCase;
                        }
                    }
                    break;
                case ConsoleKey.D4:
                    userCurrent = null;
                    Console.Clear();
                    MenuDB();
                    break;
                default:
                    break;
            }

        }

        public void Login(string name, string passWord)
        {
            var findUser = Context.Users.FirstOrDefault(u => u.Name == name && u.PassWord == passWord);
            if (findUser is not null)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {findUser.Name}");
                Idd = findUser.Id;
                userCurrent = findUser;
                userCurrent.Id = findUser.Id;
                MenuForUser();
                //type: "nvarchar(100) COLLATE Latin1_General_BIN" 
                //case -sensitive
            }
            //else
            //{
            Console.Clear();
            Console.WriteLine("Wrong Try Again!");
            MenuDB();
            //}
        }
    }
}
