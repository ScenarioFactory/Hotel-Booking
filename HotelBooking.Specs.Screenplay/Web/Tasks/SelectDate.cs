namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using System;
    using System.Globalization;
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class SelectDate : WebTask
    {
        private By TextBox => By.Id(_id);
        private static By DayAnchor(int dayNumber) => By.XPath($"//div[@id='ui-datepicker-div']//table[@class='ui-datepicker-calendar']//a[text()={dayNumber}]");
        private static readonly By SelectedMonth = By.XPath("//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-month']");
        private static readonly By SelectedYear = By.XPath("//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-year']");
        private static readonly By PreviousMonthAnchor = By.XPath("//div[@id='ui-datepicker-div']//a[@data-handler='prev']");
        private static readonly By NextMonthAnchor = By.XPath("//div[@id='ui-datepicker-div']//a[@data-handler='next']");

        private readonly DateTime _dateToSelect;
        private string? _id;

        private SelectDate(DateTime dateToSelect)
        {
            _dateToSelect = dateToSelect;
        }
        public static SelectDate Of(DateTime dateToSelect)
        {
            return new SelectDate(dateToSelect);
        }
        
        public SelectDate For(string id)
        {
            _id = id;
            return this;
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            // we could simply set the date in the textbox here, but as an exercise in WebDriver we will automate the datepicker UI

            int GetSelectedMonthNumber()
            {
                string selectedMonth = driver.GetElementTextWhenVisible(SelectedMonth);
                return DateTime.ParseExact(selectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
            }

            int GetSelectedYear()
            {
                string selectedYear = driver.GetElementTextWhenVisible(SelectedYear);
                return int.Parse(selectedYear);
            }

            driver.ClickElementWhenClickable(TextBox);

            while (_dateToSelect.Month < GetSelectedMonthNumber() || _dateToSelect.Year < GetSelectedYear())
            {
                driver.ClickElementWhenClickable(PreviousMonthAnchor);
            }

            while (_dateToSelect.Month > GetSelectedMonthNumber() || _dateToSelect.Year > GetSelectedYear())
            {
                driver.ClickElementWhenClickable(NextMonthAnchor);
            }

            driver.ClickElementWhenClickable(DayAnchor(_dateToSelect.Day));
        }
    }
}
