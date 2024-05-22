using BusinessObject;
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
        //get all product names & ids
        public  List<Product> getAllProductName()
        {
            List<Product> listP;
            using (var context = new MyStoreContext())
            {
                listP = context.Products
                    .Select(p => new Product { ProductId = p.ProductId, ProductName = p.ProductName })
                    .ToList();
            }
            return listP;
        }

        //get all string product name
        public List<string> getAllStringProductName()
        {
            List<string> listP;
            using (var context = new MyStoreContext())
            {
                listP = context.Products
                    .Select(p => p.ProductName)
                    .ToList();
            }
            return listP;
        }
        //find product name by id
        public string findProductNameById(int id)
        {
            string name;
            using (var context = new MyStoreContext())
            {
                name = context.Products
                    .Where(p => p.ProductId == id)
                    .Select(p => p.ProductName)
                    .FirstOrDefault();
            }
            return name;
        }




    }
}
