namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class Click : WebTask
    {
        private readonly By _locator;

        private Click(By locator)
        {
            _locator = locator;
        }

        public static Click On(By locator)
        {
            return new Click(locator);
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            driver.ClickElementWhenClickable(_locator);
        }
    }
}
