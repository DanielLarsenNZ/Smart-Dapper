using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloDapper.Sales
{
    public class SalesOrderReportService
    {
        private readonly ISalesOrderReportRepository _salesOrderReportRepository;

        public SalesOrderReportService(ISalesOrderReportRepository salesOrderReportRepository)
        {
            _salesOrderReportRepository = salesOrderReportRepository;
        }

        public IList<SalesReportDataPivoted> GetSalesYtdReportData(DateTime startDate)
        {
            // Check authorisation

            // Validate startDate

            // Call Repo
            return _salesOrderReportRepository.GetSalesYtdReportDataPivoted(startDate).ToList();
        }

        public IList<dynamic> GetSalesYtdReportDataDynamic(DateTime startDate)
        {
            // Check authorisation

            // Validate startDate

            // Call Repo
            return _salesOrderReportRepository.GetSalesYtdReportDataDynamic(startDate).ToList();
        }
    }
}
