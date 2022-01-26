using System;
using System.Globalization;
using System.Text.RegularExpressions;

using BL;
using Model;

namespace UI
{
  public class AddCustomerMenu : IMenu
  {
    private static Customer _newCustomer = new Customer();

    private ICustomerBL _cusBL;
    public AddCustomerMenu(ICustomerBL p_cus)
    {
      _cusBL = p_cus;
    }
    public void Display()
    {
      Console.WriteLine("Enter new customer information");
      Console.WriteLine("[1] - Name: " + _newCustomer.Name);
      Console.WriteLine("[2] - Address: " + _newCustomer.Address);
      Console.WriteLine("[3] - Email: " + _newCustomer.Email);
      Console.WriteLine("[4] - Phone Number: " + _newCustomer.PhoneNumber);
      Console.WriteLine("-----");
      Console.WriteLine("[9] - Save & Go back");
      Console.WriteLine("[0] - Go back");
      Console.WriteLine("What do you want to modify:");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";
        case "1":
          Console.WriteLine("Please enter customer name:");
          string _userInputName = Console.ReadLine();

          //Check if the input is empty
          while (_userInputName == "")
          {
            Console.WriteLine("Name should not be empty!");
            Console.WriteLine("Please enter customer name:");
            _userInputName = Console.ReadLine();
          }
          _newCustomer.Name = _userInputName;
          return "AddNewCustomer";
        case "2":
          Console.WriteLine("Please enter the address:");
          string _userInputAddress = Console.ReadLine();

          //Check if the input is empty
          while (_userInputAddress == "")
          {
            Console.WriteLine("Address should not be empty!");
            Console.WriteLine("Please enter customer address:");
            _userInputAddress = Console.ReadLine();
          }
          _newCustomer.Address = _userInputAddress;
          return "AddNewCustomer";
        case "3":
          Console.WriteLine("Please enter the email:");
          string _userInputEmail = Console.ReadLine();

          //Check if the input is empty
          while (!IsValidEmail(_userInputEmail))
          {
            Console.WriteLine("Email is not in the format, please try again! Ex. user@example.com");
            Console.WriteLine("Please enter customer email address:");
            _userInputEmail = Console.ReadLine();
          }
          _newCustomer.Email = _userInputEmail;
          return "AddNewCustomer";
        case "4":
          Console.WriteLine("Please enter the phone number:");
          string _userInputPhoneNumber = Console.ReadLine();

          //Check if the input is empty
          // while (_userInputPhoneNumber == "")
          while (!IsValidPhoneNumber(_userInputPhoneNumber))
          {
            Console.WriteLine("Phone number is not in the format, please try again! Ex. 1234567899");
            Console.WriteLine("Please enter customer phone number:");
            _userInputPhoneNumber = Console.ReadLine();
          }
          _newCustomer.PhoneNumber = _userInputPhoneNumber;
          return "AddNewCustomer";
        case "9":
          //Check if all information filled completely
          if (_newCustomer.Name == "" || _newCustomer.Address == "" || _newCustomer.Email == "" || _newCustomer.PhoneNumber == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "AddNewCustomer";
          }
          else
          {
            //Add customer to the database
            _cusBL.AddCustomer(_newCustomer);
            Console.WriteLine("Added new customer succesfully!");
            Console.WriteLine("Returning back to the main menu!");
            System.Threading.Thread.Sleep(2000);

            //Clear the input information after saved and create a new one
            _newCustomer = new Customer();
            return "MainMenu";
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewCustomer";
      }
    }

    //Email Validation using the method created by Microsoft
    //Ref: https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format
    public static bool IsValidEmail(string email)
    {
      if (string.IsNullOrWhiteSpace(email))
        return false;

      try
      {
        // Normalize the domain
        email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

        // Examines the domain part of the email and normalizes it.
        string DomainMapper(Match match)
        {
          // Use IdnMapping class to convert Unicode domain names.
          var idn = new IdnMapping();

          // Pull out and process domain name (throws ArgumentException on invalid)
          string domainName = idn.GetAscii(match.Groups[2].Value);

          return match.Groups[1].Value + domainName;
        }
      }
      catch (RegexMatchTimeoutException e)
      {
        return false;
      }
      catch (ArgumentException e)
      {
        return false;
      }

      try
      {
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
      }
      catch (RegexMatchTimeoutException)
      {
        return false;
      }
    }

    //Phone Number Validation
    public static bool IsValidPhoneNumber(string _phoneNumber)
    {
      if (_phoneNumber == "" || _phoneNumber.Length != 10)
      {
        return false;
      }
      else if (!Regex.IsMatch(_phoneNumber, @"^[0-9]*$"))
      {
        return false;
      }
      else
      {
        return true;
      }
    }
  }
}