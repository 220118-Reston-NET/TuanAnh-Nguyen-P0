using System;
using System.Collections.Generic;
using BL;
using DL;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class StoreFrontBLTest
  {
    [Fact]
    public void Should_Get_All_StoreFronts()
    {
      // Arrange
      List<StoreFront> _expectedListOfStoreFronts = new List<StoreFront>();
      _expectedListOfStoreFronts.Add(new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiTech",
        Address = "Dallas, TX",
        createdAt = new DateTime(2022 - 01 - 29)
      });

      Mock<IStoreFrontRepository> _mockRepo = new Mock<IStoreFrontRepository>();
      _mockRepo.Setup(repo => repo.GetALlStoreFronts()).Returns(_expectedListOfStoreFronts);
      IStoreFrontBL _stofBL = new StoreFrontBL(_mockRepo.Object);

      // Act
      List<StoreFront> _actualListOfStoreFronts = _stofBL.GetALlStoreFronts();

      // Assert
      Assert.Same(_expectedListOfStoreFronts, _actualListOfStoreFronts);
      Assert.Equal(_expectedListOfStoreFronts[0].Name, _actualListOfStoreFronts[0].Name);
    }

    /// <summary>
    /// Shouldn't add new storefront due to the name of new store front is existing in the database
    /// </summary>
    [Fact]
    public void Should_Not_Add_New_StoreFront()
    {
      // Arrange
      List<StoreFront> _expectedListOfStoreFronts = new List<StoreFront>();
      _expectedListOfStoreFronts.Add(new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiTech",
        Address = "Dallas, TX",
        createdAt = new DateTime(2022 - 01 - 29)
      });

      Mock<IStoreFrontRepository> _mockRepo = new Mock<IStoreFrontRepository>();
      _mockRepo.Setup(repo => repo.GetALlStoreFronts()).Returns(_expectedListOfStoreFronts);
      IStoreFrontBL _stofBL = new StoreFrontBL(_mockRepo.Object);

      StoreFront _newStoreF = new StoreFront();

      // Act & Assert
      Assert.Throws<System.Exception>(
        () => _newStoreF = _stofBL.AddStoreFront(new StoreFront()
        {
          StoreID = Guid.NewGuid().ToString(),
          Name = "KiTech",
          Address = "Dorchester, MA",
          createdAt = new DateTime(2022 - 01 - 22)
        })
      );
    }

    [Fact]
    public void Should_Not_Save_The_StoreFront()
    {
      // Arrange
      List<StoreFront> _expectedListOfStoreFronts = new List<StoreFront>();
      StoreFront _store1 = new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiTech",
        Address = "Dallas, TX",
        createdAt = new DateTime(2022 - 01 - 29)
      };
      StoreFront _store2 = new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiStore",
        Address = "Mahattan, NY",
        createdAt = new DateTime(2022 - 01 - 21)
      };
      _expectedListOfStoreFronts.Add(_store1);
      _expectedListOfStoreFronts.Add(_store2);

      Mock<IStoreFrontRepository> _mockRepo = new Mock<IStoreFrontRepository>();
      _mockRepo.Setup(repo => repo.GetALlStoreFronts()).Returns(_expectedListOfStoreFronts);
      IStoreFrontBL _stofBL = new StoreFrontBL(_mockRepo.Object);

      StoreFront _storeF = new StoreFront();

      // Act & Assert
      // Change the name of the KiTech to KiStore which is also existing in the database
      Assert.Throws<System.Exception>(
        () => _storeF = _stofBL.SaveStoreFront(new StoreFront()
        {
          StoreID = Guid.NewGuid().ToString(),
          Name = "KiStore",
          Address = "Dorchester, MA",
          createdAt = new DateTime(2022 - 01 - 22)
        })
      );
    }

    [Fact]
    public void Should_Get_StoreFront_Information_By_StoreID()
    {
      // Arrange
      List<StoreFront> _listOfStoreFronts = new List<StoreFront>();
      StoreFront _store1 = new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiTech",
        Address = "Dallas, TX",
        createdAt = new DateTime(2022 - 01 - 29)
      };
      StoreFront _store2 = new StoreFront()
      {
        StoreID = Guid.NewGuid().ToString(),
        Name = "KiStore",
        Address = "Mahattan, NY",
        createdAt = new DateTime(2022 - 01 - 21)
      };
      _listOfStoreFronts.Add(_store1);
      _listOfStoreFronts.Add(_store2);

      Mock<IStoreFrontRepository> _mockRepo = new Mock<IStoreFrontRepository>();
      _mockRepo.Setup(repo => repo.GetALlStoreFronts()).Returns(_listOfStoreFronts);
      IStoreFrontBL _stofBL = new StoreFrontBL(_mockRepo.Object);

      StoreFront _expectedStore = _store2;

      // Act
      StoreFront _actualStore = _stofBL.GetStoreFrontInfoByID(_store2.StoreID);

      // Assert
      Assert.Same(_expectedStore, _actualStore);
    }
  }
}