using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication2.InfraStructure
{
    public static class DateConverter
    {
        public static string persiandate(this PersianCalendar persian, DateTime date)
        {
            TimeSpan time = date.ToLocalTime() - date;
            DateTime thistime = date.AddMinutes(time.TotalMinutes);
            var result = $"{persian.GetHour(thistime).ToString().PadLeft(2, '0')}:{persian.GetMinute(thistime).ToString().PadLeft(2, '0')} , {persian.GetYear(thistime)}/{persian.GetMonth(thistime).ToString().PadLeft(2, '0')}/{persian.GetDayOfMonth(thistime).ToString().PadLeft(2, '0')}";

            //var Result =$"{ persian.GetHour(thistime).ToString().PadLeft(2, '0')} : { persian.GetMinute(thistime).ToString().PadLeft(2, '0')} ,{ persian.GetYear(thistime)} / { persian.GetMonth(thistime).ToString().PadLeft(2, '0')} /{ persian.GetDayOfMonth(thistime).ToString().PadLeft(2, '0')} ";



            return result;

        }
        public static DateTime ToMilady(this string persianDate)
        {

            int year = Convert.ToInt32(persianDate.Substring(0, 4));
            int month = Convert.ToInt32(persianDate.Substring(4, 2).PadLeft(2, '0'));
            int day = Convert.ToInt32(persianDate.Substring(6, 2).PadLeft(2, '0'));
            DateTime milady = new DateTime(year, month, day, new System.Globalization.PersianCalendar());

            return milady;
        }
    }
}
