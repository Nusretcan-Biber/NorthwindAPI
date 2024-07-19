using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Data.DTOs
{
    public class OrderDto
    {
        public short OrderId { get; set; }

        public string? CustomerId { get; set; }

        public short? EmployeeId { get; set; }

        public string? ShipName { get; set; }

        public string? ShipAddress { get; set; }

        public string? ShipCity { get; set; }

        public string? ShipRegion { get; set; }
    }
}
