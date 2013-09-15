-- http://www.codeproject.com/Tips/500811/Simple-Way-To-Use-Pivot-In-SQL-Query
-- http://blog.sqlauthority.com/2013/04/14/sql-server-tricks-for-row-offset-and-paging-in-various-versions-of-sql-server/

-- this block for testing in the query window
declare @startDate datetime, @endDate datetime, @PageNumber int, @RowsPerPage int;
set @startDate = convert(datetime, '01-Jan-2006', 113);
set @endDate = convert(datetime, '01-Jan-2007', 113);
set @PageNumber = 1;
set @RowsPerPage = 50;
-- /

with allrows as (
	select *, ROW_NUMBER() OVER (ORDER BY Territory, StoreName) AS RowNumber from (
		select 
			max(st.Name) as Territory,
			max(s.Name)  as StoreName,
			sum(soh.TotalDue) as TotalDue,
			left(datename(month, OrderDate), 3) as [Month]
		
		from
			Sales.SalesOrderHeader soh
			join Sales.SalesTerritory st on soh.TerritoryID = st.TerritoryID
			join Sales.Customer c on soh.CustomerID = c.CustomerID
			join Sales.Store s on c.StoreID = s.BusinessEntityID
		
		where soh.OrderDate > @startDate and
			  soh.OrderDate <= @endDate

		group by st.TerritoryID, s.BusinessEntityID, datename(month, OrderDate)
	) as normal 
	pivot
	(
		Sum(TotalDue)
		for [Month] in (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec)
	) as [pivoted]
)
select * from allrows 
where RowNumber between ((@pageNumber - 1) * @rowsPerPage) + 1
and @rowsPerPage * (@pageNumber)


/* 
-- SQL Server 2012 FTW
OFFSET (@pageNumber -1 ) * @rowsPerPage ROWS
FETCH NEXT @rowsPerPage ROWS ONLY
*/