using CakeCompany.Contract.Models;
using CakeCompany.Models.Cakes;
using CakeCompany.Models.Enum;

namespace CakeCompany.Factory
{
    public static class CreateCakeFactory
    {
        private static Lazy<Dictionary<Cake, ICake>> cakes;

        static CreateCakeFactory()
        {
            cakes = new Lazy<Dictionary<Cake, ICake>>(() => LoadCakes());
        }

        private static Dictionary<Cake, ICake> LoadCakes()
        {
            Dictionary<Cake, ICake> cakeDictionary = new()
            {
                { Cake.Chocolate, new ChocolateCake() },
                { Cake.RedVelvet, new RedVelvetCake() },
                { Cake.Vanilla, new VanillaCake() }
            };

            return cakeDictionary;
        }

        public static ICake Create(Cake cakeName)
        {
            return cakes.Value[cakeName];
        }
    }
}
