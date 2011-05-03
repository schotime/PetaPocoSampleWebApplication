using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaTimingAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.IsDebuggingEnabled)
                return;

            var list = MyDb.CurrentRequestTimings;
            var header = string.Format("Db Timings: {0} database queries in {1:0.#0}ms for this request", list.Count, list.Sum(x => x.Time));
            
            var sb = new StringBuilder();
            sb.AppendLine("<!--");
            sb.AppendLine(header);
            sb.AppendLine(new String('-', header.Length));
            foreach (var item in list)
            {
                sb.AppendFormat("[{0:0.#0}ms] - {1}\n\n", item.Time, item.Sql);
            }
            sb.AppendLine("-->");

            filterContext.HttpContext.Response.Write(sb.ToString());
        }
    }
}
