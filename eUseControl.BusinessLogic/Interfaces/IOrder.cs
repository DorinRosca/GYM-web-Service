using System.Collections.Generic;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.Interfaces
{
     public interface IOrder
     {
          PostResponse CreateOrder(OrderDbTable order);

          List<OrderData> GetOrderByTrainerId(int id);

          List<OrderData> GetOrderByUserId(int id);

          void CheckExpiration();

          List<OrderData> GetOrders();
     }
}