using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Common.UnitTests
{
    [TestFixture]
    public class UtilsTests
    {
        public class SampleObject
        {
            public string ItemA { get; set; }
            public int ItemB { get; set; }
            public int ItemC { get; set; }
        }
        
        [Test]
        public void DistinctBy_Must_Remove_Duplicated_Values_In_ComplexTypes()
        {
            List<SampleObject> withDuplicates = new List<SampleObject>();
            withDuplicates.Add(new SampleObject() { ItemA = "Foo", ItemB = 10, ItemC = 1 });
            withDuplicates.Add(new SampleObject() { ItemA = "Bar", ItemB = 20, ItemC = 2 });
            withDuplicates.Add(new SampleObject() { ItemA = "Foo", ItemB = 30, ItemC = 3 });
            withDuplicates.Add(new SampleObject() { ItemA = "Bar", ItemB = 10, ItemC = 4 });

            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemA).Count() == 2);
            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemB).Count() == 3);
            Assert.IsTrue(withDuplicates.DistinctBy(c => c.ItemC).Count() == 4);

        }

        [Test]
        public void ToLocalTime_should_return_date_with_time_offset()
        {
            var originalDateTime = new DateTime(2010, 12, 15);
            var timeOffsetInMinutes = 120;

            var newDateTime = originalDateTime.ToLocalTime(timeOffsetInMinutes);

            var timeDiscrepancy = newDateTime.Minute - originalDateTime.ToUniversalTime().AddMinutes(timeOffsetInMinutes).Minute;

            Assert.AreEqual(timeDiscrepancy, 0);
        }

        [Test]
        public void OffsetInCurrentMonth_with_input_offset_from_the_beginning_of_the_month_return_date()
        {
            var originalDateTime = new DateTime(2010, 12, 15);
            var timeOffsetInDays = 22;
            var expectedDate = new DateTime(2010, 12, timeOffsetInDays);

            var newDateTime = originalDateTime.OffsetInCurrentMonth(timeOffsetInDays);

            Assert.AreEqual(expectedDate, newDateTime);
        }

        [Test]
        public void FormatNumbers_should_format_decimal_with_current_culture()
        {
            string originalCurCulture = System.Threading.Thread.CurrentThread.CurrentCulture.ToString();
            CultureInfo ci;
            
            string test1_en_US = "en-US";
            ci = new CultureInfo(test1_en_US);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            var formattedNumber1_en_US = (10.11m).FormatNumbers();

            string test2_uk = "uk";
            ci = new CultureInfo(test2_uk);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            var formattedNumber2_uk = (10.11m).FormatNumbers();

            string test3_de = "de";
            ci = new CultureInfo(test3_de);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            var formattedNumber3_de = (10.11m).FormatNumbers();

            ci = new CultureInfo(originalCurCulture);
            System.Threading.Thread.CurrentThread.CurrentCulture = ci;

            Assert.AreEqual(formattedNumber1_en_US, "$10.11");
            Assert.AreEqual(formattedNumber2_uk, "10,11 ₴");
            Assert.AreEqual(formattedNumber3_de, "10,11 €");

        }

        [Test]
        public void FullDateMinusTickSpan_is_the_value_235959_a_day_minus_a_tick()
        {
            DateTime a = new DateTime(2016, 1, 1);
            DateTime b = new DateTime(2016, 1, 2).AddTicks(-1);
            var almost_a_day = Utils.FullDateMinusTickSpan();
            var a_day_timespan = b.Subtract(a);

            Assert.AreEqual(almost_a_day, a_day_timespan);
        }

        [Test]
        public void SetTimeTo000000_should_set_time_to_midnight()
        {
            var original_date = new DateTime(2016, 1, 1, 15, 12, 34);
            var expected_date = new DateTime(2016, 1, 1);
            var only_date = original_date.SetTimeTo000000();

            Assert.AreEqual(expected_date, only_date);
        }

        [Test]
        public void SetTimeTo235959_should_set_time_a_tick_before_midnight()
        {
            var original_date = new DateTime(2016, 1, 1, 15, 12, 34);
            var expected_date = new DateTime(2016, 1, 1).SetTimeTo235959();
            var full_day = original_date.Date.Add(Utils.FullDateMinusTickSpan());

            Assert.AreEqual(expected_date, full_day);
        }
    }
}
