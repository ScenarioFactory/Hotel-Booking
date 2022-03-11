namespace HotelBooking.Specs.Playwright.UI
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Microsoft.Playwright;

    public class DatePicker
    {
        private string TextBox => $"#{_id}";
        private static string DayAnchor(int dayNumber) => $"xpath=//div[@id='ui-datepicker-div']//table[@class='ui-datepicker-calendar']//a[text()={dayNumber}]";
        private const string SelectedMonth = "xpath=//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-month']";
        private const string SelectedYear = "xpath=//div[@id='ui-datepicker-div']//div[@class='ui-datepicker-title']/span[@class='ui-datepicker-year']";
        private const string PreviousMonthAnchor = "xpath=//div[@id='ui-datepicker-div']//a[@data-handler='prev']";
        private const string NextMonthAnchor = "xpath=//div[@id='ui-datepicker-div']//a[@data-handler='next']";

        private readonly IPage _page;
        private readonly string _id;

        public DatePicker(IPage page, string id)
        {
            _page = page;
            _id = id;
        }

        public async Task SelectDate(DateTime dateToSelect)
        {
            // we could simply set the date in the textbox here, but as an exercise in WebDriver we will automate the datepicker UI

            await _page.ClickAsync(TextBox);

            while (dateToSelect.Month < await GetSelectedMonthNumber() || dateToSelect.Year < await GetSelectedYear())
            {
                await _page.ClickAsync(PreviousMonthAnchor);
            }

            while (dateToSelect.Month > await GetSelectedMonthNumber() || dateToSelect.Year > await GetSelectedYear())
            {
                await _page.ClickAsync(NextMonthAnchor);
            }

            await _page.ClickAsync(DayAnchor(dateToSelect.Day));
        }

        private async Task<int> GetSelectedMonthNumber()
        {
            string? selectedMonth = await _page.TextContentAsync(SelectedMonth);
            return DateTime.ParseExact(selectedMonth, "MMMM", CultureInfo.CurrentCulture).Month;
        }

        private async Task<int> GetSelectedYear()
        {
            string? selectedYear = await _page.TextContentAsync(SelectedYear);
            return int.Parse(selectedYear);
        }
    }
}
