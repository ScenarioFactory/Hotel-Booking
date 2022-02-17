namespace HotelBooking.Specs.Screenplay.Framework
{
    using System;
    using System.Collections.Generic;

    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }
    }
}
