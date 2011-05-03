using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using StructureMap;

namespace PetaPocoWebApplication.Infrastructure
{
    public static class PetaTimingHelper
    {
        public static string ShowPetaTimings(this HtmlHelper htmlHelper)
        {
            var list = MyDb.CurrentRequestTimings;
            
            var sb = new StringBuilder();
            sb.AppendFormat("<!--\nTimings: {0} database queries in {1}ms for this request\n", list.Count, list.Sum(x=>x.Time));
            foreach(var item in list)
            {
                sb.AppendFormat("[{0}ms] - {1}\n", item.Time, item.Sql);
            }
            sb.Append("-->");

            return sb.ToString();
        }
    }
}
