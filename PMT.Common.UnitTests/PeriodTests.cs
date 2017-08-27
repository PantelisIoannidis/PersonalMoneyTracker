using Moq;
using NUnit.Framework;
using PMT.Common.Helpers;
using PMT.Common.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common.UnitTests
{
    [TestFixture]
    public class PeriodTests
    {
        private Period testPeriod;
        private CultureInfo currentCulture;
        private DateTime testDate;
        private DateTime fakeCurrentDate;

        [SetUp]
        public void Setup()
        {
            testPeriod = new Period();
            currentCulture = new CultureInfo("en-US");
            testDate = new DateTime(2016, 11, 29);
            fakeCurrentDate = new DateTime(2016, 12, 15);
        }

        [Test]
        public void period_init_selectedDay_should_be_the_first_day_of_the_week_of_the_selected_period_of_time()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);
            Assert.AreEqual(new DateTime(2016, 11, 27), testPeriod.FromDate);
        }

        [Test]
        public void period_init_type_month_should_return_fromdate_beginning_of_the_month_todate_end_of_the_month()
        {
            testPeriod.Init(testDate, PeriodType.Month, currentCulture);

            Assert.AreEqual(new DateTime(2016, 11, 1), testPeriod.FromDate);
            Assert.AreEqual(new DateTime(2016, 11, 30).SetTimeTo235959(), testPeriod.ToDate);
        }

        [Test]
        public void period_init_type_week_should_return_fromdate_beginning_of_the_week_todate_end_of_the_week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            Assert.AreEqual(new DateTime(2016, 11, 27), testPeriod.FromDate);
            Assert.AreEqual(new DateTime(2016, 12, 3).SetTimeTo235959(), testPeriod.ToDate);
        }

        [Test]
        public void period_init_type_year_should_return_fromdate_beginning_of_the_year_todate_end_of_the_year()
        {
            testPeriod.Init(testDate, PeriodType.Year, currentCulture);

            Assert.AreEqual(new DateTime(2016, 1, 1), testPeriod.FromDate);
            Assert.AreEqual(new DateTime(2016, 12, 31).SetTimeTo235959(), testPeriod.ToDate);
        }

        [Test]
        public void period_init_type_all_should_return_fromdate_minValue_net_supported_todate_maxvalue_net_supported()
        {
            testPeriod.Init(testDate, PeriodType.All, currentCulture);

            Assert.AreEqual(DateTime.MinValue, testPeriod.FromDate);
            Assert.AreEqual(DateTime.MaxValue, testPeriod.ToDate);
        }

        [Test]
        public void ResetSelectedDate_should_select_the_first_day_of_the_week_of_current_date()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.Week, currentCulture);
            Assert.AreEqual(new DateTime(2016, 12, 11), testPeriod.FromDate);
        }

        [Test]
        public void GetDescription_culture_en_us_for_period_week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            var description = testPeriod.GetDescription();
            Assert.AreEqual("27 - 03  Nov", description);
        }

        [Test]
        public void GetDescription_culture_en_us_for_period_month()
        {
            testPeriod.Init(testDate, PeriodType.Month, currentCulture);

            var description = testPeriod.GetDescription();
            Assert.AreEqual("November 2016", description);
        }

        [Test]
        public void GetDescription_culture_en_us_for_period_year()
        {
            testPeriod.Init(testDate, PeriodType.Year, currentCulture);

            var description = testPeriod.GetDescription();
            Assert.AreEqual("2016", description);
        }

        [Test]
        public void GetDescription_culture_en_us_for_period_all()
        {
            testPeriod.Init(testDate, PeriodType.All, currentCulture);

            var description = testPeriod.GetDescription();
            Assert.AreEqual(ViewText.All, description);
        }

        [Test]
        public void MoveToNext_period_week_should_move_forward_a_week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            testPeriod.MoveToNext();

            Assert.AreEqual(new DateTime(2016, 12, 4), testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToNext_period_month_should_move_forward_a_month()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.Month, currentCulture);

            testPeriod.MoveToNext();

            Assert.AreEqual(new DateTime(2017, 1, 8), testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToNext_period_year_should_move_forward_a_year()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.Year, currentCulture);

            testPeriod.MoveToNext();

            Assert.AreEqual(new DateTime(2017, 12, 10), testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToNext_period_all_should_not_move_to_another_date()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.All, currentCulture);

            testPeriod.MoveToNext();

            Assert.AreEqual(new DateTime(2016, 12, 11), testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToPrevious_period_week_should_move_backward_a_week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            testPeriod.MoveToPrevious();

            Assert.AreEqual(new DateTime(2016, 11, 20),testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToPrevious_period_month_should_move_backward_a_month()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.Month, currentCulture);

            testPeriod.MoveToPrevious();

            Assert.AreEqual(new DateTime(2016, 11, 6), testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToPrevious_period_year_should_move_backward_a_year()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.Year, currentCulture);

            testPeriod.MoveToPrevious();

            Assert.AreEqual (new DateTime(2015, 12, 6),testPeriod.SelectedDate);
        }

        [Test]
        public void MoveToPrevious_period_all_should_not_move_to_another_date()
        {
            testPeriod.Init(fakeCurrentDate, PeriodType.All, currentCulture);

            testPeriod.MoveToPrevious();

            Assert.AreEqual(new DateTime(2016, 12, 11), testPeriod.SelectedDate);
        }

        [Test]
        public void FirstDayOfTheMonth_should_return_first_day_of_the_month()
        {
            var firstDay = testPeriod.FirstDayOfTheMonth(testDate);

            Assert.AreEqual(new DateTime(2016, 11, 1), firstDay);
        }

        [Test]
        public void LastDayOfTheMonth_should_return_last_day_of_the_month()
        {
            var lastDay = testPeriod.LastDayOfTheMonth(testDate);

            Assert.AreEqual(new DateTime(2016, 11, 30).SetTimeTo235959(), lastDay);
        }

        [Test]
        public void FirstDayOfTheYear_should_return_first_day_of_the_year()
        {
            var firstDay = testPeriod.FirstDayOfTheYear(testDate);

            Assert.AreEqual(new DateTime(2016, 1, 1), firstDay);
        }

        [Test]
        public void LastDayOfTheYear_should_return_last_day_of_the_year()
        {
            var lastDay = testPeriod.LastDayOfTheYear(testDate);

            Assert.AreEqual(new DateTime(2016, 12, 31).SetTimeTo235959(), lastDay);
        }

        [Test]
        public void FirstDayOfTheWeek_should_return_first_day_of_the_Week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            var firstDay = testPeriod.FirstDayOfTheWeek(testDate);

            Assert.AreEqual(new DateTime(2016, 11, 27), firstDay);
        }

        [Test]
        public void LastDayOfTheWeek_should_return_last_day_of_the_Week()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            var lastDay = testPeriod.LastDayOfTheWeek(testDate);

            Assert.AreEqual(new DateTime(2016, 12, 3).SetTimeTo235959(), lastDay);
        }

        [Test]
        public void WeekOfTheYearNet_for_a_given_date_should_return_the_week_number_in_year_net_edition()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            var week = testPeriod.WeekOfTheYearNet(testDate);

            Assert.AreEqual(49, week);
        }

        [Test]
        public void WeekOfTheYearNet_for_a_given_date_should_return_the_week_number_in_year_ISO_8601_edition()
        {
            testPeriod.Init(testDate, PeriodType.Week, currentCulture);

            var week = testPeriod.WeekOfTheYear(testDate);

            Assert.AreEqual(48, week);
        }

    }


}
