CREATE DATABASE ProductManager
GO

USE ProductManager
GO

CREATE TABLE Product (
    ProductId INT IDENTITY,
    ProductName NVARCHAR(20) NOT NULL,
	ProductSku NCHAR(6) NOT NULL,
	ProductDescription NVARCHAR(100) NOT NULL,
	ProductPicture NVARCHAR(100) NOT NULL,
	ProductPrice NVARCHAR(10) NOT NULL,
	PRIMARY KEY (ProductId),
	UNIQUE (ProductSku)
)
