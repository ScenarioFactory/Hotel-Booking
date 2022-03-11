namespace HotelBooking.Specs.Playwright.Framework
{
    using System;
    using System.Threading.Tasks;

    public static class Poller
    {
        public static async Task<bool> PollForSuccessAsync(Func<Task<bool>> predicate, int pollingLimit = 10, int pollingIntervalSeconds = 1)
        {
            for (int i = 0; i < pollingLimit; i++)
            {
                if (await predicate())
                {
                    return true;
                }

                await Task.Delay(TimeSpan.FromSeconds(pollingIntervalSeconds));
            }

            return false;
        }
    }
}