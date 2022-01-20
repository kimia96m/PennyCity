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
            int month = Convert.ToInt32(persianDate.Substring(5, 2).PadLeft(2, '0'));
            int day = Convert.ToInt32(persianDate.Substring(8, 2).PadLeft(2, '0'));
            DateTime milady = new DateTime(year, month, day, new System.Globalization.PersianCalendar());

            return milady;
        }
        public static DateTime ToMiladi(this string persianDate)
        {
            string[] date = persianDate.Split(" ");
            int year = Convert.ToInt32(changePersianNumbersToEnglish(date[3]));
            int month = Convert.ToInt32(changepesianMonthstoEnglish(date[2]));
            int day = Convert.ToInt32(changePersianNumbersToEnglish(date[1]));
            DateTime milady = new DateTime(year, month, day, new System.Globalization.PersianCalendar());

            return milady;
        }
        public static string changePersianNumbersToEnglish(string input)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }
        public static string changepesianMonthstoEnglish(string input)
        {
            string[] persian = new string[12] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            for (int j = 0; j < persian.Length; j++)
                input = input.Replace(persian[j], j.ToString());

            return input;
        }
    }
}
