namespace HotelBooking.Specs.Screenplay.Web.Questions
{
    using OpenQA.Selenium;
    using Pattern;

    public abstract class WebQuestion<TAnswer> : IQuestion<TAnswer>
    {
        protected abstract TAnswer AskAs(IActor actor, IWebDriver driver);

        public TAnswer AskAs(IActor actor)
        {
            return AskAs(actor, actor.Using<BrowseTheWeb>().Driver);
        }
    }
}