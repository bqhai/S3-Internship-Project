CREATE DATABASE DbCellPhoneStore
GO

USE DbCellPhoneStore
GO
-------------CREATE TABLE----------------
CREATE TABLE Role
(
	RoleID INT PRIMARY KEY,
	RoleName NVARCHAR(100) NOT NULL,
)
GO


CREATE TABLE Account
(
	Username NVARCHAR(50) PRIMARY KEY,
	Password NVARCHAR(MAX) NOT NULL,
	RoleID INT NOT NULL,
	Status BIT, --0: Block, 1: Active

	FOREIGN KEY(RoleID) REFERENCES Role(RoleID)
)
GO

CREATE TABLE Customer
(
	CustomerID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(100),
	Username NVARCHAR(50) UNIQUE,
	Email NVARCHAR(100) UNIQUE,
	Address NVARCHAR(500),
	PhoneNumber NVARCHAR(50) UNIQUE,
	Sex NVARCHAR(50),
	DateOfBirth NVARCHAR(50),

	FOREIGN KEY(Username) REFERENCES Account(Username)
)
GO

CREATE TABLE Employee
(
	EmployeeID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(100),
	Username NVARCHAR(50) UNIQUE,
	Email NVARCHAR(100) UNIQUE,
	Address NVARCHAR(500),
	PhoneNumber NVARCHAR(50) UNIQUE,
	Sex NVARCHAR(50),
	DateOfBirth NVARCHAR(50),
	Status BIT, --0:Stop working, --1: Working
	FOREIGN KEY(Username) REFERENCES Account(Username)
)
GO
CREATE TABLE Brand
(
	BrandID NVARCHAR(50) PRIMARY KEY,
	BrandName NVARCHAR(100),
	Country NVARCHAR(100),
	Phone BIT, --0: 
	Laptop BIT,
)
GO

CREATE TABLE Smartphone
(
	SmartphoneID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(100),
	BrandID NVARCHAR(50),
	Size NVARCHAR(100),
	Weight NVARCHAR(100),
	ScreenType NVARCHAR(500),
	ScreenSize NVARCHAR(100),
	ScreenResolution NVARCHAR(100),
	FrontCamera NVARCHAR(100),
	BackCamera NVARCHAR(100),
	OperatingSystem NVARCHAR(100),
	Chipset NVARCHAR(100),
	CPU NVARCHAR(100),
	GPU NVARCHAR(100),
	SDCard NVARCHAR(100) DEFAULT N'Không hỗ trợ',
	Network NVARCHAR(200),
	WLAN NVARCHAR(100),
	Bluetooth NVARCHAR(100),
	GPS NVARCHAR(100),
	NFC NVARCHAR(100) DEFAULT N'Không hỗ trợ',
	USB NVARCHAR(100),
	SIM NVARCHAR(100),
	Sensor NVARCHAR(500),
	Battery NVARCHAR(100),
	Image NVARCHAR(500),
	
	FOREIGN KEY(BrandID) REFERENCES Brand(BrandID)
)
GO

CREATE TABLE SmartphoneVersion
(
	SmartphoneVersionID NVARCHAR(50) PRIMARY KEY,
	SmartphoneID NVARCHAR(50),
	RAM NVARCHAR(100),
	ROM NVARCHAR(100),
	Color NVARCHAR(100),
	Price INT,
	QuantityInStock INT,
	Status NVARCHAR(100),
	
	FOREIGN KEY(SmartphoneID) REFERENCES Smartphone(SmartphoneID)
)
GO

CREATE TABLE Laptop
(
	LaptopID NVARCHAR(50) PRIMARY KEY,
	Name NVARCHAR(100),
	BrandID NVARCHAR(50),
	Size NVARCHAR(100),
	Weight NVARCHAR(100),
	ScreenType NVARCHAR(500),
	ScreenSize NVARCHAR(100),
	ScreenResolution NVARCHAR(100),
	OperatingSystem NVARCHAR(100),
	OSVersion NVARCHAR(100),	
	WLAN NVARCHAR(100),
	Bluetooth NVARCHAR(100),
	Port NVARCHAR(100),
	Battery NVARCHAR(100),
	Image NVARCHAR(500),

	FOREIGN KEY(BrandID) REFERENCES Brand(BrandID)
)
GO

CREATE TABLE LaptopVersion
(
	LaptopVersionID NVARCHAR(50) PRIMARY KEY,
	LaptopID NVARCHAR(50),
	CPU NVARCHAR(100),
	VGA NVARCHAR(100),
	RAM NVARCHAR(100),
	HardDrive NVARCHAR(100),
	Price INT,
	QuantityInStock INT,
	Status NVARCHAR(100),

	FOREIGN KEY(LaptopID) REFERENCES Laptop(LaptopID)
)
GO

