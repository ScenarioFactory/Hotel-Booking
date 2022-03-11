namespace HotelBooking.Specs.Playwright.Framework
{
    using System.Threading.Tasks;
    using BoDi;
    using Microsoft.Playwright;
    using TechTalk.SpecFlow;

    [Binding]
    public class PlaywrightManager
    {
        private readonly IObjectContainer _container;
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IPage? _page;

        public PlaywrightManager(IObjectContainer container)
        {
            _container = container;
        }

        [BeforeScenario("Web")]
        public async Task InitializeWebDriver()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 50 });
            _page = await _browser.NewPageAsync();

            await _page.GotoAsync("http://hotel-test.equalexperts.io/");

            _container.RegisterInstanceAs(_page);
        }

        [AfterScenario("Web")]
        public void QuitWebDriver()
        {
            _browser?.DisposeAsync();
            _playwright?.Dispose();
        }
    }
}
