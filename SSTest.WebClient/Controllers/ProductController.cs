using SSTest.WebClient.ConsumeService;
using SSTest.WebClient.Models;
using SSTest.WebClient.Models.DataTable;
using SSTest.WebClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using SSTest.WebClient.Helper;
using System.IO;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace SSTest.WebClient.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _iProductService;

        public ProductController()
        {
            _iProductService = new ProductService(); //TODO: implement IOC
        }
        // GET: Product
        public ActionResult Index()
        {
           
            return View();
        }


      
      
        public JsonResult GetProducts(DataTableRequest request)
        {


            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();

            if (string.IsNullOrEmpty(request.Search))
            {
                 products = _iProductService.GetAllProducts();
            }
            else
            {
                 products = _iProductService.GetAllProducts().Where(x => x.Name.Contains(request.Search)); //Search
            }

            var pagedProducts = products.ToList().Skip(request.Start).Take(request.Length); //Pagination

            foreach (var item in pagedProducts) //Get Image from helper
            {
                item.PhotoName = Utilities.GetImage(item.Photo);
              
            }

            TempData["Products"] = products;
            //Prepare response
            DataTableResponse objDataTableResponse = new DataTableResponse();
            objDataTableResponse.draw = request.Draw;
            objDataTableResponse.recordsTotal = products.Count();
            objDataTableResponse.recordsFiltered = products.Count();
            objDataTableResponse.data = pagedProducts.ToArray();
            objDataTableResponse.error = "";

            return Json (objDataTableResponse, JsonRequestBehavior.AllowGet);


        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }



        [HttpGet]
        public ActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productVM, List<HttpPostedFileBase> Image)
        {
            productVM.LastUpdated = System.DateTime.Now;
            byte[] fileData = null;
            if (Image[0] != null)
                using (var binaryReader = new BinaryReader(Image[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Image[0].ContentLength);
                    productVM.Photo = fileData;
                }
            _iProductService.Create(productVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            
            _iProductService.Delete(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

           
            ProductViewModel productVM = new ProductViewModel();

            productVM = _iProductService.GetProductById(id);



            return View("Edit", productVM);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productVM, List<HttpPostedFileBase> Image)
        {
            productVM.LastUpdated = System.DateTime.Now;
            byte[] fileData = null;
            if (Image[0] != null)
                using (var binaryReader = new BinaryReader(Image[0].InputStream))
                {
                    fileData = binaryReader.ReadBytes(Image[0].ContentLength);
                    productVM.Photo = fileData;
                }
            _iProductService.Edit(productVM);

            return RedirectToAction("Index");
        }

        public ActionResult ExportDataToExcel()
        {
            try
            {
                List<ProductViewModel> products = (List<ProductViewModel>)TempData["Products"];
                var grid = new GridView();
                grid.DataSource = products;
                grid.DataBind();               

                Response.ClearContent();
                Response.AddHeader("content-disposition", "attachement; filename=ProductsReport.xls");
                Response.ContentType = "application/excel";
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                grid.RenderControl(htw);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
              //  _logger.Info($"Product list exported as excel file including {products.Count} products.");
                return View("Index", products);
            }
            catch (Exception ex)
            {
               // _logger.Error(ex);
                return View("Error", new { code = -1, message = ex.Message });
            }
        }
    }
}