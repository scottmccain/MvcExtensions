using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace TabTest.Helpers
{
    public static class StringExtensions
    {
        public static bool SafeCastTo<T>(this string from, out T to)
        {
            try
            {
                to = (T) Convert.ChangeType(from, typeof (T));
                return true;
            }
            catch (Exception)
            {
                to = default(T);
                return false;
            }
        }
    }
}