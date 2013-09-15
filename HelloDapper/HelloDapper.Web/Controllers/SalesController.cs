using System;
using System.Web.Mvc;
using HelloDapper.Sales;

namespace HelloDapper.Web.Controllers
{
    public class SalesController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SalesReport(DateTime startDate)
        {
            ViewBag.StartDate = startDate;

            // TODO: Ninject
            var service = new SalesOrderReportService(new SalesOrderReportEfRepository());
            //var service = new SalesOrderReportService(new SalesOrderReportDapperRepository());
            return View(service.GetSalesYtdReportData(startDate));
        }

        /// <summary>
        /// Gets the data for the Sales Report
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns><see cref="JsonResult"/> with Data as IEnumerable of dynamic</returns>
        /// <remarks>This is an example of where the Query helper that returns IEnumerable of dynamic may be useful. This would work if the Web 
        /// project was in process with the service layer. Bit tricky if web and services are on seperate tiers.</remarks>
        [HttpGet]
        public JsonResult GetSalesReport(DateTime startDate)
        {
            var service = new SalesOrderReportService(new SalesOrderReportDapperRepository());
            return new JsonResult {Data = service.GetSalesYtdReportDataDynamic(startDate), JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }
    }
}
