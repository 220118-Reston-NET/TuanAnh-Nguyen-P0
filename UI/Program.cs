// See https://aka.ms/new-console-template for more information
using Model;
using UI;

// Console.WriteLine("Hello, World!");

// Customer cus = new Customer();
// cus.Name = "A"; //Validation is working since can't input a empty string

bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
  Console.Clear();

  menu.Display();
  string ans = menu.UserChoice();

  switch (ans)
  {
    case "CustomersMenu":
      menu = new CustomersMenu();
      break;
    case "AddNewCustomer":
      menu = new AddCustomerMenu();
      break;
    case "MainMenu":
      menu = new MainMenu();
      break;
    case "Exit":
      repeat = false;
      break;
    default:
      Console.WriteLine("Page does not exist!");
      break;
  }
}
