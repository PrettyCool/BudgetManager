using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BudgetManager_V1.Extentions
{
    public static class MyExtensions
    {
        /// <summary>
        /// time is not counted
        /// </summary>
        /// <param name="target"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool DateIsBetween(this DateTime target, DateTime d1, DateTime d2)
        {
            target = new DateTime(target.Year, target.Month, target.Day); 
            d1 = new DateTime(d1.Year, d1.Month, d1.Day); 
            d2 = new DateTime(d2.Year, d2.Month, d2.Day);
            return target >= d1 && target <= d2;
        }
    }
}