CREATE TABLE Orders
(
	OrderID NVARCHAR(50) PRIMARY KEY,
	Payments NVARCHAR(100),
	Delivery NVARCHAR(100),
	Notes NVARCHAR(500),
	OrderDate DATE,
	TotalPrice INT,
	CustomerID NVARCHAR(50),
	Status BIT, --0: Unpaid --1: Paid
	
	FOREIGN KEY(CustomerID) REFERENCES Customer(CustomerID)
)
GO

CREATE TABLE OrderDetail
(
	OrderID NVARCHAR(50),
	ProductVersionID NVARCHAR(50),
	Amount INT,
	Price INT,

	PRIMARY KEY(OrderID, ProductVersionID),
	FOREIGN KEY(OrderID) REFERENCES Orders(OrderID),
	FOREIGN KEY(ProductVersionID) REFERENCES SmartphoneVersion(SmartphoneVersionID),
	FOREIGN KEY(ProductVersionID) REFERENCES LaptopVersion(LaptopVersionID)

)
GO

CREATE TABLE ProductIntroduce
(
	ProductID NVARCHAR(50) PRIMARY KEY,
	Header1 NVARCHAR(MAX),
	Text1 NVARCHAR(MAX),
	Image1 NVARCHAR(100),
	Header2 NVARCHAR(MAX),
	Text2 NVARCHAR(MAX),
	Image2 NVARCHAR(100),
	Header3 NVARCHAR(MAX),
	Text3 NVARCHAR(MAX),
	Image3 NVARCHAR(100),
	Header4 NVARCHAR(MAX),
	Text4 NVARCHAR(MAX),
	Image4 NVARCHAR(100),
	Header5 NVARCHAR(MAX),
	Text5 NVARCHAR(MAX),
	Image5 NVARCHAR(100),
	Text6 NVARCHAR(MAX),
	Image7 NVARCHAR(100),

	FOREIGN KEY(ProductID) REFERENCES Smartphone(SmartphoneID),
	FOREIGN KEY(ProductID) REFERENCES Laptop(LaptopID)
)
GO

-------------INSERT DATA----------------
--Role--
INSERT INTO Role VALUES(1, N'Quản trị hệ thống')
INSERT INTO Role VALUES(2, N'Nhân viên')
INSERT INTO Role VALUES(3, N'Khách hàng')

--Account--
INSERT INTO Account VALUES(N'bqhai1205', N'123456', 1, 1)
INSERT INTO Account VALUES(N'hdhieu2610', N'123456', 2, 1)
INSERT INTO Account VALUES(N'dtqnhu', N'123456', 3, 1)

--Employee--
INSERT INTO Employee VALUES(N'EMP100', N'Bùi Quang Hải', N'bqhai1205', N'bqhai@gmail.com', N'Dĩ An, Bình Dương', N'0979510945', N'Nam', N'12/05/1999', 1)
INSERT INTO Employee VALUES(N'EMP101', N'Hồ Đức Hiếu', N'hdhieu2610', N'hdhieu@gmail.com', N'Q12, TPHCM', N'0979510946', N'Nam', N'26/10/1999', 1)

--Customer--
INSERT INTO Customer VALUES(N'CUS100', N'Đỗ Thị Quỳnh Như', N'dtqnhu', N'dtqnhu@gmail.com', N'Q12, TPHCM', N'0979510947', N'Nữ', N'26/01/1999')
--Brand--
INSERT INTO Brand VALUES(N'AP', N'Apple', N'Mỹ')
INSERT INTO Brand VALUES(N'SA', N'Samsung', N'Hàn Quốc')
INSERT INTO Brand VALUES(N'OP', N'Oppo', N'Trung Quốc')
INSERT INTO Brand VALUES(N'XI', N'Xiaomi', N'Trung Quốc')
INSERT INTO Brand VALUES(N'NO', N'Nokia', N'Trung Quốc')
INSERT INTO Brand VALUES(N'RE', N'Realme', N'Trung Quốc')
INSERT INTO Brand VALUES(N'AS', N'Asus', N'Đài Loan')
INSERT INTO Brand VALUES(N'HU', N'Huawei', N'Trung Quốc')
INSERT INTO Brand VALUES(N'VI', N'Vivo', N'Trung Quốc')
INSERT INTO Brand VALUES(N'VS', N'Vsmart', N'Việt Nam')
INSERT INTO Brand VALUES(N'BK', N'BKAV', N'Việt Nam')

INSERT INTO Brand VALUES(N'MS', N'Microsoft', N'Mỹ')
INSERT INTO Brand VALUES(N'LE', N'Lenovo', N'Trung Quốc')
INSERT INTO Brand VALUES(N'HP', N'HP', N'Mỹ')
INSERT INTO Brand VALUES(N'DE', N'DELL', N'Mỹ')
INSERT INTO Brand VALUES(N'LG', N'LG', N'Hàn Quốc')
INSERT INTO Brand VALUES(N'AC', N'Acer', N'Đài Loan')
INSERT INTO Brand VALUES(N'MI', N'MSI', N'Đài Loan')


