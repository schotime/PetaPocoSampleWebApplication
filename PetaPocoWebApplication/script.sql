drop table budgetperiods
go
create table budgetperiods (
	budgetperiodid int identity,
	description nvarchar(250),
	fromdate datetime,
	todate datetime
)
go

drop table expenses
go
create table expenses (
	expenseid int identity,
	budgetperiodid int,
	description nvarchar(250),
	budgetamount decimal(20,2),
	actualamount decimal(20,2),
	remark nvarchar(200)
)
go

select * from budgetperiods
select * from expenses