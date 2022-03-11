namespace HotelBooking.Specs.Playwright.Dto
{
    using System;
    using System.Linq;
    using TechTalk.SpecFlow;

    public class HotelGuest
    {
        public HotelGuest(string fullName)
        {
            FirstName = fullName.Split(' ').First();
            Surname = fullName.Split(' ').Skip(1).Single();
        }

        public HotelGuest(string firstName, string surname)
        {
            FirstName = firstName;
            Surname = surname;
        }

        public string FirstName { get; }
        
        public string Surname { get; }

        protected bool Equals(HotelGuest other)
        {
            return FirstName == other.FirstName && Surname == other.Surname;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((HotelGuest)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, Surname);
        }

        public static bool operator ==(HotelGuest? left, HotelGuest? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HotelGuest? left, HotelGuest? right)
        {
            return !Equals(left, right);
        }

        [Binding]
        private class HotelGuestStepArgumentTransformation
        {
            [StepArgumentTransformation]
            private HotelGuest ToHotelGuest(string fullName) => new(fullName);
        }
    }
}
