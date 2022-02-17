namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using System.Globalization;
    using Dto;
    using OpenQA.Selenium;
    using Pattern;

    public class CreateBooking : WebTask
    {
        private static readonly By FirstNameTextbox = By.Id("firstname");
        private static readonly By SurnameTextbox = By.Id("lastname");
        private static readonly By PriceTextbox = By.Id("totalprice");
        private static readonly By DepositPaidSelect = By.Id("depositpaid");
        private static readonly By SaveButton = By.XPath("//div[@id='form']//input[@type='button']");

        private readonly Booking _booking;

        private CreateBooking(Booking booking)
        {
            _booking = booking;
        }

        public static CreateBooking For(Booking booking)
        {
            return new CreateBooking(booking);
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            actor.AttemptsTo(
                SendKeys.Of(_booking.Guest.FirstName).To(FirstNameTextbox),
                SendKeys.Of(_booking.Guest.Surname).To(SurnameTextbox),
                SendKeys.Of(_booking.Price.ToString(CultureInfo.InvariantCulture)).To(PriceTextbox),
                Select.ByText(_booking.DepositPaid ? "true" : "false").At(DepositPaidSelect),
                SelectDate.Of(_booking.CheckIn).For("checkin"),
                SelectDate.Of(_booking.CheckOut).For("checkout"),
                Click.On(SaveButton));
        }
    }
}
