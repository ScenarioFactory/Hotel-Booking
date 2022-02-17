namespace HotelBooking.Specs.Screenplay.Pattern
{
    public interface IActor
    {
        TAnswer AsksFor<TAnswer>(IQuestion<TAnswer> question);

        void AttemptsTo(params ITask[] tasks);

        IActor WhoCan(params IAbility[] abilities);

        TAbility Using<TAbility>() where TAbility : IAbility;
    }
}
