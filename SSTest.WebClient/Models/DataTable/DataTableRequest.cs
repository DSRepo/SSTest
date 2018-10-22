using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSTest.WebClient.Models.DataTable
{
    public class DataTableRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }

        public DataTableOrder[] Order { get; set; }
        public DataTableColumn[] Columns { get; set; }
        public string Search
        {
            get
            {

                return HttpContext.Current.Request.Form.GetValues("search[value]").FirstOrDefault();

            }
        }
          
        //public DataTableSearch Search { get; set; }
    }
}