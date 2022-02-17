namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using Framework;
    using OpenQA.Selenium;
    using Pattern;

    public class Select : WebTask
    {
        private readonly string _textToSelect;
        private By? _locator;

        private Select(string textToSelect)
        {
            _textToSelect = textToSelect;
        }

        public static Select ByText(string textToSelect)
        {
            return new Select(textToSelect);
        }

        public Select At(By locator)
        {
            _locator = locator;
            return this;
        }

        protected override void PerformAs(IActor actor, IWebDriver driver)
        {
            driver.SetSelectedOptionWhenVisible(_locator, _textToSelect);
        }
    }
}
