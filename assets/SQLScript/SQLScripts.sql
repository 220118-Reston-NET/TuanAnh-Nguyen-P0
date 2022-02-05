-- P0 Revature Project --
-- Tuan Anh Nguyen --

-- Customers -------
CREATE TABLE Customers(
	cusID varchar(50),
	cusName varchar(50),
	cusAddress varchar(50),
	cusEmail varchar(50),
	cusPhoneNo varchar(10),
	createdAt smalldatetime,
	PRIMARY KEY (cusID)
)

-- StoreFronts -------
CREATE TABLE StoreFronts(
	storeID varchar(50),
	storeName varchar(50),
	storeAddress varchar(50),
	createdAt smalldatetime,
	PRIMARY KEY (storeID)
)

-- Products -------
CREATE TABLE Products(
	productID varchar(50),
	productName varchar(50),
	productPrice int,
	productDesc varchar(100),
	createdAt smalldatetime,
	PRIMARY KEY (productID)
)

-- Orders -------
CREATE TABLE Orders(
	orderID varchar(50),
	totalPrice int,
	storeID varchar(50),
	cusID varchar(50),
	createdAt smalldatetime,
	PRIMARY KEY (orderID),
	FOREIGN KEY (storeID) REFERENCES StoreFronts(storeID),
	FOREIGN KEY (cusID) REFERENCES Customers(cusID)
)

-- LineItems -------
CREATE TABLE LineItems(
	productID varchar(50),
	orderID varchar(50),
	quantity int,
	FOREIGN KEY (productID) REFERENCES Products(productID),
	FOREIGN KEY (orderID) REFERENCES Orders(orderID)
)

-- Inventory -------
CREATE TABLE Inventory(
	inventoryID varchar(50),
	productID varchar(50),
	storeID varchar(50),
	quantity int,
	PRIMARY KEY (inventoryID),
	FOREIGN KEY (storeID) REFERENCES StoreFronts(storeID),
	FOREIGN KEY (productID) REFERENCES Products(productID)
)