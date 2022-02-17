namespace HotelBooking.Specs.Screenplay
{
    using System;
    using System.IO;
    using Microsoft.Extensions.Configuration;

    public static class AppSettings
    {
        private static readonly IConfiguration Configuration;

        static AppSettings()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
        }

        public static string Url => Configuration["HotelBooking:Url"];
    }
}