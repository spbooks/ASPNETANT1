USE [Northwind]
GO

DBCC FREESESSIONCACHE
DBCC FREEPROCCACHE
DBCC FREESYSTEMCACHE('ALL')

CHECKPOINT
DBCC DROPCLEANBUFFERS
GO

SELECT * FROM [Order Details] od 
INNER JOIN Orders o on o.OrderID = od.OrderID
GO

SELECT * FROM [Category Sales for 1997]
GO

SELECT * FROM Invoices
GO

SELECT COUNT(OrderID) OrderCount, ShipPostalCode 
FROM Orders 
GROUP BY ShipPostalCode 
ORDER BY COUNT(OrderID) DESC
GO

EXEC [Ten Most Expensive Products]
GO

SELECT COUNT(OrderID) OrderCount, c.CustomerID, c.ContactName 
FROM Orders o INNER JOIN Customers c ON o.CustomerID = c.CustomerID 
GROUP BY c.CustomerID, c.ContactName ORDER BY COUNT(OrderID) DESC
GO

SELECT LEN(ContactName), ContactName 
FROM Customers ORDER BY LEN(ContactName) DESC
GO

SELECT * FROM [Category Sales for 1997]
GO

SELECT * FROM Invoices
GO

EXEC [Ten Most Expensive Products]
GO