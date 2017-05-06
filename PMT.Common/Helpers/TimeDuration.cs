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

        public DateTime CurrentDate { get; set; }

        private int weekOfYearNet;
        public int WeekOfYear { get; set; }

        public int MonthOfYear { get; set; }

        CultureInfo currentCulture;

        public TimeDuration()
        {
            CurrentDate = DateTime.UtcNow;
            currentCulture = CultureInfo.CurrentCulture;
            CalculateDates();
        }

        public TimeDuration(DateTime current, CultureInfo currentCulture =null)
        {
            this.CurrentDate = current;
            if (currentCulture == null)
                this.currentCulture = CultureInfo.CurrentCulture;
            else
                this.currentCulture = currentCulture;

            CalculateDates();
            FromDate = CurrentDate.AddDays(-10);
            ToDate = CurrentDate.AddDays(10);

        }

        private void CalculateDates()
        {
            weekOfYearNet = currentCulture.Calendar.GetWeekOfYear(
                CurrentDate,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);

            var day = (int)currentCulture.Calendar.GetDayOfWeek(CurrentDate);
            WeekOfYear = currentCulture.Calendar.GetWeekOfYear(
                CurrentDate.AddDays(4 - (day == 0 ? 7 : day)), 
                CalendarWeekRule.FirstFourDayWeek, 
                DayOfWeek.Monday);

            MonthOfYear = CurrentDate.Month;


        }
    }
}
