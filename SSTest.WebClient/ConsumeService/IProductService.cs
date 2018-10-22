using SSTest.WebClient.Models;
using SSTest.WebClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSTest.WebClient.ConsumeService
{
   public interface IProductService
    {
         IEnumerable<ProductViewModel> GetAllProducts();
        ProductViewModel GetProductById(int Id);
        bool Create(ProductViewModel product);
        bool Edit(ProductViewModel product);
        bool Delete(int Id);
    }
}
