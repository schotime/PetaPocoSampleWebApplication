using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace PetaPocoWebApplication.Infrastructure
{
    public class PetaTuningAttribute : ActionFilterAttribute
    {
        public PetaTuningAttribute()
        {
            MaxQueriesForWarning = 20;
        }

        public int MaxQueriesForWarning { get; set; }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.IsDebuggingEnabled)  
                return;

            var list = MyDb.CurrentRequestTimings;
            var sb = new StringBuilder();
            sb.AppendLine("<!--");

            var header = string.Format("Db Timings: {0} database queries in {1:0.#0}ms for this request", list.Count, list.Sum(x => x.Time));
            sb.AppendLine(header);
            sb.AppendLine(new String('-', header.Length));

            ShowWarningsIfAny(sb, list, header.Length);

            foreach (var item in list)
            {
                sb.AppendFormat("[{0:0.#0}ms] - {1}\n\n", item.Time, item.FormattedSql);
            }
            sb.AppendLine("-->");

            filterContext.HttpContext.Response.Write(sb.ToString());
        }

        private void ShowWarningsIfAny(StringBuilder sb, List<PetaTuning> list, int length)
        {
            bool hasWarning = false;
            if (IsPossibleSqlNPlus1(list))
            {
                sb.AppendLine("**** Warning: Duplicate queries detected. Possible Select N+1 ****");
                hasWarning = true;
            }
            if (list.Count > MaxQueriesForWarning)
            {
                sb.AppendLine(string.Format("**** Warning: Greater than {0} queries. Possible performance implications ****", MaxQueriesForWarning));
                hasWarning = true;
            }
            if (hasWarning)
            {
                sb.AppendLine(new String('-', length));
            }
        }

        private static bool IsPossibleSqlNPlus1(IEnumerable<PetaTuning> list)
        {
            return list
                .GroupBy(x => x.Sql)
                .Where(x => x.Count() > 2)
                .Any();
        }
    }
}
