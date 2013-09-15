using System;

namespace HelloDapper.LegacyContext.Sales
{
    /// <summary>
    /// Defines a service for shipping orders.
    /// </summary>
    /// <remarks>This interface exposes a beautiful shiny facade to our consuming code, hiding the horrible-ness of the code inside the context. Note how commens and
    /// good looking parameter names have been added to make this service indistinguishable from other domain services.</remarks>
    public interface IOrderShippingService
    {
        /// <summary>
        /// Ship an order to an address
        /// </summary>
        /// <param name="addressId">The Address ID, as obtained from AddressService</param>
        /// <param name="shipMethod">The shipping method</param>
        /// <param name="shipDate">The date that the order is to ship</param>
        /// <param name="dueDate">The date that the order is due to arrive with the customer</param>
        void ShipToAddress(int addressId, ShipMethod shipMethod, DateTime shipDate, DateTime dueDate);
    }
}