create database QuanLyQuanAn
go

use QuanLyQuanAn
go

--Món ăn(MONAN)
--Bàn(BAN)
--Loại món ăn(LOAIMONAN)
--Tài khoản(TAIKHOAN)
--Hóa đơn(HOADON)
--Hóa đơn 1 món ăn (HOADONinfor)

create table BAN
(
	id int identity primary key, -- identity : tự dộng tăng
	name nvarchar(100) not null default N'Bàn chưa đánh số',
	trangthai nvarchar(100) default N'Trống' -- trống || có người
)
go

create table TAIKHOAN
(
	tendangnhap nvarchar(100) primary key,
	tenhienthi nvarchar(100) not null default N'Lê Chí Công (chủ quán)', 
	matkhau nvarchar(100) not null default 0,
	loaitaikhoa int not null default 0 -- chủ quán : 1 || nhân viên : 0
)
go

create table LOAIMONAN
(
	id int identity primary key not null, 
	name nvarchar(100) not null default N'Chưa đặt tên',
)
go

create table MONAN
(
	id int identity primary key not null, 
	name nvarchar(100) not null default N'Chưa đặt tên',
	idLoaiMonAn int not null,
	gia float not null default 0

	foreign key (idLoaiMonAn) references LOAIMONAN(id)
)
go

create table HOADON
(
	id int identity primary key not null,
	ngayCheckIn date not null default getdate(),--mặc định là hôm nay
	ngayCheckOut date,
	idBan int not null,
	trangthai int not null default 0 -- 1 là đã thanh toán

	foreign key(idBan) references BAN(id)
)
go

create table HOADONinfor
(
	id int identity primary key not null,
	idHoaDon int not null,
	idMonAn int not null,
	soluong int not null default 0--mặc định là 0

	foreign key(idHoaDon) references HOADON(id),
	foreign key(idMonAn) references MONAN(id)
)
go

insert into dbo.TAIKHOAN
values (N'KingAthur', -- tên đăng nhập
		N'Công Lê', -- tên hiển thị
		N'12345', -- mật khẩu
		1 -- loại tài khoản (chủ quán : 1 || nhân viên : 0)
		)
insert into dbo.TAIKHOAN
values (N'NV1', -- tên đăng nhập
		N'Nhân Viên 1', -- tên hiển thị
		N'12345', -- mật khẩu
		0 -- loại tài khoản (chủ quán : 1 || nhân viên : 0)
		)
insert into dbo.TAIKHOAN
values (N'NV2', -- tên đăng nhập
		N'Nhân Viên 2', -- tên hiển thị
		N'12345', -- mật khẩu
		0 -- loại tài khoản (chủ quán : 1 || nhân viên : 0)
		)
go

create proc USP_GetAccountByUserName
@userName nvarchar(100)
as
begin
	select * from TAIKHOAN where tendangnhap = @userName
end
go

create proc USP_Login
@userName nvarchar(100), @pass nvarchar(100)
as
begin
	select * from TAIKHOAN where tendangnhap = @userName and matkhau = @pass
end
go

declare @i int = 1

while @i <= 12
begin
	insert into BAN values (N'Bàn ' + CAST(@i as nvarchar(100)), N'Trống')
	set @i = @i + 1
end
go

create proc USP_GetTableList
as select * from BAN
go

alter proc USP_InsertBill
@idTable int
as
begin
	insert into HOADON
	values (GETDATE(), --ngày checkin
			null, -- ngày checkout
			@idTable, -- id bàn
			0, -- trạng thái
			0, -- giảm giá			
			
			)
end
go

create proc USP_InsertBillInfo
@idBill int, @idFood int, @count int
as
begin
	declare @isExitsBillInfo int;
	declare @foodCount int = 1;

	select @isExitsBillInfo = id, @foodCount = soluong 
	from HOADONinfor
	where idHoaDon = @idBill and idMonAn = @idFood
	
	if (@isExitsBillInfo > 0)
	begin
		declare @newCount int = @foodCount + @count;
		if (@newCount > 0)
			update HOADONinfor set soluong = @foodCount + @count where idMonAn = @idFood
		else
			delete HOADONinfor where idHoaDon = @idBill and idMonAn = @idFood
	end
	else
		insert into HOADONinfor
		values (@idBill, --id hóa đơn
				@idFood, -- id món ăn
				@count -- số lượng
				)
end
go

--thêm loại món ăn
insert into LOAIMONAN 
values ( N'Salad' ) -- tên loại món ăn - nvarchar(100) -- id = 1
insert into LOAIMONAN 
values ( N'Fruit' ) -- tên loại món ăn - nvarchar(100) -- id = 2
insert into LOAIMONAN 
values ( N'Ốc' ) -- tên loại món ăn - nvarchar(100) -- id = 3
insert into LOAIMONAN 
values ( N'Cơm văn phòng' ) -- tên loại món ăn - nvarchar(100) -- id = 4
insert into LOAIMONAN 
values ( N'Mỳ Spaghetti' ) -- tên loại món ăn - nvarchar(100) -- id = 5
insert into LOAIMONAN 
values ( N'Lẩu Thái' ) -- tên loại món ăn - nvarchar(100) -- id = 6
insert into LOAIMONAN 
values ( N'Nước' ) -- tên loại món ăn - nvarchar(100) -- id = 7

