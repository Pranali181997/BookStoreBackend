USE [BookStoreDB]
GO

/****** Object:  StoredProcedure [dbo].[SP_Update_Book]    Script Date: 6/19/2022 8:15:55 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[SP_Update_Book]
(
@BookId int,
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
As
Begin
 Update Books
 set BookName = @BookName,
 AuthorName=@AuthorName,
Description=@Description,
Book_Quantity=@Book_Quantity,
Book_Image=@Book_Image,
Orignal_Price=@Orignal_Price,
Discount_Price=@Discount_Price,
Rating=@Rating,
Total_Count_Of_rating=@Total_Count_Of_rating
where BookId = @BookId;
End
GO
-----------------------------deleteBook----------------------------------------
Create proc SP_Delete_Book
(
@BookId int
)
As
Begin
 delete from Books where BookId = @BookId ;
end
-------------------sp for get all notes---------------------
create procedure SP_GetAll_Books
AS
Begin
		Select * from Books;
End
--------------------------------------
select * from Books

------------------sp for get book by bookid----------------
---------------------- creating procedure fro the get book by Id ----

create proc SP_GetBook_ById
(
@BookId int
)
As
Begin
	select * from Books where BookId = @BookId;
End