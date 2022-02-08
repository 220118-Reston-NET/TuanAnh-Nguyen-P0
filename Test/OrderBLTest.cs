using System;
using System.Collections.Generic;
using BL;
using DL;
using Model;
using Moq;
using Xunit;

namespace Test
{
  public class OrderBLTest
  {
    [Fact]
    public void Should_Get_All_Orders()
    {
      // Arrange
      List<Orders> _expectedListOfOrders = new List<Orders>();
      _expectedListOfOrders.Add(new Orders()
      {
        OrderID = Guid.NewGuid().ToString(),
        TotalPrice = 2900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      });

      Mock<IOrderRepository> _mockRepo = new Mock<IOrderRepository>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(_expectedListOfOrders);
      IOrderBL _orderBL = new OrderBL(_mockRepo.Object);

      // Act
      List<Orders> _actualListOfOrders = _orderBL.GetAllOrders();

      // Assert
      Assert.Same(_expectedListOfOrders, _actualListOfOrders);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
    }
  }
}