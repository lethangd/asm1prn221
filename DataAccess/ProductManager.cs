using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductManager
    {
        public static List<Product> getListProductByProductName(string productName)
        {
            List<Product> listP;
            using (var context = new MyStoreContext())
            {
                listP = context.Products
                    .Where(p => p.ProductName == productName)
                    .ToList();
            }
            return listP;
        }


        public static List<Product> getListProductByUnitPrice(int unitPrice)
        {
            List<Product> listP;
            using (var context = new MyStoreContext())
            {
                listP = context.Products
                    .Where(p => p.UnitPrice == unitPrice)
                    .ToList();
            }
            return listP;
        }
    }
}
