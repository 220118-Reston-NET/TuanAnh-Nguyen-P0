using System;
using System.Collections.Generic;
using BL;
using DL;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class ProductBLTest
  {
    [Fact]
    public void Should_Get_All_Products()
    {
      // Arrange
      List<Products> _expectedListOfProducts = new List<Products>();
      _expectedListOfProducts.Add(new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "Towel",
        Price = 20,
        Desc = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });

      Mock<IProductRepository> _mockRepo = new Mock<IProductRepository>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).Returns(_expectedListOfProducts);
      IProductBL _prodBL = new ProductBL(_mockRepo.Object);

      // Act
      List<Products> _actualListOfProducts = _prodBL.GetAllProducts();

      // Assert
      Assert.Same(_expectedListOfProducts, _actualListOfProducts);
      Assert.Equal(_expectedListOfProducts[0].Name, _actualListOfProducts[0].Name);
    }

    /// <summary>
    /// Check the validation of AddProduct method to make sure that it can't be added when has the name is existing in the database
    /// </summary>
    [Fact]
    public void Product_Should_Not_Added_To_Database()
    {
      // Arrange
      List<Products> _expectedListOfProducts = new List<Products>();
      _expectedListOfProducts.Add(new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "Towel",
        Price = 20,
        Desc = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });

      Products _newProd = new Products();

      Mock<IProductRepository> _mockRepo = new Mock<IProductRepository>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).Returns(_expectedListOfProducts);
      IProductBL _prodBL = new ProductBL(_mockRepo.Object);

      // Act & Assert
      Assert.Throws<System.Exception>(
        () => _newProd = _prodBL.AddProduct(new Products()
        {
          ProductID = Guid.NewGuid().ToString(),
          Name = "Towel",
          Price = 30,
          Desc = "Hard",
          MinimumAge = 2,
          createdAt = new DateTime(2022 - 02 - 19)
        })
      );
    }

    /// <summary>
    /// Shouldn't save the product when the changed name is also existing in the database
    /// </summary>
    [Fact]
    public void Should_Not_Save_The_Product()
    {
      //Arrange
      List<Products> _expectedListOfProducts = new List<Products>();
      _expectedListOfProducts.Add(new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "Towel",
        Price = 20,
        Desc = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      });
      _expectedListOfProducts.Add(new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "iPad",
        Price = 899,
        Desc = "New Generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 02 - 19)
      });

      Products _prod = new Products();

      Mock<IProductRepository> _mockRepo = new Mock<IProductRepository>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).Returns(_expectedListOfProducts);
      IProductBL _prodBL = new ProductBL(_mockRepo.Object);

      // Act & Assert
      // Test to change name of Towel to iPad
      Assert.Throws<System.Exception>(
        () => _prod = _prodBL.SaveProduct(new Products()
        {
          ProductID = Guid.NewGuid().ToString(),
          Name = "iPad",
          Price = 20,
          Desc = "Soft, Good",
          MinimumAge = 0,
          createdAt = new DateTime(2022 - 07 - 19)
        })
      );
    }

    [Fact]
    public void Should_Get_Product_Detail_By_ProductID()
    {
      //Arrange
      List<Products> _listOfProducts = new List<Products>();
      Products _prod1 = new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "Towel",
        Price = 20,
        Desc = "Soft, Good",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 07 - 19)
      };
      Products _prod2 = new Products()
      {
        ProductID = Guid.NewGuid().ToString(),
        Name = "iPad",
        Price = 899,
        Desc = "New Generation",
        MinimumAge = 0,
        createdAt = new DateTime(2022 - 02 - 19)
      };
      _listOfProducts.Add(_prod1);
      _listOfProducts.Add(_prod2);

      Products _prod = new Products();

      Mock<IProductRepository> _mockRepo = new Mock<IProductRepository>();
      _mockRepo.Setup(repo => repo.GetAllProducts()).Returns(_listOfProducts);
      IProductBL _prodBL = new ProductBL(_mockRepo.Object);

      Products _expectedProd = _prod1;

      // Act
      Products _actualProd = _prodBL.GetProductDetail(_prod1.ProductID);

      // Assert
      Assert.Same(_expectedProd, _actualProd);
    }
  }
}