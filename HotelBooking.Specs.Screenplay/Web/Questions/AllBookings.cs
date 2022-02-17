namespace HotelBooking.Specs.Screenplay.Web.Questions
{
    using System;
    using System.Globalization;
    using System.Linq;
    using Dto;
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class AllBookings : WebQuestion<Booking[]>
    {
        private static readonly By BookingElements = By.XPath("//div[@id='bookings']/div[@id]");

        private AllBookings()
        {
        }

        public static AllBookings ShownOnScreen()
        {
            return new AllBookings();
        }
        
        protected override Booking[] AskAs(IActor actor, IWebDriver driver)
        {
            return
                driver.GetMultipleElementAttributeValuesWhenVisible(BookingElements, "id")
                    .Select(id => new BookingRow(driver, id).Booking)
                    .ToArray();
        }

        private class BookingRow
        {
            private By ValueElements => By.XPath($"//div[@id='bookings']/div[@id='{_id}']/div");

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
        }
    }
}
