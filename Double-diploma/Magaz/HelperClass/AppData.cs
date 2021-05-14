using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magaz
{
    class AppData
    {
        public static shopDBEntities2 context = new shopDBEntities2(); // надо переместить
        public static void AddProduct(Product product) 
        {
            product.idProduct = context.Product.Count();
            context.Product.Add(product);
        }
        public static void AddVendor(Vendor vendor)
        {
            vendor.idVendor = context.Vendor.Count();
            context.Vendor.Add(vendor);
        }
        public static void AddEmployee(Employee employee)
        {
            employee.idEmployee = context.Employee.Count();
            context.Employee.Add(employee);
        }
    }
}
