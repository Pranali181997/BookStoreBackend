create database BookStoreDB

use BookStoreDB

create table Users
(
UserID int identity(1,1) primary key,
FullName varchar(225),
EmailId varchar(225),
Password varchar (225),
MobileNumber bigint
)
insert into Users(FullName,EmailId,Password,MobileNumber)
values ('parul','parul@gmail.com','123@parul',8528528528);

select * from Users

create procedure spAddUser
(
@FullName varchar(225),
@EmailId varchar(225),
@Password varchar (225),
@MobileNumber bigint
)
as 
begin

insert into Users(FullName,EmailId,Password,MobileNumber)
values (@FullName, @EmailId, @Password, @MobileNumber);
end

exec spAddUser @FullName='Pranali', @EmailId='pranali@gmail.com', @Password='123@Pranali', @MobileNumber=8956598464;

----------------------spForUserLogIn--------------------------------------------
alter procedure spLoginUser(
@EmailId varchar(225),
@Password varchar(225)
)
as begin
select * From Users 
where EmailId=@EmailId and Password=@Password;
end
----------------------------------ForgetPassword--------------------------------------------------
create procedure spForgetPassword
(
@EmailId varchar(225)
)
as begin
select * From Users 
where EmailId=@EmailId;
end
----------------------------------ResetPaasword---------------------------------------
create procedure spResetPassword
(
@EmailId varchar(225),
@Password varchar(225)
)
as begin
update  Users set Password= @Password where EmailId=@EmailId
end
-------------------------------------------------------------------------------
