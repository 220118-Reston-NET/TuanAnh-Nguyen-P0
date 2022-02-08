using System;
using System.Collections.Generic;
using BL;
using DL;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class InventoryBLTest
  {
    [Fact]
    public void Should_Get_All_Inventory_From_Store()
    {
      // Arrange
      List<Inventory> _expectedListOfInventory = new List<Inventory>();
      _expectedListOfInventory.Add(new Inventory()
      {
        InventoryID = Guid.NewGuid().ToString(),
        ProductID = "edc3d007-614e-40ed-b590-1826449518f3",
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        Quantity = 5
      });

      Mock<IInventoryRepository> _mockRepo = new Mock<IInventoryRepository>();
      _mockRepo.Setup(repo => repo.GetAllProductsFromStore("d270786b-3c63-4576-bca3-13b1be8ddc7b")).Returns(_expectedListOfInventory);
      IInventoryBL _invenBL = new InventoryBL(_mockRepo.Object);

      // Act
      List<Inventory> _actualListOfInventory = _invenBL.GetAllProductsFromStore("d270786b-3c63-4576-bca3-13b1be8ddc7b");

      // Assert
      Assert.Same(_expectedListOfInventory, _actualListOfInventory);
      Assert.Equal(_expectedListOfInventory[0].StoreID, _actualListOfInventory[0].StoreID);
    }
  }
}