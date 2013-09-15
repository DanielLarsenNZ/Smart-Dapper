using System;
using System.Diagnostics;
using System.Linq;
using HelloDapper.Sales;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloDapper.Tests.Sales
{
    [TestClass]
    public class SalesOrderReportServiceIntegrationTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void GetSalesYtdReportData_ValidDate_DoesNotThrow()
        {
            var service = new SalesOrderReportService(new SalesOrderReportDapperRepository());

            using (new TimingsHelper())
            {
                var result = service.GetSalesYtdReportData(DateTime.Now);
                Debug.Print(result.Count() + " records returned");
            }
        }
    }
}
