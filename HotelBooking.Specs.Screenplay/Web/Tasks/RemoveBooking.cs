namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Dto;
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class RemoveBooking : WebTask
    {
        private static readonly By BookingElements = By.XPath("//div[@id='bookings']/div[@id]");

        private readonly Booking _booking;

        private RemoveBooking(Booking booking)
        {
            _booking = booking;
        }

        public static RemoveBooking For(Booking booking)
        {
            return new RemoveBooking(booking);
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            driver.GetMultipleElementAttributeValuesWhenVisible(BookingElements, "id")
                .Select(id => new BookingRow(driver, id))
                .Where(row => row.Booking.Equals(_booking))
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
