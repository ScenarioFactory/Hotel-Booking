namespace HotelBooking.Specs.Screenplay.Framework
{
    using BoDi;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using TechTalk.SpecFlow;

    [Binding]
    public class WebDriverManager
    {
        private readonly IObjectContainer _container;
        private IWebDriver? _driver;

        public WebDriverManager(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario("Web")]
        public void InitializeWebDriver()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl(AppSettings.Url);
            _driver.Manage().Window.Maximize();

            _container.RegisterInstanceAs(_driver);
        }

        [AfterScenario("Web")]
        public void QuitWebDriver()
        {
            _driver?.Quit();
        }
    }
}
