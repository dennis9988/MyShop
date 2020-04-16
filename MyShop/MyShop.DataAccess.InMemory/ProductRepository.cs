using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductRepository
    {
        //create a cache and call it cache
        ObjectCache cache = MemoryCache.Default;
        //create a list of type Product, and call it products
        List<Product> products;

        //constructor
        public ProductRepository()
        {
            //if products does not exist, create it
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>();
            }
        }
        //i think this is a method, putting products into the cache
        public void Commit()
        {
            cache["products"] = products;

        }
        //add p to products, p is passed in
        public void Insert(Product p)
        {
            products.Add(p);

        }
        //you can hover over shit
        //update function, taking in product of type Product, remeber Product is a class with a bunch of shit in it
        public void Update(Product product)
        {
            //reads like find p where p.ID equals product.Id
            Product productToUpdate = products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        //another method, this time to find shit.  
        public Product Find(string Id)
        {
            Product product = products.Find(p => p.Id == Id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        //i think this is an interface
        public IQueryable<Product> Collection()
        {
            return products.AsQueryable();
        }
        //delete function
        public void Delete(string Id)
        {
            Product productToDelete = products.Find(p => p.Id == Id);

            if (productToDelete != null)
            {
                products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }
    }
}
