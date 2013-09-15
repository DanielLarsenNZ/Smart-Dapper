namespace HelloDapper.Sales
{
    internal class SalesReportData
    {
        public string Territory { get; set; }
        public string StoreName { get; set; }
        public int Month { get; set; }
        public decimal TotalDue { get; set; }
    }

    public class SalesReportDataPivoted
    {
        public string Territory { get; set; }
        public string StoreName { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
    }
}
