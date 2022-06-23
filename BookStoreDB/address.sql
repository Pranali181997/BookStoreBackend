Create table AddressType
(
AdTypeId int identity(1,1) primary key,
AddressType Varchar(225)
)

insert into AddressType values
('Home'),
('Office'),
('Other');

select * from AddressType
select * from Address
truncate table Address
---------------------------------
create table Address
(
AddressId int not null Identity(1,1) Primary Key,
UserId int not null Foreign key (UserId) references Users(UserId),
Address varchar (225) not null,
City varchar (225) not null,
state varchar (225) not null,
AdTypeId int not null foreign key (AdTypeId) references AddressType(AdTypeId)
)
-------------------------------SpForAddAdress---------------------
alter procedure SPForAddAddress
(
@Address varchar(225),
@City varchar(225),
@State varchar(225),
@AdTypeId Int,
@UserId Int
)
as begin
insert into Address values(@UserId,@Address,@City,@State,@AdTypeId)
end
------Creating procedure for the Delete-----------

create proc SPDeleteAddress
(
@AddressId int,
@UserId int
)
As
Begin
		delete from Address where AddressId = @AddressId and UserId =@UserId;
end
--------------spForGetAddressByUserId----------------
create proc spForGetAddressByUserId
(

@UserId int
)
As
Begin
		select *from Address where UserId=@UserId;
end
--------------------SPforUpdateAddress--------------------
create procedure SPforUpdateAddress
(
@Address varchar(225),
@AddressId int,
@City varchar(225),
@State varchar (225),
@AdTypeId int,
@UserId int
)
as begin
update Address set adress=@Address,City=@City,State=@State,AdTypeId=@AdTypeId where UserId=@UserId and AddressId=@AddressId
end


alter table Address drop column adress
alter table Address add adress varchar(225) not null default 'Mumbai'
