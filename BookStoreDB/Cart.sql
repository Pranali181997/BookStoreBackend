create table Cart 
(
CartId int Identity (1,1) primary key,
UserId int not null Foreign key (UserId) references Users (UserId),
BookId int not null Foreign key (BookId) references Books (BookId),
Quantity_Of_Book int not null
)
insert into Cart values (7,2,1)

-----------------------SPForAddCart------------------------------
create procedure spAddCart
(
@UserId int,
@BookId int,
@Quantity_Of_Book int
)
as begin 

insert into Cart(UserId,BookId,Quantity_Of_Book)
values (@UserId, @BookId, @Quantity_Of_Book)	
end
--------------------------------------
select *from Users
select *from Cart
select *from Books
truncate table Cart
drop table Cart
------------------spforUpdate---------------
alter proc SP_UpdateQty_InCart
(
@Quantity_Of_Book int,
@cartId int,
@UserId int

)
As
Begin
	update Cart Set Quantity_Of_Book = @Quantity_Of_Book where CartId = @CartId and UserId=@UserId;
end
------------------spForRemove-------------------
	alter procedure SP_RemoveCart
	(
	@UserId int,
	@CartId int
	)
	As
	Begin
		delete from Cart where CartId = @CartId and UserId=@UserId;
	end
---------------------------------spForGetBookFromCart--------------------------------------
	create procedure spForGetBookFromCart(
	@UserId int
	)
	As
	Begin
		select 
		c.CartId,
		c.BookId,
		c.UserId,
		c.Quantity_Of_Book,
		b.BookName,
		b.AuthorName,
		b.Description,		
		b.Book_Image,
		b.Discount_Price,
		b.Rating,
		b.Total_Count_Of_rating
		from Cart c 
		inner join Books b
		on c.BookId=b.BookId
		where c.UserId=@UserId ;
	end