using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSTest.WebClient.Helper
{
    public static class Utilities
    {
        public  static string GetImage(byte[] image)
        {
            string imgSrc = "";
            if (image != null)
            {
                var base64 = Convert.ToBase64String(image, 0, image.Length);
                imgSrc = String.Format("data:image/jpg;base64,{0}", base64);
            }
            else
            {
                imgSrc = "/Content/images/imageNotFound.jpg";
            }
            return imgSrc;
        }
    }
}