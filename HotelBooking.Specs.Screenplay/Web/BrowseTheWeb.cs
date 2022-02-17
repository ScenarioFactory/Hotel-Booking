namespace HotelBooking.Specs.Screenplay.Web
{
    using OpenQA.Selenium;
    using Pattern;

    public class BrowseTheWeb : IAbility
    {
        public IWebDriver Driver { get; }

        private BrowseTheWeb(IWebDriver driver)
        {
            Driver = driver;
        }

        public static BrowseTheWeb With(IWebDriver driver)
        {
            return new BrowseTheWeb(driver);
        }
    }
}
