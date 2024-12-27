use master
go
if DB_ID('MyDatabase') is not null
	drop database MyDatabase
go
create database MyDatabase
go
use MyDatabase
go
create table Phongban(
maphong int primary key identity,
tenphong nvarchar(25)
)
insert into Phongban values(N'Tổng hợp')
insert into Phongban values(N'Tài chính')
insert into Phongban values(N'Nghiệp vụ')
go
create table Nhanvien(
manv int primary key identity,
hotennv nvarchar(30) not null,
tuoi int not null,
diachi nvarchar(30),
luongnv int not null,
maphong int foreign key references Phongban(maphong),
matkhau varchar(50) not null)
go
insert into Nhanvien values(N'Thu Hà',24,N'Hà nội',2000,1,'11')
insert into Nhanvien values(N'Tuấn Anh',32,N'Hải phòng',3200,1,'12')
insert into Nhanvien values(N'Thanh Hùng',26,N'Hà nam',2500,1,'13')
insert into Nhanvien values(N'Mai Lan',28,N'Thái bình',2500,1,'14')
insert into Nhanvien values(N'Thùy Hương',27,N'Hà nội',2400,2,'15')
insert into Nhanvien values(N'Ngọc Lan',24,N'Lạng sơn',2200,2,'16')
insert into Nhanvien values(N'Ngọc Sơn',28,N'Thái bình',2700,2,'17')
insert into Nhanvien values(N'Thu Nga',25,N'Hà nội',2800,3,'18')
insert into Nhanvien values(N'Văn Quân',36,N'Hải phòng',4000,3,'19')
insert into Nhanvien values(N'Nhật Minh',45,N'Hải phòng',5000,3,'20')
  
GO
select * from nhanvien