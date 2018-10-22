using SSTest.WebClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using SSTest.WebClient.Helper;
using SSTest.WebClient.ViewModel;

namespace SSTest.WebClient.ConsumeService
{
    public class ProductService :IProductService
    {
        

        public IEnumerable<ProductViewModel> GetAllProducts()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.API_BASE_URI);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync("api/products").Result;
                    if (response.IsSuccessStatusCode)
                        return response.Content.ReadAsAsync<IEnumerable<ProductViewModel>>().Result;

                    return null;
                }
            }
            catch (Exception)
            {
                return null; ;


            }
        }

        public ProductViewModel GetProductById(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.API_BASE_URI);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.GetAsync("api/products/" + id).Result;
                    if (response.IsSuccessStatusCode)
                        return response.Content.ReadAsAsync<ProductViewModel>().Result;

                    return null;
                }
            }
            catch (Exception)
            {
                return null; ;


            }
        }


        public bool Create(ProductViewModel product)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.API_BASE_URI);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PostAsJsonAsync("api/products", product).Result;
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception)
            {
                return false;


            }
        }

        public bool Edit(ProductViewModel product)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.API_BASE_URI);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.PutAsJsonAsync("api/products/" + product.Id, product).Result;
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception)
            {
                return false;


            }
        }

        public bool Delete(int id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Constants.API_BASE_URI);
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = client.DeleteAsync("api/products/" + id).Result;
                    return response.IsSuccessStatusCode;
                }

            }
            catch (Exception)
            {
                return false;


            }
        }
    }
}