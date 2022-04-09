
/* CREATE DATABASE */

CREATE DATABASE TraderJoes;

/* CREATE PRODUCT TABLE */

CREATE TABLE Product(
	ID int IDENTITY PRIMARY KEY,
	Name varchar(100),
	BarCode varchar(12),
	Image varchar(255),
	Category varchar(50),
	Status varchar(60),
	Description varchar(255)
);

INSERT INTO Product
VALUES('Health Ade Kombucha Tropical Punch', '0000 0000', '/TraderJoes/HealthAdeKombuchaTropicalPunch.jpg',
'drinks', 'okay', 'Good Summer Drink');

INSERT INTO Product
VALUES('Health Ade Kombucha Pink Lady', '0000 0000', '/TraderJoes/HealthAdeKombuchaPinkLady.jpg',
'drinks', 'okay', 'Good Summer Drink');
