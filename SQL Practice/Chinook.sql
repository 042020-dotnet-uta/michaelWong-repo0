--1
SELECT CustomerId, FirstName, LastName, Country
FROM Customer
WHERE Country != 'USA';
--2
SELECT CustomerId, FirstName, LastName, Country
FROM Customer
WHERE Country = 'Brazil';
--3
SELECT *
FROM Employee
WHERE Title LIKE '%Sales%Agent%';
--4
SELECT DISTINCT BillingCountry
FROM Invoice;
--5
SELECT COUNT(InvoiceId) AS Invoices2009, SUM(Total) AS TotalAmount
FROM Invoice
WHERE YEAR(InvoiceDate) = 2009;
--6
SELECT Quantity
FROM InvoiceLine
WHERE InvoiceLineId = 37;
--7
SELECT BillingCountry, COUNT(InvoiceId) AS TotalInvoices
FROM Invoice
GROUP BY BillingCountry;
--8
SELECT BillingCountry, Sum(Total) AS TotalSales
FROM Invoice
GROUP BY BillingCountry
ORDER BY TotalSales DESC;