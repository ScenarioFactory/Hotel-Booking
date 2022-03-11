namespace HotelBooking.Specs.Playwright.Framework
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Playwright;

    public static class LocatorExtensions
    {
        public static async Task<ILocator[]> GetLocatorsAsync(this ILocator locator)
        {
            int rowCount = await locator.CountAsync();

            List<ILocator> locators = new List<ILocator>();

            for (int i = 0; i < rowCount; i++)
            {
                locators.Add(locator.Nth(i));
            }

            return locators.ToArray();
        }
    }
}
