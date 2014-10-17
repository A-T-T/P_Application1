using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace P_Application1
{
    public static class Extension
    {
        public static string ToUpperTurkish(this string convertString)
        {
            convertString = convertString.Trim().ToUpper().Replace('S', 'Ş').Replace("C", "Ç").Replace('G', 'Ğ').Replace('U', 'Ü').Replace('I', 'İ').Replace('O', 'Ö').
                Replace(' ', '-').Replace("?", "").Replace(",", "").Replace(".", "").Replace("\"", "").Replace(" ", "").Replace("&", "").Replace("%", "");
            return convertString;
        }

        public static string ToUpperEnglish(this string convertString)
        {
            convertString = convertString.Trim().ToUpper().Replace('Ş', 'S').Replace("Ç", "C").Replace('Ğ', 'G').Replace('Ü', 'U').Replace('İ', 'I').Replace('Ö', 'O').
                Replace(' ', '-').Replace("?", "").Replace(",", "").Replace(".", "").Replace("\"", "").Replace(" ", "").Replace("&", "").Replace("%", "");
            return convertString;
        }

        public static IEnumerable<T> OrderByDynamic<T>(this IEnumerable<T> source, string propertyName, string direction = "Asc")
        {
            if (propertyName.Contains(","))
            {
                var sortName = propertyName.Split(',');
                if (direction == "Asc")
                {
                    return source.OrderBy(x => x.GetType().GetProperty(sortName[0]).GetValue(x, null)).ThenBy(x => x.GetType().GetProperty(sortName[1]).GetValue(x, null));
                }
                else
                {
                    return source.OrderByDescending(x => x.GetType().GetProperty(sortName[0]).GetValue(x, null)).ThenBy(x => x.GetType().GetProperty(sortName[1]).GetValue(x, null));
                }
            }
            else if (direction == "Asc")
                return source.OrderBy(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
            else
                return source.OrderByDescending(x => x.GetType().GetProperty(propertyName).GetValue(x, null));
        }

    }
}