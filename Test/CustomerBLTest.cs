using System;
using System.Collections.Generic;
using BL;
using DL;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class CustomerBLTest
  {
    [Fact]
    public void Should_Get_All_Customers()
    {
      // Arrange
      List<Customer> _expectedListOfCustomers = new List<Customer>();
      _expectedListOfCustomers.Add(new Customer()
      {
        CustomerID = Guid.NewGuid().ToString(),
        Name = "Tester",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });

      Mock<ICustomerRepository> _mockRepo = new Mock<ICustomerRepository>();
      _mockRepo.Setup(repo => repo.GetALlCustomers()).Returns(_expectedListOfCustomers);
      ICustomerBL _cusBL = new CustomerBL(_mockRepo.Object);

      // Act
      List<Customer> _actualListOfCustomer = _cusBL.GetALlCustomers();

      // Assert
      Assert.Same(_expectedListOfCustomers, _actualListOfCustomer);
      Assert.Equal(_expectedListOfCustomers[0].Name, _actualListOfCustomer[0].Name);
      Assert.Equal(_expectedListOfCustomers[0].Email, _actualListOfCustomer[0].Email);
    }

    /// <summary>
    /// Should not add a new customer when the name is existing in the database
    /// </summary>
    [Fact]
    public void Should_Not_Add_New_Customer()
    {
      // Arrange
      List<Customer> _expectedListOfCustomers = new List<Customer>();
      _expectedListOfCustomers.Add(new Customer()
      {
        CustomerID = Guid.NewGuid().ToString(),
        Name = "Tester",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });

      Mock<ICustomerRepository> _mockRepo = new Mock<ICustomerRepository>();
      _mockRepo.Setup(repo => repo.GetALlCustomers()).Returns(_expectedListOfCustomers);
      ICustomerBL _cusBL = new CustomerBL(_mockRepo.Object);

      Customer _newCus = new Customer();
      // Act & Assert
      // Shouldn't add new customer due to the name is existing in the database
      Assert.Throws<System.Exception>(
        () => _newCus = _cusBL.AddCustomer(new Customer()
        {
          CustomerID = Guid.NewGuid().ToString(),
          Name = "Tester",
          Address = "Miami, FL",
          Email = "tester222@gmail.com",
          PhoneNumber = "1234567890",
          createdAt = new DateTime(2022 - 02 - 01),
          DateOfBirth = new DateTime(1994 - 02 - 03),
        })
      );
    }

    [Fact]
    public void Should_Not_Save_The_Customer()
    {
      // Arrange
      List<Customer> _expectedListOfCustomers = new List<Customer>();
      _expectedListOfCustomers.Add(new Customer()
      {
        CustomerID = Guid.NewGuid().ToString(),
        Name = "Tester",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1990 - 02 - 03),
      });
      _expectedListOfCustomers.Add(new Customer()
      {
        CustomerID = Guid.NewGuid().ToString(),
        Name = "Tester2",
        Address = "Miami, FL",
        Email = "tester222@gmail.com",
        PhoneNumber = "1234567890",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1994 - 02 - 03),
      });

      Mock<ICustomerRepository> _mockRepo = new Mock<ICustomerRepository>();
      _mockRepo.Setup(repo => repo.GetALlCustomers()).Returns(_expectedListOfCustomers);
      ICustomerBL _cusBL = new CustomerBL(_mockRepo.Object);

      Customer _cus = new Customer();
      // Act & Assert
      // Change the name of the customer from Tester to Tester2 which is existing in the database
      Assert.Throws<System.Exception>(
        () => _cus = _cusBL.SaveCustomer(new Customer()
        {
          CustomerID = Guid.NewGuid().ToString(),
          Name = "Tester2",
          Address = "San Diego, CA",
          Email = "tester@gmail.com",
          PhoneNumber = "9018273645",
          createdAt = new DateTime(2022 - 02 - 01),
          DateOfBirth = new DateTime(1990 - 02 - 03),
        })
      );
    }

    /// <summary>
    /// This test for GetCustomerInfoByID method, the customerID should be in the database
    /// </summary>
    [Fact]
    public void Should_Get_Customer_Information_Matched_Id()
    {
      // Arrange
      List<Customer> _listOfCustomers = new List<Customer>();
      Customer _cus1 = new Customer()
      {
        CustomerID = "65a810a2-149d-4ad7-80b6-91cd8e2ff8cb",
        Name = "Tester",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1990 - 02 - 03),
      };
      Customer _cus2 = new Customer()
      {
        CustomerID = "2a72e7ef-1795-48d6-8faa-f4570b9eccaf",
        Name = "Tester2",
        Address = "Miami, FL",
        Email = "tester222@gmail.com",
        PhoneNumber = "1234567890",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1994 - 02 - 03),
      };
      _listOfCustomers.Add(_cus1);
      _listOfCustomers.Add(_cus2);

      Mock<ICustomerRepository> _mockRepo = new Mock<ICustomerRepository>();
      _mockRepo.Setup(repo => repo.GetALlCustomers()).Returns(_listOfCustomers);
      ICustomerBL _cusBL = new CustomerBL(_mockRepo.Object);

      Customer _expectedCustomer = _cus2;

      Customer _actualCustomer = new Customer();
      // Act
      _actualCustomer = _cusBL.GetCustomerInfoByID(_cus2.CustomerID);

      // Assert
      Assert.Same(_expectedCustomer, _actualCustomer);
      Assert.Equal(_expectedCustomer.CustomerID, _actualCustomer.CustomerID);
      Assert.Equal(_expectedCustomer.Name, _actualCustomer.Name);
      Assert.Equal(_expectedCustomer.Address, _actualCustomer.Address);
      Assert.Equal(_expectedCustomer.Email, _actualCustomer.Email);
      Assert.Equal(_expectedCustomer.PhoneNumber, _actualCustomer.PhoneNumber);
      Assert.Equal(_expectedCustomer.createdAt, _actualCustomer.createdAt);
      Assert.Equal(_expectedCustomer.DateOfBirth, _actualCustomer.DateOfBirth);
    }

    /// <summary>
    /// Test the SearchCustomersByName method, should return all the customer object that have Name contains input
    /// </summary>
    [Fact]
    public void Should_Get_All_Customer_That_Name_Contains_Input()
    {
      // Arrange
      List<Customer> _listOfCustomers = new List<Customer>();
      Customer _cus1 = new Customer()
      {
        CustomerID = "65a810a2-149d-4ad7-80b6-91cd8e2ff8cb",
        Name = "Tester",
        Address = "San Diego, CA",
        Email = "tester@gmail.com",
        PhoneNumber = "9018273645",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1990 - 02 - 03),
      };
      Customer _cus2 = new Customer()
      {
        CustomerID = "2a72e7ef-1795-48d6-8faa-f4570b9eccaf",
        Name = "Tester2",
        Address = "Miami, FL",
        Email = "tester222@gmail.com",
        PhoneNumber = "1234567890",
        createdAt = new DateTime(2022 - 02 - 01),
        DateOfBirth = new DateTime(1994 - 02 - 03),
      };
      Customer _cus3 = new Customer()
      {
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        Name = "Manager",
        Address = "Boston, MA",
        Email = "manager@gmail.com",
        PhoneNumber = "8903126745",
        createdAt = new DateTime(2022 - 02 - 02),
        DateOfBirth = new DateTime(1980 - 06 - 22),
      };
      _listOfCustomers.Add(_cus1);
      _listOfCustomers.Add(_cus2);
      _listOfCustomers.Add(_cus3);

      Mock<ICustomerRepository> _mockRepo = new Mock<ICustomerRepository>();
      _mockRepo.Setup(repo => repo.GetALlCustomers()).Returns(_listOfCustomers);
      ICustomerBL _cusBL = new CustomerBL(_mockRepo.Object);

      List<Customer> _expectedListOfCustomer = new List<Customer>();
      _expectedListOfCustomer.Add(_cus3);

      // Act
      List<Customer> _actualListOfCustomer = _cusBL.SearchCustomersByName(_cus3.Name);

      // Assert
      Assert.Equal(_expectedListOfCustomer.Count, _actualListOfCustomer.Count);
    }
  }
}