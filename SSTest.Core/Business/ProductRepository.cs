using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSTest.Core.Interface;
using SSTest.Core.DataAccess;

namespace SSTest.Core.Business
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
       
    }
}
