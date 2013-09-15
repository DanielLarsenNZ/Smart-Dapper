using System;
using System.Collections.Generic;
using System.Linq;

namespace HelloDapper.Sales
{
    public class SalesOrderReportEfRepository : ISalesOrderReportRepository
    {
        /// <summary>
        /// Private helper to retrieve the un-pivoted data
        /// </summary>
        /// <param name="startDate"></param>
        /// <returns></returns>
        private IEnumerable<SalesReportData> GetSalesYtdReportData(DateTime startDate)
        {
            var endDate = startDate.AddYears(1);

            var entities = new Data.AdventureWorksData();
            
            // Linq expression to get monthly sales totals grouped by Territory and Store, but not pivoted. Pivoting is very difficult (near impossible) 
            //  in Linq to Entities, without jumping through some major hoops. NH is not much better.
            var query = from o in entities.SalesOrderHeaders
                        where o.OrderDate > startDate && o.OrderDate <= endDate
                              && o.Customer.Store != null
                        group o by new { Territory = o.SalesTerritory.Name, StoreName = o.Customer.Store.Name, o.OrderDate.Month} into g
                        select new SalesReportData
                            {
                                Territory = g.Key.Territory,
                                StoreName = g.Key.StoreName,
                                Month = g.Key.Month,
                                TotalDue = g.Sum(orders=>orders.TotalDue),
                            };

            return query.AsEnumerable();
        }

        public IEnumerable<SalesReportDataPivoted> GetSalesYtdReportDataPivoted(DateTime startDate)
        {
            var data = GetSalesYtdReportData(startDate).ToList();
            var pivotedData = new List<SalesReportDataPivoted>();

            // this code is pivoting in memory. This is for demonstration purposes only. This would not perform or scale well on any reasonably large or busy system.
            foreach (var row in data.GroupBy(d => new {d.Territory, d.StoreName}))
            {
                pivotedData.Add(new SalesReportDataPivoted
                    {
                        StoreName = row.Key.StoreName,
                        Territory = row.Key.Territory,
                        Jan = data.Where(d => d.Month == 1 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Feb = data.Where(d => d.Month == 2 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Mar = data.Where(d => d.Month == 3 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Apr = data.Where(d => d.Month == 4 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        May = data.Where(d => d.Month == 5 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Jun = data.Where(d => d.Month == 6 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Jul = data.Where(d => d.Month == 7 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Aug = data.Where(d => d.Month == 8 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Sep = data.Where(d => d.Month == 9 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Oct = data.Where(d => d.Month == 10 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Nov = data.Where(d => d.Month == 11 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue),
                        Dec = data.Where(d => d.Month == 12 && d.StoreName == row.Key.StoreName && d.Territory == row.Key.Territory).Sum(r => r.TotalDue)
                    });
            }

            return pivotedData;
        }

        public IEnumerable<dynamic> GetSalesYtdReportDataDynamic(DateTime startDate)
        {
            throw new NotImplementedException();
        }
    }
}
