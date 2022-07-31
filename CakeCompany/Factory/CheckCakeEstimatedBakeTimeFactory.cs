using CakeCompany.Models.Enum;

namespace CakeCompany.Factory
{
    public static class CheckCakeEstimatedBakeTimeFactory
    {
        private static Lazy<Dictionary<Cake, DateTime>> estimatedBakeTimes;

        static CheckCakeEstimatedBakeTimeFactory()
        {
            estimatedBakeTimes = new Lazy<Dictionary<Cake, DateTime>>(() => LoadCakeEstimatedBakeTime());
        }

        private static Dictionary<Cake, DateTime> LoadCakeEstimatedBakeTime()
        {
            Dictionary<Cake, DateTime> estimatedBakeTimesDictionary = new()
            {
                { Cake.Chocolate, DateTime.Now.Add(TimeSpan.FromMinutes(30)) },
                { Cake.RedVelvet, DateTime.Now.Add(TimeSpan.FromMinutes(60)) },
                { Cake.Vanilla, DateTime.Now.Add(TimeSpan.FromHours(15)) }
            };

            return estimatedBakeTimesDictionary;
        }

        public static DateTime GetCakeEstimatedBakeTime(Cake cakeName)
        {
            return estimatedBakeTimes.Value[cakeName];
        }
    }
}
