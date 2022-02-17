namespace HotelBooking.Specs.UI
{
    using System.Globalization;
    using Dto;
    using Framework;
    using OpenQA.Selenium;

    public class BookingForm
    {
        private static readonly By FirstNameTextbox = By.Id("firstname");
        private static readonly By SurnameTextbox = By.Id("lastname");
        private static readonly By PriceTextbox = By.Id("totalprice");
        private static readonly By DepositPaidSelect = By.Id("depositpaid");
        private static readonly By SaveButton = By.XPath("//div[@id='form']//input[@type='button']");

        private readonly IWebDriver _driver;
        private readonly DatePicker _checkInDatePicker;
        private readonly DatePicker _checkOutDatePicker;

        public BookingForm(IWebDriver driver)
        {
            _driver = driver;
            _checkInDatePicker = new DatePicker(driver, "checkin");
            _checkOutDatePicker = new DatePicker(driver, "checkout");
        }

        public void CreateBookingFor(Booking booking)
        {
            _driver.SetTextboxValueWhenVisible(FirstNameTextbox, booking.Guest.FirstName);
            _driver.SetTextboxValueWhenVisible(SurnameTextbox, booking.Guest.Surname);
            _driver.SetTextboxValueWhenVisible(PriceTextbox, booking.Price.ToString(CultureInfo.InvariantCulture));
            
            _driver.SetSelectedOptionWhenVisible(DepositPaidSelect, booking.DepositPaid ? "true" : "false");
            
            _checkInDatePicker.SelectDate(booking.CheckIn);
            _checkOutDatePicker.SelectDate(booking.CheckOut);
            
            _driver.ClickElementWhenClickable(SaveButton);
        }
    }
}
