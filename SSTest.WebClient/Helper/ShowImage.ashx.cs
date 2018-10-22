using SSTest.WebClient.ConsumeService;
using SSTest.WebClient.ViewModel;
using SSTest.WebClient.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SSTest.WebClient.Helper
{
    /// <summary>
    /// Summary description for ShowImage
    /// </summary>
    public class ShowImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Int32 ProductId;
            if (context.Request.QueryString["id"] != null)
            {
                ProductId = Convert.ToInt32(context.Request.QueryString["id"]);
            }
            else
            {
                throw new ArgumentException("No parameter specified");
            }
            context.Response.ContentType = "image/jpeg";
            Stream strm = GetImage(ProductId);
            byte[] buffer = new byte[4096];
            int byteSeq = strm.Read(buffer, 0, 4096);

            while (byteSeq > 0)
            {
                context.Response.OutputStream.Write(buffer, 0, byteSeq);
                byteSeq = strm.Read(buffer, 0, 4096);
            }
        }

        private Stream GetImage(int productId)
        {
             
            IProductService _iProductService = new ProductService();
            ProductViewModel pvm = new ProductViewModel();

            pvm = _iProductService.GetProductById(productId);
            return new MemoryStream((byte[])pvm.Photo);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}