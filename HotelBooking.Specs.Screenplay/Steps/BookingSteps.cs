namespace HotelBooking.Specs.Screenplay.Steps
{
    using System.Linq;
    using Dto;
    using FluentAssertions;
    using Framework;
    using OpenQA.Selenium;
    using Pattern;
    using TechTalk.SpecFlow;
    using TechTalk.SpecFlow.Assist;
    using Web;
    using Web.Questions;
    using Web.Tasks;

    [Binding]
    public class BookingSteps
    {
        private readonly IActor _actor;
        private Booking? _bookingUnderTest;

        public BookingSteps(IWebDriver driver)
        {
            _actor = new Actor()
                .WhoCan(BrowseTheWeb.With(driver));
        }

        [Given(@"there are no bookings for guest '(.*)'")]
        public void GivenThereAreNoBookingsForGuest(HotelGuest guest)
        {
            // the teardown would typically be performed by a repository acting directly on the database,
            // but in the absence of database access this must be performed via the UI

            _actor.AttemptsTo(RemoveBookings.For(guest));
        }

        [Given(@"the following booking for guest '(.*)'")]
        public void GivenTheFollowingBookingForGuest(HotelGuest guest, Table table)
        {
            // the teardown and setup would typically be performed by a repository acting directly on the database,
            // but in the absence of database access this must be performed via the UI

            TableRow values = table.Rows.Single();

            _bookingUnderTest = new Booking(
                guest,
                values.GetDecimal("Price"),
                values.GetBoolean("Deposit Paid"),
                values.GetDateTime("Check-in"),
                values.GetDateTime("Check-out"));

            _actor.AttemptsTo(
                RemoveBookings.For(guest),
                CreateBooking.For(_bookingUnderTest));
        }

        [When(@"the following booking is made for guest '(.*)'")]
        public void WhenTheFollowingBookingIsMadeForGuest(HotelGuest guest, Table table)
        {
            TableRow values = table.Rows.Single();

            _bookingUnderTest = new Booking(
                guest,
                values.GetDecimal("Price"),
                values.GetBoolean("Deposit Paid"),
                values.GetDateTime("Check-in"),
                values.GetDateTime("Check-out"));

            _actor.AttemptsTo(CreateBooking.For(_bookingUnderTest));
        }

        [When(@"the booking is cancelled")]
        public void WhenTheBookingIsCancelled()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            _actor.AttemptsTo(RemoveBooking.For(_bookingUnderTest));
        }

        [Then(@"the booking should appear in the list of bookings")]
        public void ThenTheBookingShouldAppearInTheListOfBookings()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            bool hasExpectedBookingAppeared = Poller.PollForSuccess(() =>
            {
                Booking[] bookings = _actor.AsksFor(AllBookings.ShownOnScreen());
                return bookings.Contains(_bookingUnderTest);
            });

            hasExpectedBookingAppeared.Should().BeTrue();
        }

        [Then(@"the booking should be removed from the list of bookings")]
        public void ThenTheBookingShouldBeRemovedFromTheListOfBookings()
        {
            _bookingUnderTest.Should().NotBeNull("the booking under test should have been set in a preceding step");

            bool hasBookingBeenRemoved = Poller.PollForSuccess(() =>
            {
                Booking[] bookings = _actor.AsksFor(AllBookings.ShownOnScreen());
                return !bookings.Contains(_bookingUnderTest);
            });

            hasBookingBeenRemoved.Should().BeTrue();
        }
    }
}
