namespace HotelBooking.Specs.UI
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Dto;
    using Framework;
    using OpenQA.Selenium;

    public class BookingsView
    {
        private static readonly By ExistingBookings = By.XPath("//div[@id='bookings']/div[@id]");

        private readonly IWebDriver _driver;

        public BookingsView(IWebDriver driver)
        {
            _driver = driver;
        }

        private BookingRow[] BookingRows
        {
            get
            {
                return
                    _driver.GetMultipleElementAttributeValuesWhenVisible(ExistingBookings, "id")
                        .Select(id => new BookingRow(_driver, id))
                        .ToArray();
            }
        }

        public Booking[] GetBookings()
        {
            return BookingRows
                .Select(row => row.Booking)
                .ToArray();
        }

        public void RemoveBooking(Booking booking)
        {
            BookingRows
                .Where(row => row.Booking.Equals(booking))
                .ForEach(row => row.Delete());
        }

        public void RemoveBookingsFor(HotelGuest guest)
        {
            BookingRows
                .Where(row => row.Guest.Equals(guest))
                .ForEach(row => row.Delete());
        }

        private class BookingRow
        {
            private By ValueElements => By.XPath($"//div[@id='bookings']/div[@id='{_id}']/div");
            private By DeleteButton => By.XPath($"//div[@id='bookings']/div[@id='{_id}']//input[@type='button']");

            private readonly IWebDriver _driver;
            private readonly string _id;

            public BookingRow(IWebDriver driver, string id)
            {
                _driver = driver;
                _id = id;
            }

            public HotelGuest Guest
            {
                get
                {
                    string[] bookingValues = _driver.GetMultipleElementTextValuesWhenVisible(ValueElements);
                    return new HotelGuest(bookingValues[0], bookingValues[1]);
                }
            }

            public Booking Booking
            {
                get
                {
                    string[] bookingValues = _driver.GetMultipleElementTextValuesWhenVisible(ValueElements);
                    return new Booking(
                        new HotelGuest(bookingValues[0], bookingValues[1]),
                        decimal.Parse(bookingValues[2]),
                        bool.Parse(bookingValues[3]),
                        DateTime.ParseExact(bookingValues[4], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                        DateTime.ParseExact(bookingValues[5], "yyyy-MM-dd", CultureInfo.InvariantCulture));
                }
            }

            public void Delete()
            {
                _driver.ClickElementWhenClickable(DeleteButton);
            }
        }
    }
}
