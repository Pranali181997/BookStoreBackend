use BookStoreDB
create table FeedBack
(
FeedBackID int not null identity(1,1) primary key,
UserId int not null Foreign key(UserId) references Users (UserId),
BookId int not null Foreign key(BookId) references Books(BookId),
FeedBack varchar (225)not null,
)
select * from FeedBack
select * from Books
alter table FeedBack add Rating Float not null 
---------------SPForFeedBack----------------------
create procedure SPForAddFeedBack
(
@UserId int,
@BookId int ,
@FeedBack varchar(225),
@Rating float
)
as begin
insert into FeedBack values(@UserId,@BookId,@FeedBack,@Rating) 
end
-------------------------------SPForGetAllFeedback--------------------
create procedure SPForGetAllFeedback
(
@BookId int
)
AS
Begin
select
     f.FeedbackId,
	 f.UserId,
	 f.BookId,
	 f.FeedBack,
	 f.Rating,
	 u.FullName
	 From Users u
	 Inner join Feedback f
	 on f.UserId = U.UserId where BookId =@BookId;
End