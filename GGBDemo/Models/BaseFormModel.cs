
using System;
using System.Collections.Generic;
using System.Web;

namespace GGBDemo.Models
{
    public class BaseFormModel
    {
        public string ActionName { get; set; }
        public string Method { get; set; }
        public List<FormElement> FormElements { get; set; }
        public string AccessToken { get; set; }
        public BaseFormModel()
        {
            if (HttpContext.Current.Session["AT"] == null)
            {
                AccessToken = Guid.NewGuid().ToString();
                HttpContext.Current.Session["AT"] = AccessToken;
            }
            else
                AccessToken = HttpContext.Current.Session["AT"].ToString();
        }
    }
}