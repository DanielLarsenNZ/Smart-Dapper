using System;
using System.Data;
using System.Linq;
using Dapper;

namespace HelloDapper.LegacyContext.Sales
{
    /// <summary>
    /// A service for shipping orders.
    /// </summary>
    /// <remarks>This is a facade for a our legacy bounded context. Along with the interface, this class acts as an anti-corruption layer between our old and new code.</remarks>
    public class OrderShippingService : IOrderShippingService
    {
        /// <summary>
        /// Ship an order to an address
        /// </summary>
        /// <param name="addressId">The Address ID, as obtained from AddressService</param>
        /// <param name="shipMethod">The shipping method</param>
        /// <param name="shipDate">The date that the order is to ship</param>
        /// <param name="dueDate">The date that the order is due to arrive with the customer</param>
        public void ShipToAddress(int addressId, ShipMethod shipMethod, DateTime shipDate, DateTime dueDate)
        {
            // connection is IDbConnection. Can be MySQL, SQLLite, Access, DB2, sybase, Oracle... This opens up a world of opportunity for exposing legacy code and systems to 
            //  your domain.
            var connection = GetConnection();
            
            // note old skool proc name
            const string sql = "dbo.sp_soh_sh_addr_2";
            
            // note horrible proc param names, translated from shiny interface names. 
            // note translation of .NET DateTime to old skool date as string is done here.
            var result = connection.Query(sql, new
                {
                    i_x_add_id = addressId, 
                    i_x_sh_m_id = (int)shipMethod, 
                    dt_sh_dt = shipDate.ToString("dd-MMM-yyyy"), 
                    dt_du_dt = dueDate.ToString("dd-MMM-yyyy")
                }, 
                commandType: CommandType.StoredProcedure).First();

            // note magic return number handling so that this method returns as successful, or throws. Magic numbers are researched and translated (and documented) here.
            switch ((int)result.RESULT_CODE)
            {
                case 0:
                    System.Diagnostics.Trace.WriteLine(sql + " executed OK", "Info");
                    break;
                case -10:
                    throw new InvalidOperationException("Order has already been shipped");
                default:
                    throw new InvalidOperationException("Unknown error in " + sql);
            }
        }

        private IDbConnection GetConnection()
        {
            throw new NotImplementedException();
        }
    }
}