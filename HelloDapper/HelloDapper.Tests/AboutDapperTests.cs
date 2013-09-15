using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using Dapper;
using HelloDapper.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloDapper.Tests
{
    [TestClass]
    public class AboutDapperTests
    {
        [TestMethod]
        [TestCategory("Integration")]
        public void QueryOfT_SimpleModel_DoesNotThrow()
        {
            var connection = new SqlConnection(ConnectionStringHelper.GetConnectionString());

            using (var timer = new TimingsHelper())
            {
                var orders = connection.Query<SalesReportDataDemo>("select * from Sales.SalesOrderHeader where TotalDue > @minTotalDue", 
                    new {minTotalDue = 100}).ToList();
                timer.Split("Query");
                Debug.Print("orders.Count() = " + orders.Count());
                timer.Split("Count");
                Debug.Print(orders.First().ToString());
                timer.Split("First");
                orders = orders.OrderByDescending(o => o.SalesOrderId).ToList();
                Debug.Print(orders.First().ToString());
                timer.Split("Order by desc");
                orders = orders.OrderBy(o => o.SalesOrderId).ToList();
                Debug.Print(orders.First().ToString());
                timer.Split("Order by asc");
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Query_SimpleQuery_DoesNotThrow()
        {
            var connection = new SqlConnection(ConnectionStringHelper.GetConnectionString());

            using (new TimingsHelper())
            {
                var orders = connection.Query("select SalesOrderId, TotalDue, Comment from Sales.SalesOrderHeader where TotalDue > @minTotalDue", 
                    new { minTotalDue = 100 }).ToList();
                Debug.Print("orders.Count() = " + orders.Count());
                var listOfOrders = orders.ToList();
                int orderId = listOfOrders[0].SalesOrderId;
                string comment = listOfOrders[0].Comment;
                Debug.Print(orderId.ToString() + ", " + comment);
            }
        }

        [TestMethod]
        [TestCategory("Integration")]
        public void Execute_InsertAndRollback_DoesNotThrow()
        {
            var connection = new SqlConnection(ConnectionStringHelper.GetConnectionString());

            using (new TimingsHelper())
            {
                connection.Execute("update Sales.SalesOrderHeader set Comment = @comment where SalesOrderId = @id", new {comment = "Hello Dapper", id = 75123});
            }
        }
    }

    class SalesReportDataDemo
    {
        public int SalesOrderId { get; set; }
        public int TotalDue { get; set; }
        public string Comment { get; set; }

        public override string ToString()
        {
            return string.Format("SalesOrderId = {0}, TotalDue = {1}, Comment = \"{2}\"", SalesOrderId, TotalDue, Comment);
        }
    }
}
