CREATE DATABASE SuperMarket;
GO
USE SuperMarket;
Go

CREATE TABLE WareHouse(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(MAX) NOT NULL,
	DeletedDate DATE
);

CREATE TABLE Product(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name VARCHAR(MAX) NOT NULL,
	Price DECIMAL(18,2),
	Quantity INT,
	ExpiredDate DATE,
	WareHouseId INT REFERENCES WareHouse(Id),
	DeletedDate DATE
)

CREATE INDEX Index_WareHouse_Id ON WareHouse(Id);
CREATE INDEX Index_Product_Id ON Product(Id);

--procedure untuk menampilkan data product
CREATE OR ALTER PROCEDURE GetAllProductWithPaging
    @PageNumber INT,
    @PageSize INT,
	@WareHouseId INT = NULL,
	@Expired BIT = NULL
AS
BEGIN
	SELECT WH.Id AS WareHouseId, WH.Name AS WareHouseName,
       P.Id AS ProductId, P.Name AS ProductName, P.Price, P.Quantity, P.ExpiredDate
	FROM WareHouse AS WH
	JOIN Product AS P ON WH.Id = P.WareHouseId
	 WHERE P.DeletedDate IS NULL
		AND
        (
            @Expired IS NULL
            OR 
            (
                (@Expired = 1 AND P.ExpiredDate < GETDATE())
                OR
                (@Expired = 0 AND P.ExpiredDate >= GETDATE())
            )
        )
		AND (
			@WareHouseId IS NULL
            OR 
            (P.WareHouseId = @WareHouseId)
		)
	ORDER BY P.Id
	OFFSET (@PageNumber - 1) * @PageSize ROWS
	FETCH NEXT @PageSize ROWS ONLY;
END;
--test exec procedure
EXEC GetAllProductWithPaging @PageNumber = 1, @PageSize = 10, @WareHouseId = 2;

--memunculkan data yang expired yang di insert
CREATE TRIGGER ProductInputTrigger
ON Product
AFTER INSERT
AS
BEGIN
    DECLARE @ExpiredProducts TABLE (
        ProductId INT,
        ProductName NVARCHAR(MAX),
        WareHouseId VARCHAR(10),
        WareHouseName VARCHAR(MAX),
        ExpiredDate DATE
    );

    INSERT INTO @ExpiredProducts (ProductId, ProductName, WareHouseId, WareHouseName, ExpiredDate)
    SELECT i.Id, i.Name, wh.Id, wh.Name, i.ExpiredDate
    FROM inserted AS i
    INNER JOIN WareHouse AS wh ON i.WareHouseId = wh.Id
    WHERE i.ExpiredDate <= GETDATE();

    -- Display expired products
    IF EXISTS (SELECT * FROM @ExpiredProducts)
    BEGIN
        PRINT 'Expired products:';
        SELECT * FROM @ExpiredProducts;
    END
END;


--Dummy WareHouse
INSERT INTO WareHouse (Name)
VALUES ('Warehouse 1'),
       ('Warehouse 2'),
       ('Warehouse 3');
--Dummy Products

update WareHouse
set DeletedDate = null;

INSERT INTO Product (Name, Price, Quantity, ExpiredDate, WareHouseId)
VALUES 
    ('Product A (Expired)', 10.99, 100, '2023-12-31', 1), -- Expired
    ('Product B (Expired)', 5.49, 50, '2023-11-15', 2),  -- Expired
    ('Product C (Expired)', 20.99, 75, '2023-10-20', 1), -- Expired
    ('Product D', 15.99, 120, '2024-08-10', 3),           -- Not expired
    ('Product E', 8.99, 80, '2024-04-25', 2);             -- Not expired










