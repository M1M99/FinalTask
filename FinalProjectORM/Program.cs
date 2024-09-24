using FinalProjectORM.Accessary;
using FinalProjectORM.ContextDBApp;
using FinalProjectORM.Models.Admin_Panel.ModelsForAdmin;
using FinalProjectORM.Models.UserPanel.ModelsForUser;

Servis servis = new Servis();
ContextMarketApp app = new ContextMarketApp();
Menu menu = new Menu();

#region AdminLogin
Admin admin = new Admin { Name = "Rizvan", PassWord = "Rizvan123" };
app.Admins.Add(admin);
app.SaveChanges();
#endregion

while (true)
{
    menu.MenuAccessary();
}