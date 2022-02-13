using System.Globalization;
using System.Text.RegularExpressions;
using BL;
using Model;

namespace UI
{
  public class EditCustomerProfileMenu : IMenu
  {
    private CustomerBL _cusBL;
    public EditCustomerProfileMenu(CustomerBL p_cusBL)
    {
      _cusBL = p_cusBL;
    }
    private static Customer _cusInfo = new Customer();
    public void Display()
    {
      if (_cusInfo.Name == "")
      {
        _cusInfo = _cusBL.GetCustomerInfoByID(ListCustomersMenu._currentCustomer.CustomerID);
      }
      Console.WriteLine("Your profile:");
      Console.WriteLine("[1] - Name: " + _cusInfo.Name);
      Console.WriteLine("[2] - Address: " + _cusInfo.Address);
      Console.WriteLine("[3] - Email: " + _cusInfo.Email);
      Console.WriteLine("[4] - Phone Number: " + _cusInfo.PhoneNumber);
      Console.WriteLine("[5] - Date Of Birth: " + _cusInfo.DateOfBirth.ToShortDateString() + $"({Age(_cusInfo.DateOfBirth)})");
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
          _cusInfo.Name = _userInputName;
          return "EditCustomerProfile";
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
          _cusInfo.Address = _userInputAddress;
          return "EditCustomerProfile";
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
          _cusInfo.Email = _userInputEmail;
          return "EditCustomerProfile";
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
          _cusInfo.PhoneNumber = _userInputPhoneNumber;
          return "EditCustomerProfile";
        case "5":
          Console.WriteLine("Please enter the Date Of Birth following by this format 'MM/dd/yyyy':");
          string _userInputDOB = Console.ReadLine();

          //Check if the input is empty
          while (!IsValidDateOfBirth(_userInputDOB + " 8:30 AM"))
          {
            Console.WriteLine("Date of Birth is not in the format, please try again! Ex. 12/31/1990");
            Console.WriteLine("Please enter date of birth following by this format 'MM/dd/yyyy':");
            _userInputDOB = Console.ReadLine();
          }
          _cusInfo.DateOfBirth = Convert.ToDateTime(_userInputDOB);
          return "EditCustomerProfile";
        case "9":
          //Check if all information filled completely
          if (_cusInfo.Name == "" || _cusInfo.Address == "" || _cusInfo.Email == "" || _cusInfo.PhoneNumber == "")
          {
            Console.WriteLine("You need to fill every information above before saving!");
            System.Threading.Thread.Sleep(2000);
            return "EditCustomerProfile";
          }
          else
          {
            //Save customer back to the database
            try
            {
              Log.Information("Save customer:\n" + _cusInfo);
              _cusBL.SaveCustomer(_cusInfo);
              Log.Information("Save customer to the database");
              Console.WriteLine("Save customer succesfully!");

              //Clear the input information after saved and create a new one
              ListCustomersMenu._currentCustomer = _cusInfo;
              _cusInfo = new Customer();
              Console.WriteLine("Returning back to the main menu!");
              System.Threading.Thread.Sleep(2000);

              return "MainMenu";
            }
            catch (System.Exception exc)
            {
              Log.Warning("Failed to save customer due to the name existed in the database");
              Console.WriteLine(exc.Message);
              Console.WriteLine("Please press Enter to continue");
              Console.ReadLine();
              return "EditCustomerProfile";
            }
          }
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "EditCustomerProfile";
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

    public static bool IsValidDateOfBirth(string _dateOfBirth)
    {
      DateTime dateValue;
      if (_dateOfBirth == "")
      {
        return false;
      }
      else if (DateTime.TryParseExact(_dateOfBirth, "g", new CultureInfo("en-US"), DateTimeStyles.AllowLeadingWhite, out dateValue))
      {
        Console.WriteLine(dateValue);
        return true;
      }
      return false;
    }

    public int Age(DateTime p_dateOfBirth)
    {
      int _age = DateTime.UtcNow.Year - p_dateOfBirth.Year;
      if (DateTime.UtcNow.Month < p_dateOfBirth.Month || DateTime.UtcNow.Month == p_dateOfBirth.Month && DateTime.UtcNow.Day < p_dateOfBirth.Day)
      {
        _age--;
      }
      return _age;
    }
  }
}