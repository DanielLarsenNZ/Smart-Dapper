if exists (select 1 from sys.procedures where object_id = object_id('Sales.GetSalesYtdReportDataPivoted'))
	drop procedure Sales.GetSalesYtdReportDataPivoted
go

CREATE PROCEDURE Sales.GetSalesYtdReportDataPivoted
	@startDate datetime, @endDate datetime, @PageNumber int, @RowsPerPage int
AS
	-- Purpose: Returns pivoted, aggregated and paged YTD sales data

	-- CTE for paging (in SQL 2008 R2). This is easier in SQL 2012 (see below)
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
	-- this code will pivot the monthly totals
	pivot
	(
		Sum(TotalDue)
		for [Month] in (Jan, Feb, Mar, Apr, May, Jun, Jul, Aug, Sep, Oct, Nov, Dec)
	) as [pivoted]
)
-- now select a page of data. CTE is computed as part of this select, so it is very fast and efficient.
select * from allrows 
where RowNumber between ((@pageNumber - 1) * @rowsPerPage) + 1
and @rowsPerPage * (@pageNumber)


/* 
-- SQL Server 2012 paging FTW
OFFSET (@pageNumber -1 ) * @rowsPerPage ROWS
FETCH NEXT @rowsPerPage ROWS ONLY
*/