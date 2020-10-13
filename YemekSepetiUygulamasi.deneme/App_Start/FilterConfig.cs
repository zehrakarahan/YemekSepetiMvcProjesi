using System.Web;
using System.Web.Mvc;

namespace YemekSepetiUygulamasi.deneme
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
