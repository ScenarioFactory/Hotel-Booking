namespace HotelBooking.Specs.UI
{
    using System;
    using System.Globalization;
    using Framework;
    using OpenQA.Selenium;

    public class DatePicker
    {
        private By TextBox => By.Id(_id);
        private static By DayAnchor(int dayNumber) => By.XPath($"//div[@id='ui-datepicker-div']//table[@class='ui-datepicker-calendar']//a[text()={dayNumber}]");
        private static readonly By SelectedMonth = By.XPath("//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-month']");
        private static readonly By SelectedYear = By.XPath("//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-year']");
        private static readonly By PreviousMonthAnchor = By.XPath("//div[@id='ui-datepicker-div']//a[@data-handler='prev']");
        private static readonly By NextMonthAnchor = By.XPath("//div[@id='ui-datepicker-div']//a[@data-handler='next']");

        private readonly IWebDriver _driver;
        private readonly string _id;

        public DatePicker(IWebDriver driver, string id)
        {
            _driver = driver;
            _id = id;
        }

        public void SelectDate(DateTime dateToSelect)
        {
            // we could simply set the date in the textbox here, but as an exercise in WebDriver we will automate the datepicker UI

            _driver.ClickElementWhenClickable(TextBox);

            while (dateToSelect.Month < GetSelectedMonthNumber() || dateToSelect.Year < GetSelectedYear())
            {
                _driver.ClickElementWhenClickable(PreviousMonthAnchor);
            }

            while (dateToSelect.Month > GetSelectedMonthNumber() || dateToSelect.Year > GetSelectedYear())
            {
                _driver.ClickElementWhenClickable(NextMonthAnchor);
            }

            _driver.ClickElementWhenClickable(DayAnchor(dateToSelect.Day));
        }

        private int GetSelectedMonthNumber()
        {
            string selectedMonth = _driver.GetElementTextWhenVisible(SelectedMonth);
            return DateTime.ParseExact(selectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
        }

        private int GetSelectedYear()
        {
            string selectedYear = _driver.GetElementTextWhenVisible(SelectedYear);
            return int.Parse(selectedYear);
        }
    }
}
