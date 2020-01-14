using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

namespace Domain.Extensions.Session
{
    public class SessionCart : Cart
    { 
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Meal meal, DayOfWeek dayOfWeek)
        {
            base.AddItem(meal, dayOfWeek);
            Session.SetJson("Cart", this);
        }
        public override void RemoveLine(Meal meal)
        {
            base.RemoveLine(meal);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
