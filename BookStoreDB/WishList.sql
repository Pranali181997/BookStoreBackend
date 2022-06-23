Create table WishList
(
WishListId int identity(1,1) primary key,
UserId int not null foreign key (UserId) references	Users(UserId),
BookId int not null foreign key (BookId) references	Books(BookId),
)
--------------------spFor AddWishList-----------------
create procedure SpForAddToWishList
(
@BookId int,
@UserId int
)
As
Begin 
		insert into WishList
		Values (@UserId,@BookId);
end
---------------Creating procedure for the Deletting from wishList ---------------------------------

create proc SP_Remove_FromWishList
(
@WishListId int
)
AS
Begin
		delete from WishList where WishListId = @WishListId;
end

select * from Users;
select * from WishList
select * from Books

------- Creating procedure for the  Get all from WishList ------------------

create proc SP_GetAll_FromWishList
(
@UserId int
) 
As
Begin
	select
		w.WishListId,
		w.BookId,
		w.UserId,
		b.BookName,
		b.Book_Image,
		b.AuthorName,
		b.Description
		from WishList w
		inner join Books b
		on w.BookId = b.BookId
		where w.UserId =@UserId;
end

truncate table wishlist
-------------------------------------------------------------
select * from WishList