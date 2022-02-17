namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using System.Linq;
    using Dto;
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class RemoveBookings : WebTask
    {
        private static readonly By BookingElements = By.XPath("//div[@id='bookings']/div[@id]");

        private readonly HotelGuest _guest;

        private RemoveBookings(HotelGuest guest)
        {
            _guest = guest;
        }

        public static RemoveBookings For(HotelGuest guest)
        {
            return new RemoveBookings(guest);
        }
        
        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            driver.GetMultipleElementAttributeValuesWhenVisible(BookingElements, "id")
                .Select(id => new BookingRow(driver, id))
                .Where(row => row.Guest.Equals(_guest))
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

            public void Delete()
            {
                _driver.ClickElementWhenClickable(DeleteButton);
            }
        }
    }
}
