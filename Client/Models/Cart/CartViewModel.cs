
using Domain.Extensions;

namespace Models.Cart
{
    public class CartViewModel
    {
        public Domain.Cart Cart { get; set; }
        public string ReturnUrl { get; set; }

        public double ChangePrice(Domain.Dish dish)
        {
            return DishMethods.GetDishPrice(dish);
        }
    }
}
