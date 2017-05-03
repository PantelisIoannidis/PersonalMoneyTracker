using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common.Helpers
{
    public class TimeDuration
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime current { get; set; }

        private int weekOfYearNet;
        public int WeekOfYear { get; set; }

        public int MonthOfYear { get; set; }

        CultureInfo currentCulture;

        public TimeDuration()
        {
            current = DateTime.UtcNow;
            currentCulture = CultureInfo.CurrentCulture;
            CalculateDates();
        }

        public TimeDuration(DateTime current, CultureInfo currentCulture =null)
        {
            this.current = current;
            if (currentCulture == null)
                this.currentCulture = CultureInfo.CurrentCulture;
            else
                this.currentCulture = currentCulture;

            CalculateDates();
        }

        private void CalculateDates()
        {
            weekOfYearNet = currentCulture.Calendar.GetWeekOfYear(
                current,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

            var day = (int)currentCulture.Calendar.GetDayOfWeek(current);
            WeekOfYear = currentCulture.Calendar.GetWeekOfYear(
                current.AddDays(4 - (day == 0 ? 7 : day)), 
                CalendarWeekRule.FirstFourDayWeek, 
                DayOfWeek.Monday);

            MonthOfYear = current.Month;


        }
    }
}
