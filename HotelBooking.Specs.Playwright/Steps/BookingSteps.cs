namespace HotelBooking.Specs.Playwright.Steps
{
    using System.Linq;
    using System.Threading.Tasks;
    using Dto;
    using FluentAssertions;
    using Framework;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using UI;

    [Binding]
    public class BookingSteps
    {
        private readonly BookingsView _bookingsView;
        private readonly BookingForm _bookingForm;
        private Booking? _bookingUnderTest;

        public BookingSteps(BookingsView bookingsView, BookingForm bookingForm)
        {
            _bookingsView = bookingsView;
            _bookingForm = bookingForm;
        }

        [Given(@"there are no bookings for guest '(.*)'")]
        public async Task GivenThereAreNoBookingsForGuest(HotelGuest guest)
        {
            // the teardown would typically be performed by a repository acting directly on the database,
            // but in the absence of database access this must be performed via the UI

            await _bookingsView.RemoveBookingsFor(guest);
        }

        [Given(@"the following booking for guest '(.*)'")]
        public async Task GivenTheFollowingBookingForGuest(HotelGuest guest, Table table)
        {
            // the teardown and setup would typically be performed by a repository acting directly on the database,
            // but in the absence of database access this must be performed via the UI

            await _bookingsView.RemoveBookingsFor(guest);

            TableRow values = table.Rows.Single();

            _bookingUnderTest = new Booking(
                guest,
                values.GetDecimal("Price"),
                values.GetBoolean("Deposit Paid"),
                values.GetDateTime("Check-in"),
                values.GetDateTime("Check-out"));

            await _bookingForm.CreateBookingFor(_bookingUnderTest);
        }
        
        [When(@"the following booking is made for guest '(.*)'")]
        public async Task WhenTheFollowingBookingIsMadeForGuest(HotelGuest guest, Table table)
        {
            TableRow values = table.Rows.Single();

            _bookingUnderTest = new Booking(
                guest,
                values.GetDecimal("Price"),
                values.GetBoolean("Deposit Paid"),
                values.GetDateTime("Check-in"),
                values.GetDateTime("Check-out"));

            await _bookingForm.CreateBookingFor(_bookingUnderTest);
        }

        [When(@"the booking is cancelled")]
        public async Task WhenTheBookingIsCancelled()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            await _bookingsView.RemoveBooking(_bookingUnderTest);
        }

        [Then(@"the booking should appear in the list of bookings")]
        public async Task ThenTheBookingShouldAppearInTheListOfBookings()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            bool hasExpectedBookingAppeared = await Poller.PollForSuccessAsync(async () =>
            {
                Booking[] bookings = await _bookingsView.GetBookings();
                return bookings.Contains(_bookingUnderTest);
            });

            hasExpectedBookingAppeared.Should().BeTrue();
        }

        [Then(@"the booking should be removed from the list of bookings")]
        public async Task ThenTheBookingShouldBeRemovedFromTheListOfBookings()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            bool hasBookingBeenRemoved = await Poller.PollForSuccessAsync(async () =>
            {
                Booking[] bookings = await _bookingsView.GetBookings();
                return !bookings.Contains(_bookingUnderTest);
            });

            hasBookingBeenRemoved.Should().BeTrue();
        }
    }
}
