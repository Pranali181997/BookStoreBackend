create table Admin
(
AdminId int not null Identity(1,1) Primary key,
AdminName varchar (225) not null,
AdminMobNumber bigint not null,
AdminEmailId varchar (225) not null,
AdminPassword varchar (225) not null
)
insert into Admin values ('Pranali',852963741,'Pranali@gmail.com','123@Pranali')
select *from Admin
-------------------SPForLogIn--------------------------------
create procedure SPForLogIn
(
@AdminEmailId varchar(225),
@AdminPassword varchar(225)
)
as begin
select * from Admin
select * from Books

Where AdminEmailId=@AdminEmailId and AdminPassword=@AdminPassword;
end
