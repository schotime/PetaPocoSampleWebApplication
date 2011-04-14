using System;
using System.Reflection;
using System.Text.RegularExpressions;
using PetaPoco;

namespace PetaPocoWebApplication.Infrastructure
{
    public class MyMapper : DefaultMapper
    {
        public override bool MapPropertyToColumn(PropertyInfo pi, ref string columnName, ref bool resultColumn)
        {
            columnName = Regex.Replace(pi.Name, "([A-Z])", "_$1").TrimStart('_');
            return base.MapPropertyToColumn(pi, ref columnName, ref resultColumn);
        }

        public override Func<object, object> GetFromDbConverter(PropertyInfo pi, Type SourceType)
        {
            if (SourceType == typeof(string) && pi.PropertyType == typeof(bool))
            {
                return (x =>
                {
                    if ((string)x == "Y")
                        return true;

                    return false;
                });
            }

            return base.GetFromDbConverter(pi, SourceType);
        }

        public override Func<object, object> GetToDbConverter(Type SourceType)
        {
            if (SourceType == typeof(bool))
                return x => (bool)x ? "Y" : "N";

            return base.GetToDbConverter(SourceType);
        }
    }
}