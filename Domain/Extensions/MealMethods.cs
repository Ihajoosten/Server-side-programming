
namespace Domain.Extensions
{
    public static class MealMethods
    {
        public static double GetMealPrice(this Meal meal)
        {
            double price = 0.0;
            foreach (var dish in meal.MealDishes)
            {
                price += dish.Price;
            }
            return price;
        }
    }
}
