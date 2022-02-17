namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class SendKeys : WebTask
    {
        private readonly string _value;
        private By? _locator;

        public SendKeys(string value)
        {
            _value = value;
        }

        public static SendKeys Of(string value)
        {
            return new SendKeys(value);
        }

        public SendKeys To(By locator)
        {
            _locator = locator;
            return this;
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            driver.SendKeysWhenVisible(_locator, _value);
        }
    }
}
