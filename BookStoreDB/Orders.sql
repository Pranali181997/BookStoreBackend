create table OrderTable
(

OrderId int identity (1,1) primary key,
Order_Date Date Not null,
Books_Qty int not null,
Order_Price float not null,
Actual_Price float Not null,
BookId int not null foreign key (BookId) references Books (BookId),
UserId int not null foreign key (UserId) references Users(UserId),
AddressId int not null foreign key (AddressId) references Address (AddressId)
)
select * from OrderTable
---------------SPForOrderTable---------------
alter procedure AddOrder
(
@UserId int,
@Bookid int,
@AddressId int
)
as
declare @OrderPrice float;
declare @ActualPrice float;
declare @BookQuantity int;
begin 
		if(exists(select * from Address where AddressId=@AddressId))
		
		begin
		select @BookQuantity =Quantity_Of_Book from Cart where BookId=@BookId and UserId=@UserId
		set @OrderPrice=(select Discount_Price from Books where BookId=@BookId)
		set @ActualPrice=(select Orignal_Price from Books where BookId=@BookId)
				if((select Book_Quantity from Books where BookId=@BookId)>=@BookQuantity)
				begin 
						insert into OrderTable values (GETDATE(),@BookQuantity,@OrderPrice*@BookQuantity,@ActualPrice*@BookQuantity,
						@BookId,@UserId,@AddressId)

						update Books set Book_Quantity=Book_Quantity-@BookQuantity where BookId=@BookId

						delete From Cart where BookId=@BookId and UserId=@UserId
				end
				else
				begin 
				select 2 
				end
		end
	end

	exec AddOrder @BookId=1,@UserId=7,@AddressId=2;
--------------------------------------
select * from Cart
select * from Books
select * from Address
select * from OrderTable
truncate table OrderTable
-----------------------------------------------SPForGetOrders--------------------------------------------------
create procedure SpGetorder
(
@UserId int
)
As
Begin
	select b.BookName,
		b.Book_Image,
		b.AuthorName,
		o.Actual_Price,
		o.Order_Price,
		o.Order_Date,
		o.Books_Qty,
		o.BookId,
		o.OrderId,
		o.UserId,
		o.AddressId
	From OrderTable o
	Inner Join Books b
	On o.BookId = b.BookId
	where o.UserId =@UserId;
End
----------------------------------------RemoveOrder------------------------
create procedure SpForRemoveOrder 
(
@OrderId int,
@UserId int
)
as
begin
 delete from OrderTable where UserId=@UserId and OrderId=@OrderId
 end