--thêm món ăn
--id = 1 -> salad
insert into MONAN 
values (N'Nộm bò khô', -- tên món ăn
		1, -- id Loại món ăn
		30000) -- giá

-- id = 2 -> Fruit
insert into MONAN 
values (N'Khoai tây lốc xoáy', -- tên món ăn
		2, -- id Loại món ăn
		15000) -- giá

-- id = 3 -> Ốc
insert into MONAN 
values (N'Ốc bé', -- tên món ăn
		3, -- id Loại món ăn
		20000) -- giá

-- id = 4 -> Cơm văn phòng
insert into MONAN 
values (N'Cơm gà chiên giòn', -- tên món ăn
		4, -- id Loại món ăn
		40000) -- giá

-- id = 5 -> Mỳ Spaghetti
insert into MONAN 
values (N'Mỳ Spaghetti bò', -- tên món ăn
		5, -- id Loại món ăn
		35000) -- giá

-- id = 6 -> Lẩu Thái
insert into MONAN 
values (N'Lẩu hải sản', -- tên món ăn
		6, -- id Loại món ăn
		220000) -- giá

-- id = 7 -> Nước
insert into MONAN 
values (N'Bia', -- tên món ăn
		7, -- id Loại món ăn
		15000) -- giá

-- thêm hóa đơn
insert into HOADON
values (GETDATE(), -- ngày checkin - date
		null, -- ngày checkout - date
		2, -- id bàn
		0 -- trạng thái (1 -> đã thanh toán)
		)

-- thêm thông tin hóa đơn
insert into HOADONinfor
values (1, -- id hóa đơn
		2, -- id bàn
		2 -- số lượng
		)

select * from HOADON
select * from HOADONinfor
select * from MONAN
select * from LOAIMONAN

select f.name, bi.soluong, f.gia, f.gia*bi.soluong as tongGia from HOADONinfor as bi, HOADON as b, MONAN as f
where bi.idHoaDon = b.id and bi.idMonAn = f.id and b.idBan = 4
go

alter trigger UTG_UpdateBillInfo
on HOADONinfor for insert, update
as
begin
	declare @idBill int
	
	select @idBill = idHoaDon from inserted

	declare @idTable int

	select @idTable = idBan from HOADON where id = @idBill and trangthai = 0

	declare @count int
	select @count = count(*) from HOADONinfor where idHoaDon = @idBill

	if (@count > 0)
	begin
		update BAN set trangthai = N'Có người' where id = @idTable
	end
	else
	update BAN set trangthai = N'Trống' where id = @idTable
end
go



alter trigger UTG_UpdateBill
on HOADON for update
as
begin
	declare @idBill int

	select @idBill = id from inserted

	declare @idTable int

	select @idTable = idBan from HOADON where id = @idBill 

	declare @count int = 0

	select @count = count(*) from HOADON where idBan = @idTable and trangthai = 0

	if (@count = 0)
		update BAN set trangthai = N'Trống' where id = @idTable
end
go

--thêm 1 trường giảm giá vào hóa đơn
alter table HOADON
add discount int
go

--cập nhật trường giảm giá = 0
update HOADON set discount = 0;

select * from HOADON
go

alter proc USP_SwitchTable
@idTable1 int, @idTable2 int
as begin
	declare @id1Bill int
	declare @id2Bill int

	declare @is1TableEmpty int = 1
	declare @is2TableEmpty int = 1

	select @id2Bill = id from HOADON where idBan = @idTable2 and trangthai = 0
	select @id1Bill = id from HOADON where idBan = @idTable1 and trangthai = 0

	if (@id1Bill is null)
	begin
		insert into HOADON values
		(getdate(),
		null,
		@idTable1,
		0, 
		0
		)
		select @id1Bill = max(id) from HOADON where idBan = @idTable1 and trangthai = 0

	end

	select @is1TableEmpty = count(*) from HOADONinfor where idHoaDon = @id1Bill

	if (@id2Bill is null)
	begin
		insert into HOADON values
		(getdate(),
		null,
		@idTable2,
		0,
		0
		)
		select @id2Bill = max(id) from HOADON where idBan = @idTable2 and trangthai = 0

	end

	select @is2TableEmpty = count(*) from HOADONinfor where idHoaDon = @id2Bill

	select id into IDBillInfoTable from HOADONinfor where idHoaDon = @id2Bill

	update HOADONinfor set idHoaDon = @id2Bill where idHoaDon = @id1Bill

	update HOADONinfor set idHoaDon = @id1Bill where id in (select * from IDBillInfoTable)

	drop TABLE IDBillInfoTable

	if (@is1TableEmpty = 0)
		update BAN set trangthai = N'Trống' where id = @idTable2
	if (@is2TableEmpty = 0)
		update BAN set trangthai = N'Trống' where id = @idTable1
end
go

alter table HOADON drop column tongTien 
delete HOADON

select * from HOADON
update HOADON set ngayCheckOut = getdate(), trangthai = 1 , discount = 10 , tongTien = 100000 where id = 10;
