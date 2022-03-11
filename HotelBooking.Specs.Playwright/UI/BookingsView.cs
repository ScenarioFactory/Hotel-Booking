namespace HotelBooking.Specs.Playwright.UI
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Dto;
    using Framework;
    using Microsoft.Playwright;

    public class BookingsView
    {
        private const string ExistingBookings = "xpath=//div[@id='bookings']/div[@id]";

        private readonly IPage _page;

        public BookingsView(IPage page)
        {
            _page = page;
        }

        public async Task<Booking[]> GetBookings()
        {
            BookingRow[] bookingRows = await GetBookingRows();

            return await Task.WhenAll(bookingRows.Select(row => row.Booking()));
        }

        public async Task RemoveBooking(Booking booking)
        {
            BookingRow[] bookingRows = await GetBookingRows();

            foreach (BookingRow row in bookingRows)
            {
                if (await row.Booking() == booking)
                {
                    await row.Delete();
                }
            }
        }

        public async Task RemoveBookingsFor(HotelGuest guest)
        {
            BookingRow[] bookingRows = await GetBookingRows();

            foreach (BookingRow row in bookingRows)
            {
                if (await row.Guest() == guest)
                {
                    await row.Delete();
                }
            }
        }
        
        private async Task<BookingRow[]> GetBookingRows()
        {
            ILocator[] locators =  await _page.Locator(ExistingBookings).GetLocatorsAsync();

            string?[] ids = await Task.WhenAll(locators.Select(locator => locator.GetAttributeAsync("id")));

            return ids
                .Select(id => new BookingRow(_page, id))
                .ToArray();
        }

        private class BookingRow
        {
            private string ValueElements => $"xpath=//div[@id='bookings']/div[@id='{_id}']/div";
            private string DeleteButton => $"xpath=//div[@id='bookings']/div[@id='{_id}']//input[@type='button']";

            private readonly IPage _page;
            private readonly string _id;

            public BookingRow(IPage page, string id)
            {
                _page = page;
                _id = id;
            }

            public async Task<Booking> Booking()
            {
                IReadOnlyList<string> bookingValues = await _page.Locator(ValueElements).AllTextContentsAsync();

                return new Booking(
                    new HotelGuest(bookingValues[0], bookingValues[1]),
                    decimal.Parse(bookingValues[2]),
                    bool.Parse(bookingValues[3]),
                    DateTime.ParseExact(bookingValues[4], "yyyy-MM-dd", CultureInfo.InvariantCulture),
                    DateTime.ParseExact(bookingValues[5], "yyyy-MM-dd", CultureInfo.InvariantCulture));
            }

            public async Task<HotelGuest> Guest()
            {
                IReadOnlyList<string> bookingValues = await _page.Locator(ValueElements).AllTextContentsAsync();
                return new HotelGuest(bookingValues[0], bookingValues[1]);
            }

            public async Task Delete()
            {
                await _page.Locator(DeleteButton).ClickAsync();
            }
        }
    }
}
