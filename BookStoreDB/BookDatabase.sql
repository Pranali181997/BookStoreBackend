create table Books 
(
BookId int Identity(1,1) primary key,
BookName varchar(225) not null,
AuthorName varchar(225) not null,
Description varchar(225) not null,
Book_Quantity Int not null,
Book_Image varchar (225) not null,
Orignal_Price int not null,
Discount_Price int not null,
Rating float not null,
Total_Count_Of_rating int not null
);
select * from Books

insert into Books 
(
BookName,AuthorName,Description,Book_Quantity,Book_Image,Orignal_Price,Discount_Price,Rating,Total_Count_Of_rating
)
values
(
'INDKAVIKAS','vikky','SmartBoy',2,'coverpage',1000,50,9.01,10000
);

drop table Books
----------------------------StoredProcedureForBook-------------------------------
alter procedure spAddBook
(
@BookName varchar (225),
@AuthorName varchar(225),
@Description varchar(225),
@Book_Quantity int ,
@Book_Image varchar,
@Orignal_Price int,
@Discount_Price int,
@Rating float,
@Total_Count_Of_rating int
)
as begin
insert into Books
values
(
@BookName,@AuthorName,@Description,@Book_Quantity,@Book_Image,@Orignal_Price,@Discount_Price,@Rating,@Total_Count_Of_rating
);
end
---------------------------------------updateBook--------------------------------------------------------------
