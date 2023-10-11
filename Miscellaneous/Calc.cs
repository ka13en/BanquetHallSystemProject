using System.Linq;
using Banquet_Hall_App.BanquetSystemDbContext;

namespace Banquet_Hall_App.Miscellaneous
{
    public static class Calc
    {
        public const decimal OrderStartPrice = 500M;

        public static void UpdateAllOrdersTotalCost(MyDbContext context)
        {
            var orders = context.Orders.ToList();
            foreach (var order in orders)
            {
                order.TotalCost = CalculateOrderTotalCost(context, order.Id);
                context.SaveChanges();
            }
        }

        public static void UpdateOrderTotalCost(MyDbContext context, uint orderId)
        {
            var order = context.Orders.Find(orderId);
            if (order != null)
            {
                order.TotalCost = CalculateOrderTotalCost(context, orderId);
                context.SaveChanges();
            }
        }

        private static decimal CalculateOrderTotalCost (MyDbContext context, uint orderId)
        {
            decimal totalCost = OrderStartPrice;
            var OrderDishAsList = context.OrderDish.Where(od => od.Order.Id == orderId).ToList();
            foreach (var orderDish in OrderDishAsList)
            {
                var dish = context.Dish.Find(orderDish.DishId);
                totalCost += orderDish.DishesAmount * dish.Price;
            }
            return totalCost;
        }
    }
}
