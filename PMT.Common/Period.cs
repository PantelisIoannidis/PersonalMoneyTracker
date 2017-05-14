using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common.Helpers
{
    public enum PeriodType
    {
        Week =0,
        Month=1,
        Year=2,
        All=3
    }

    public class Period
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTime SelectedDate { get; set; }

        private CultureInfo currentCulture;

        private PeriodType _type;

        public Period()
        {
            SelectedDate = DateTime.Now;
            SelectedDate = FirstDayOfTheWeek(SelectedDate);
            currentCulture = CultureInfo.CurrentCulture;
            CalculateDates();
        }

        public Period(DateTime current,PeriodType type=PeriodType.Month, CultureInfo currentCulture = null)
        {
            if (currentCulture == null)
                this.currentCulture = CultureInfo.CurrentUICulture;
            else
                this.currentCulture = currentCulture;

            this.SelectedDate = current;
            SelectedDate = FirstDayOfTheWeek(SelectedDate);
            this._type = type;


            CalculateDates();

        }

        public void ResetSelectedDate(DateTime date)
        {
            SelectedDate = date;
            SelectedDate = FirstDayOfTheWeek(SelectedDate);
            CalculateDates();
        }

        public string GetDescription()
        {
            string description = "";
            switch (_type)
            {
                case PeriodType.Week:
                    description = $"{FromDate.ToString("dd")} - {ToDate.ToString("dd")} , {SelectedDate.ToString("MMM")}";
                    break;

                case PeriodType.Month:
                    description = $"{SelectedDate.ToString("MMMM")}, {SelectedDate.Year}";
                    break;

                case PeriodType.Year:
                    description = $"{SelectedDate.Year}";
                    break;

                case PeriodType.All:
                    description = ViewText.All;
                    break;
            }
            return description;
        }

        private void CalculateDates()
        {
            switch (_type)
            {
                case PeriodType.Week:
                    FromDate = FirstDayOfTheWeek(SelectedDate);
                    ToDate = LastDayOfTheWeek(SelectedDate);
                    break;

                case PeriodType.Month:
                    FromDate = FirstDayOfTheMonth(SelectedDate);
                    ToDate = LastDayOfTheMonth(SelectedDate);
                    break;

                case PeriodType.Year:
                    FromDate = FirstDayOfTheYear(SelectedDate);
                    ToDate = LastDayOfTheYear(SelectedDate);
                    break;

                case PeriodType.All:
                    FromDate = DateTime.MinValue;
                    ToDate = DateTime.MaxValue;
                    break;
            }
        }

        public void MoveToNextWeek()
        {
            SelectedDate.AddDays(7);
            CalculateDates();
        }
        public void MoveToPreviousWeek()
        {
            SelectedDate.AddDays(-7);
            CalculateDates();
        }

        public void MoveToNextMonth()
        {
            SelectedDate.AddMonths(1);
            CalculateDates();
        }

        public void MoveToPreviousMonth()
        {
            SelectedDate.AddMonths(-1);
            CalculateDates();
        }

        public void MoveToNextYear()
        {
            SelectedDate.AddYears(1);
            CalculateDates();
        }

        public void MoveToPreviousYear()
        {
            SelectedDate.AddYears(-1);
            CalculateDates();
        }

        public DateTime FirstDayOfTheMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public DateTime LastDayOfTheMonth(DateTime date)
        {
            return FirstDayOfTheMonth(date).AddMonths(1).AddTicks(-1);
        }

        public DateTime FirstDayOfTheYear(DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        public DateTime LastDayOfTheYear(DateTime date)
        {
            return FirstDayOfTheYear(date).AddMonths(12).AddTicks(-1);
        }

        public DateTime FirstDayOfTheWeek(DateTime date)
        {
            DayOfWeek firstDayFromCulture = currentCulture.DateTimeFormat.FirstDayOfWeek;
            int daysPassed =date.DayOfWeek - firstDayFromCulture;
            return date.AddDays(-daysPassed);
        }

        public DateTime LastDayOfTheWeek(DateTime date)
        {
            return FirstDayOfTheWeek(date).AddDays(6);
        }

        public int WeekOfTheYearNet(DateTime date)
        {
            return currentCulture.Calendar.GetWeekOfYear(
                date,
                currentCulture.DateTimeFormat.CalendarWeekRule,
                currentCulture.DateTimeFormat.FirstDayOfWeek);
        }

        public int WeekOfTheYear(DateTime date)
        {
            var day = (int)currentCulture.Calendar.GetDayOfWeek(date);
            return currentCulture.Calendar.GetWeekOfYear(
                date.AddDays(4 - (day == 0 ? 7 : day)),
                CalendarWeekRule.FirstFourDayWeek,
                DayOfWeek.Monday);
        }



    }
}
