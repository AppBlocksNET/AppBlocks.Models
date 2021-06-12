declare @idold NVARCHAR(255);
declare @idnew NVARCHAR(255);
declare @idtemp NVARCHAR(255);

set @idold = '535BC8E3-112E-4D41-AF86-AFDA730259A2'
set @idnew ='Templates'
set @idtemp = 'Temp'

update items set typeid=@idtemp where typeid=@idold
update items set parentid=@idtemp where parentid=@idold
update items set ownerid=@idtemp where ownerid=@idold

update items set id=@idnew where id=@idold

update items set typeid=@idnew where typeid=@idtemp
update items set parentid=@idnew where parentid=@idtemp
update items set ownerid=@idnew where ownerid=@idtemp
