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

    /// <summary>
    /// Should Get all orders of Customer that ID equal to the input ID
    /// </summary>
    [Fact]
    public void Should_Get_All_Orders_By_Customer_ID()
    {
      // Arrange
      List<Orders> _listOfAllOrders = new List<Orders>();
      Orders _order1 = new Orders()
      {
        OrderID = "e5228c5d-5fe9-4147-82ca-54d202cca632",
        TotalPrice = 2900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order2 = new Orders()
      {
        OrderID = "383884c3-bf20-412b-bc65-d70ca80ddf5b",
        TotalPrice = 2100,
        StoreID = "da1d56b8-2c10-4df2-b589-85d08246f74a",
        CustomerID = "2a72e7ef-1795-48d6-8faa-f4570b9eccaf",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order3 = new Orders()
      {
        OrderID = "6287cdb2-e83a-441b-a752-46f0e3c2ac75",
        TotalPrice = 900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderRepository> _mockRepo = new Mock<IOrderRepository>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(_listOfAllOrders);
      IOrderBL _orderBL = new OrderBL(_mockRepo.Object);

      List<Orders> _expectedListOfOrders = new List<Orders>();
      _expectedListOfOrders.Add(_order1);
      _expectedListOfOrders.Add(_order3);

      List<Orders> _actualListOfOrders = new List<Orders>();
      // Act
      _actualListOfOrders = _orderBL.GetAllOrdersByCustomerID(_order1.CustomerID);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[1].Status, _actualListOfOrders[1].Status);
    }

    [Fact]
    public void Should_Get_All_Orders_By_Customer_ID_With_Filter()
    {
      // Arrange
      List<Orders> _listOfAllOrders = new List<Orders>();
      Orders _order1 = new Orders()
      {
        OrderID = "e5228c5d-5fe9-4147-82ca-54d202cca632",
        TotalPrice = 2900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order2 = new Orders()
      {
        OrderID = "383884c3-bf20-412b-bc65-d70ca80ddf5b",
        TotalPrice = 2100,
        StoreID = "da1d56b8-2c10-4df2-b589-85d08246f74a",
        CustomerID = "2a72e7ef-1795-48d6-8faa-f4570b9eccaf",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order3 = new Orders()
      {
        OrderID = "6287cdb2-e83a-441b-a752-46f0e3c2ac75",
        TotalPrice = 900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderRepository> _mockRepo = new Mock<IOrderRepository>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(_listOfAllOrders);
      IOrderBL _orderBL = new OrderBL(_mockRepo.Object);

      List<Orders> _expectedListOfOrders = new List<Orders>();
      _expectedListOfOrders.Add(_order3);

      List<Orders> _actualListOfOrders = new List<Orders>();
      // Act
      _actualListOfOrders = _orderBL.GetAllOrdersByCustomerIDWithFilter(_order3.CustomerID, _order3.Status);

      // Assert
      Assert.Equal(_expectedListOfOrders.Count, _actualListOfOrders.Count);
      Assert.Equal(_expectedListOfOrders[0].OrderID, _actualListOfOrders[0].OrderID);
      Assert.Equal(_expectedListOfOrders[0].TotalPrice, _actualListOfOrders[0].TotalPrice);
    }

    [Fact]
    public void Should_Get_Order_Detail_By_OrderID()
    {
      // Arrange
      List<Orders> _listOfAllOrders = new List<Orders>();
      Orders _order1 = new Orders()
      {
        OrderID = "e5228c5d-5fe9-4147-82ca-54d202cca632",
        TotalPrice = 2900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order2 = new Orders()
      {
        OrderID = "383884c3-bf20-412b-bc65-d70ca80ddf5b",
        TotalPrice = 2100,
        StoreID = "da1d56b8-2c10-4df2-b589-85d08246f74a",
        CustomerID = "2a72e7ef-1795-48d6-8faa-f4570b9eccaf",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Order Placed",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      Orders _order3 = new Orders()
      {
        OrderID = "6287cdb2-e83a-441b-a752-46f0e3c2ac75",
        TotalPrice = 900,
        StoreID = "d270786b-3c63-4576-bca3-13b1be8ddc7b",
        CustomerID = "9459e135-4de9-4566-88b2-85512b9e3bff",
        createdAt = new DateTime(2022 - 02 - 07),
        Status = "Shipped",
        ListLineItems = new List<LineItems>(),
        ListTrackings = new List<Shipment>()
      };
      _listOfAllOrders.Add(_order1);
      _listOfAllOrders.Add(_order2);
      _listOfAllOrders.Add(_order3);

      Mock<IOrderRepository> _mockRepo = new Mock<IOrderRepository>();
      _mockRepo.Setup(repo => repo.GetAllOrders()).Returns(_listOfAllOrders);
      IOrderBL _orderBL = new OrderBL(_mockRepo.Object);

      Orders _expectedOrder = _order2;

      // Act
      Orders _actualOrder = _orderBL.GetOrderByOrderID(_order2.OrderID);

      // Assert
      Assert.Same(_expectedOrder, _actualOrder);
    }
  }
}