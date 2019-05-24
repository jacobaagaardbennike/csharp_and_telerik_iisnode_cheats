using System;
using System.Collections.Generic;
using System.Linq;


namespace Service
{
    public class CalenderService
    {
     
        public bool DateIsAHoliday(DateTime dateToCheck)
        {
            bool result = false;
            DateTime easterSunday = CalculateEasterSunday(DateTime.Now.Year);

            //nytårsdag og jul
            if (CheckIfChristmas(dateToCheck))
                result = true;

            //påske torsdag, fredag og mandag
            if (dateToCheck.Equals(easterSunday.AddDays(-3)) || dateToCheck.Equals(easterSunday.AddDays(-2)) || dateToCheck.Equals(easterSunday.AddDays(1)))
                result = true;

            //store bededag
            if (dateToCheck.Equals(easterSunday.AddDays(5 + 3 * 7)))
                result = true;

            //store bededag
            if (dateToCheck.Equals(easterSunday.AddDays(26)))
                result = true;

            //kristig himmelfartsdag
            if (dateToCheck.Equals(easterSunday.AddDays(39)))
                result = true;

            //pinsedag
            if (dateToCheck.Equals(easterSunday.AddDays(50)))
                result = true;

            return result;
        }
        
        private DateTime CalculateEasterSunday(int year)
        {
            int a = year % 19;
            int b = year / 100;
            int c = year % 100;
            int d = b / 4;
            int e = b % 4;
            int f = (b + 8) / 25;
            int g = (b - f + 1) / 3;
            int h = (19 * a + b - d - g + 15) % 30;
            int i = c / 4;
            int k = c % 4;
            int l = (32 + 2 * e + 2 * i - h - k) % 7;
            int m = (a + 11 * h + 22 * l) / 451;
            int month = (h + l - 7 * m + 114) / 31;
            int day = ((h + l - 7 * m + 114) % 31) + 1;
            return new DateTime(year, month, day).Date;
        }
        
        private bool CheckIfChristmas(DateTime dateToCheck)
        {
            bool result = false;

            DateTime start = new DateTime(DateTime.Now.Year, 12, 23);
            DateTime end = new DateTime(DateTime.Now.AddYears(1).Year, 1, 2);

            if (dateToCheck >= start && dateToCheck <= end)
                result = true;

            return result;
        }
    }
}
