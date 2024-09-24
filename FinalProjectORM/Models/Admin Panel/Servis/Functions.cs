using FinalProjectORM.Accessary;
using FinalProjectORM.ContextDBApp;
using FinalProjectORM.Models.Admin_Panel.ModelsForAdmin;

namespace FinalProjectORM.Models.Admin_Panel.Servis
{

    public class Functions
    {
        ContextMarketApp DBContext = new ContextMarketApp();
        Menu menu = new Menu();
        
        public void ReturnAllProduct()
        {
            var TakeAllProduct = DBContext.Products.ToList();
            for (int i = 0; i < TakeAllProduct.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {TakeAllProduct[i].Name}");
            }
        }

        public void AddProduct(Product product)
        {
            DBContext.Products.Add(product);
            Console.WriteLine("Added Successfully!");
            DBContext.SaveChanges();
        }
        public void AddCategory(Category category)
        {
            DBContext.Categories.Add(category);
            DBContext.SaveChanges();
            Console.WriteLine("Added Category Successfully!");
        }

        public void MenuForAdmin()
        {
        Begin:
            Console.Clear();
            Console.Write("1. Add Product\n2. Add Category\n3. Statistics\n4. Log Out\nMake Choise : ");
            var check = Console.ReadKey();
            switch (check.Key)
            {
                case ConsoleKey.D1:
                    Console.Clear();
                    if (DBContext.Categories.Count() > 0)
                    {
                        Console.Write("\tAdd Product: \nEnter Product Name : ");
                        var name = Console.ReadLine();
                        Console.Write("Enter Description : ");
                        var description = Console.ReadLine();
                        Console.Write("Enter Category Id : \n");
                        var c = DBContext.Categories.ToList();
                        for (int i = 0; i < c.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {c[i].Name}");
                        }
                        int categoryId = Convert.ToInt32(Console.ReadLine());
                        var selectId = c[categoryId - 1].Id;
                        var linqMet = DBContext.Products.FirstOrDefault(p => p.CategoryId == selectId && p.Name == name);
                        if (linqMet is not null)
                        {
                            Console.WriteLine("This Product Is In the Database\nEnter - For Continue");
                            Console.ReadLine();
                            goto Begin;
                        }
                        else
                        {
                            Console.WriteLine("Enter Price : ");
                            int price = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Quantity : ");
                            int quantity = int.Parse(Console.ReadLine());
                            Product product = new Product { Name = name, Description = description, Category = c[categoryId - 1], Price = price, Quantity = quantity };
                            AddProduct(product);
                        }
                    }
                    else { Console.WriteLine("Category not available. Add Category\nClick ENTER For Continue."); Console.ReadKey(); goto Begin; }

                    break;
                case ConsoleKey.D2:
                    Console.Clear();
                CategoryN:
                    Console.Write("\tAdd Category: \nEnter Category Name : ");
                    var nameForCategory = Console.ReadLine();
                    if (!string.IsNullOrEmpty(nameForCategory) && !string.IsNullOrWhiteSpace(nameForCategory) && !DBContext.Categories.Any(c => c.Name.ToLower() == nameForCategory.ToLower()))
                    {
                        Category category = new Category { Name = nameForCategory };
                        AddCategory(category);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Product Is Already Exist Or Name Is Empty");
                        goto CategoryN;
                    }
                    break;
                case ConsoleKey.D4:
                    Console.Clear();
                    menu.MenuAccessary();
                    break;
                default:
                    goto Begin;
            }
        }
        public void Login(string name, string passWord)
        {
            Console.Clear();
            var findAdmin = DBContext.Admins.FirstOrDefault(u => u.Name == name && u.PassWord == passWord);
            if (findAdmin is not null)
            {
                Console.Clear();
                Console.WriteLine($"Welcome {findAdmin.Name}");
                MenuForAdmin();
            }
            else { Console.WriteLine("False PassWord Or False Name\nTry Again"); }
        }
    }
}
