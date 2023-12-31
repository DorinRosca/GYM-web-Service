﻿using System.Collections.Generic;
using eUseControl.BusinessLogic.Core;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.BL
{
     internal class OrderBl : OrderApi, IOrder
     {
          public PostResponse CreateOrder(OrderDbTable order)
          {
               return CreateOrderAction(order);
          }

          public List<OrderData> GetOrderByTrainerId(int id)
          {
               return GetOrderByTrainerIdAction(id);
          }

          public List<OrderData> GetOrderByUserId(int id)
          {
               return GetOrderByUserIdAction(id);
          }

          public void CheckExpiration()
          {
               CheckExpirationAction();
          }

          public List<OrderData> GetOrders()
          {
               return GetOrdersAction();
          }
     }
}