namespace HotelBooking.Specs.Playwright.UI
{
    using System.Globalization;
    using System.Threading.Tasks;
    using Dto;
    using Microsoft.Playwright;

    public class BookingForm
    {
        private const string FirstNameTextbox = "#firstname";
        private const string SurnameTextbox = "#lastname";
        private const string PriceTextbox = "#totalprice";
        private const string DepositPaidSelect = "#depositpaid";
        private const string SaveButton = "xpath=//div[@id='form']//input[@type='button']";

        private readonly IPage _page;
        private readonly DatePicker _checkInDatePicker;
        private readonly DatePicker _checkOutDatePicker;

        public BookingForm(IPage page)
        {
            _page = page;
            _checkInDatePicker = new DatePicker(page, "checkin");
            _checkOutDatePicker = new DatePicker(page, "checkout");
        }

        public async Task CreateBookingFor(Booking booking)
        {
            await _page.FillAsync(FirstNameTextbox, booking.Guest.FirstName);
            await _page.FillAsync(SurnameTextbox, booking.Guest.Surname);
            await _page.FillAsync(PriceTextbox, booking.Price.ToString(CultureInfo.InvariantCulture));

            await _page.SelectOptionAsync(DepositPaidSelect, booking.DepositPaid ? "true" : "false");

            await _checkInDatePicker.SelectDate(booking.CheckIn);
            await _checkOutDatePicker.SelectDate(booking.CheckOut);

            await _page.ClickAsync(SaveButton);
        }
    }
}
