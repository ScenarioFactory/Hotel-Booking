namespace HotelBooking.Specs.Framework
{
    using System;
    using Polly;

    public static class FunctionRetrier
    {
        public static void RetryOnException<TException>(Action action, int retryCount = 5, int waitSeconds = 1)
            where TException : Exception
        {
            Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(action);
        }

        public static TResult RetryOnException<TResult, TException>(Func<TResult> function, int retryCount = 5, int waitSeconds = 1)
            where TException : Exception
        {
            return Policy
                .Handle<TException>()
                .WaitAndRetry(retryCount, count => TimeSpan.FromSeconds(waitSeconds))
                .Execute(function);
        }
    }
}