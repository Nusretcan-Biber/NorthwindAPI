
using Northwind.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Businnes.IModelServices
{
    public interface IShipperService
    {
        public int CreateShipper(Shipper shipperModel);
        public int UpdateShipper(Shipper shipperModel);
        public int DeleteShipper(short shipperId);
        public List<Shipper> GetAllShippers();
        public Shipper GetShipperById(short shipperId);

    }
}
