namespace AMVTravels.Extensions
{
    public static class WebHostEnvionmentExtensions
    {
        public static bool IsDevelopment(this IWebHostEnvironment host)
        {
            return host.IsEnvironment("Local") || host.IsEnvironment("Development");
        }
    }
}
