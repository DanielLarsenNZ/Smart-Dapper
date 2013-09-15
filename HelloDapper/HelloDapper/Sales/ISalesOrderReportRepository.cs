using System;
using System.Collections.Generic;

namespace HelloDapper.Sales
{
    public interface ISalesOrderReportRepository
    {
        IEnumerable<SalesReportDataPivoted> GetSalesYtdReportDataPivoted(DateTime startDate);
        IEnumerable<dynamic> GetSalesYtdReportDataDynamic(DateTime startDate);
    }
}