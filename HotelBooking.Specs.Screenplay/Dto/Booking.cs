namespace HotelBooking.Specs.Screenplay.Dto
{
    using System;

    public class Booking
    {
        public Booking(HotelGuest guest, decimal price, bool depositPaid, DateTime checkIn, DateTime checkOut)
        {
            Guest = guest;
            Price = price;
            DepositPaid = depositPaid;
            CheckIn = checkIn;
            CheckOut = checkOut;
        }

        public HotelGuest Guest { get; }

        public decimal Price { get; }
        
        public bool DepositPaid { get; }
        
        public DateTime CheckIn { get; }
        
        public DateTime CheckOut { get; }

        protected bool Equals(Booking other)
        {
            return Guest.Equals(other.Guest) && Price == other.Price && DepositPaid == other.DepositPaid && CheckIn.Equals(other.CheckIn) && CheckOut.Equals(other.CheckOut);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;

            return Equals((Booking)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Guest, Price, DepositPaid, CheckIn, CheckOut);
        }

        public static bool operator ==(Booking? left, Booking? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Booking? left, Booking? right)
        {
            return !Equals(left, right);
        }
    }
}
