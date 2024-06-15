use master;
go
if exists (select name from sys.databases where name = 'SalesManagementDb')
begin
	drop database SalesManagementDb;
end;
go
create database SalesManagementDb;
go
use SalesManagementDb;
go

create table Member (
   MemberId int not null,
   Email varchar(100) not null,
   CompanyName varchar(40) not null,
   City varchar(15) not null,
   Country varchar(15) not null,
   [Password] varchar(30) not null,

   primary key (MemberId)
);

create table [Order] (
  OrderId int not null,
  MemberId int not null,
  OrderDate datetime not null default getdate(),
  RequiredDate datetime null,
  ShippedDate datetime null,
  Freight money null,
  
  primary key (OrderId),
  foreign key (MemberId) references dbo.Member(MemberId)
);

create table Category(
  CategoryId int not null,
  CategoryName varchar(100) not null,

  primary key (CategoryId)
);

create table Product (
  ProductId int not null,
  CategoryId int not null,
  ProductName varchar(40) not null,
  [Weight] varchar(20) not null,
  UnitPrice money not null,
  UnitsInStock int not null,

  primary key (ProductId),
  foreign key (CategoryId) references dbo.Category(CategoryId)
);


create table OrderDetail (
  OrderId int not null,
  ProductId int not null,
  UnitPrice money not null,
  Quantity int not null,
  Discount float not null,

  primary key (OrderId, ProductId),
  foreign key (OrderId) references dbo.[Order](OrderId),
  foreign key (ProductId) references dbo.Product(ProductId)
);


insert into dbo.Member(MemberId,Email,CompanyName,City,Country,[Password])
values (1, 'minh@gmail.com', 'My Company', 'TPHCM', 'VN', '12345');