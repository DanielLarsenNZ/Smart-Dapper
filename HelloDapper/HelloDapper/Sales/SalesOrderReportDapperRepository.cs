using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Dapper;
using HelloDapper.Helpers;
using StackExchange.Profiling;

namespace HelloDapper.Sales
{
    public class SalesOrderReportDapperRepository : ISalesOrderReportRepository
    {
        public IEnumerable<SalesReportDataPivoted> GetSalesYtdReportDataPivoted(DateTime startDate)
        {
            //TODO: Surface paging to the UI
            const int pageNumber = 1;
            const int rowsPerPage = 50;
            
            const string sql = @"
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
                ";
            
            var connection = GetConnection();
            return connection.Query<SalesReportDataPivoted>(sql, new {startDate, endDate = startDate.AddYears(1), pageNumber, rowsPerPage});

            /*
            // This code for Stored Proc.
            const string sql = "Sales.GetSalesYtdReportDataPivoted";
            return connection.Query<SalesReportDataPivoted>(sql, new { startDate, endDate = startDate.AddYears(1), pageNumber, rowsPerPage }, 
                commandType: CommandType.StoredProcedure);
            */
        }

        public IEnumerable<dynamic> GetSalesYtdReportDataDynamic(DateTime startDate)
        {
            //TODO: Surface paging to the UI
            const int pageNumber = 1;
            const int rowsPerPage = 50;

            var connection = GetConnection();
            const string sql = "Sales.GetSalesYtdReportDataPivoted";
            return connection.Query(sql, new { startDate, endDate = startDate.AddYears(1), pageNumber, rowsPerPage }, commandType: CommandType.StoredProcedure);
        }

        private static DbConnection GetConnection()
        {
            var connection = new SqlConnection(ConnectionStringHelper.GetConnectionString());

            // wrap the connection with a profiling connection that tracks timings 
            return new StackExchange.Profiling.Data.ProfiledDbConnection(connection, MiniProfiler.Current);
        }
    }
}
