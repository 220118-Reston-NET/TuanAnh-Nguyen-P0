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
  }
}