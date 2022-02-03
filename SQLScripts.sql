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

DROP TABLE Customers

INSERT INTO Customers
VALUES('f8fa9c82-67aa-4826-894d-2acc68c06e21', 'Kira', 'Zanesville, OH', 'kira@gmail.com', '1234567890', '2/28/2022 3:50:37 PM')

DELETE FROM Customers

SELECT * FROM Customers

-- StoreFronts -------
CREATE TABLE StoreFronts(
	storeID varchar(50),
	storeName varchar(50),
	storeAddress varchar(50),
	createdAt smalldatetime,
	PRIMARY KEY (storeID)
)

DROP TABLE StoreFronts 

INSERT INTO StoreFronts 
VALUES('f8fa7c82-67bu-4861-894d-2vcc68c06e22', 'KiTech', 'Zanesville, OH', '2/3/2022 3:19:34 PM')

SELECT * FROM StoreFronts

DELETE FROM StoreFronts 

-- Products -------
CREATE TABLE Products(
	productID varchar(50),
	productName varchar(50),
	productPrice int,
	productDesc varchar(100),
	createdAt smalldatetime,
	PRIMARY KEY (productID)
)

DROP TABLE Products 

INSERT INTO Products
VALUES('f8fa9c82-67aa-9501-894d-2acc61c06e21', 'Macbook Pro', 2899, 'Good, New Generation', '2/3/2022 3:50:37 PM')

SELECT * FROM Products 

UPDATE Products 
SET productName = 'MacbookPro',
	productPrice = 2999,
	productDesc = 'Good, Next Generation'
WHERE productID = 'f8fa9c82-67aa-9501-894d-2acc61c06e21'

DELETE FROM Products 

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

DROP TABLE Orders 

INSERT INTO Orders
VALUES('f8fa9c82-67ba-9501-894b-2aac11c06e21', 1503, 'f8fa7c82-67bu-4861-894d-2vcc68c06e22', 'f8fa9c82-67aa-4826-894d-2acc68c06e21', '2/3/2022 3:50:37 PM')

SELECT * FROM Orders

DELETE FROM Orders 

-- LineItems -------
CREATE TABLE LineItems(
	productID varchar(50),
	orderID varchar(50),
	quantity int,
	FOREIGN KEY (productID) REFERENCES Products(productID),
	FOREIGN KEY (orderID) REFERENCES Orders(orderID)
)

DROP TABLE LineItems

INSERT INTO LineItems
VALUES('f8fa9c82-67aa-9501-894d-2acc61c06e21', 'f8fa9c82-67ba-9501-894b-2aac11c06e21', 5)

SELECT * FROM LineItems

SELECT * FROM LineItems
WHERE orderID = '2b48d6df-ccea-4428-8a88-184da9e570d0'

DELETE FROM LineItems 

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

DROP TABLE Inventory

INSERT INTO Inventory
VALUES('f9fa9c82-61aa-9501-894d-2acc61c66e21', 'f8fa9c82-67aa-9501-894d-2acc61c06e21', 'f8fa7c82-67bu-4861-894d-2vcc68c06e22', 20)

UPDATE Inventory
SET quantity = quantity + 2
WHERE inventoryID = 'f9fa9c82-61aa-9501-894d-2acc61c66e21'

UPDATE Inventory
SET quantity = quantity - 3
WHERE productID = 'f8fa9c82-67aa-9501-894d-2acc61c06e21'
	AND storeID = 'f8fa7c82-67bu-4861-894d-2vcc68c06e22'

SELECT * FROM Inventory
WHERE storeID = 'f8fa7c82-67bu-4861-894d-2vcc68c06e22'
	
SELECT p.productID, p.productName, p.productPrice, p.productDesc
FROM Inventory i, Products p
WHERE i.productID = p.productID
	AND i.storeID = 'f8fa7c82-67bu-4861-894d-2vcc68c06e22'
	AND i.quantity > 0

SELECT * FROM Inventory

DELETE  FROM Inventory 