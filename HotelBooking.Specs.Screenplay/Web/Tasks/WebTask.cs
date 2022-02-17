namespace HotelBooking.Specs.Screenplay.Web.Tasks
{
    using OpenQA.Selenium;
    using Pattern;

    public abstract class WebTask : ITask
    {
        protected abstract void PerformAs(IActor actor, IWebDriver driver);

        public void PerformAs(IActor actor)
        {
            PerformAs(actor, actor.Using<BrowseTheWeb>().Driver);
        }
    }
}