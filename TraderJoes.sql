
/* CREATE DATABASE */

CREATE DATABASE TraderJoes;

/* CREATE PRODUCT TABLE */

CREATE TABLE Product(
	ID int IDENTITY PRIMARY KEY,
	Name varchar(100),
	Price float,
	BarCode varchar(25),
	Image varchar(255),
	Category varchar(50),
	Status varchar(60),
	Description varchar(255)
);

INSERT INTO Product
VALUES('Health Ade Kombucha Tropical Punch', 2.49,'0000 0000', '/TraderJoes/HealthAdeKombuchaTropicalPunch.jpg',
'drinks', 'okay', 'Good Summer Drink');

INSERT INTO Product
VALUES('Health Ade Kombucha Pink Lady', 2.49,'0000 0001', '/TraderJoes/HealthAdeKombuchaPinkLady.jpg',
'drinks', 'okay', 'Good Summer Drink');

UPDATE Product
SET Name = 'Health Ade Kombucha Pink Lady Update', Price = 2.49, BarCode = '0000 0000',Image = '/TraderJoes/HealthAdeKombuchaPinkLady.jpg',
Category = 'drinks', Status = 'okay', Description = 'Good Summer Drink'
WHERE ID = 2;
