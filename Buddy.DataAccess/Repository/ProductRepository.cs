using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Buddy.DataAccess.Repository.IRepository;
using Buddy.DataAccess.Data;
using Buddy.Models.Models;


namespace Buddy.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {

        private AppDbContext _db;

        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            var objdb= _db.Products.FirstOrDefault(u=>u.Id==obj.Id);
            if (objdb != null)
            {
                objdb.ISBN=obj.ISBN;
                objdb.Name=obj.Name;
                objdb.Description=obj.Description;
                objdb.Price=obj.Price;
                objdb.Price50=obj.Price50;
                objdb.price100=obj.price100;
                objdb.ListPrice=obj.ListPrice;
                objdb.Author=obj.Author;
                objdb.CategoryId=obj.CategoryId;
                if (obj.Imageurl != null) { 
                      objdb.Imageurl=obj.Imageurl;
                }
            }

            //_db.Products.Update(obj);
        
        }
    }
}
