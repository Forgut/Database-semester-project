CREATE TRIGGER [customer cannot complete order without address]
	ON [dbo].[Orders]
	FOR DELETE, UPDATE
	AS
	BEGIN
	declare @address varchar(50)
		set @address = (select Customers.Address 
		from Customers join Orders on Customers.Id = Orders.CustomerID join inserted on Orders.Id = inserted.Id)
	if @address is null 
	begin 
	rollback
	raiserror('Customer cannot complete order without address!',16,1)
	end

	END