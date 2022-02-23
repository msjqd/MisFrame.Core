create database MisFrameDb

go
 
use MisFrameDb

go

create table Advertisement
(
	id int primary key,
	ImgUrl Varchar(max),
	Title Varchar(max),
	Url Varchar(max),
	Remark Varchar(max),
	CreateDate Datetime
)
