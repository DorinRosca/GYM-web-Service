using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using eUseControl.BusinessLogic.BL;
using eUseControl.BusinessLogic.DBModel;
using eUseControl.Domain.Entities.Order;
using eUseControl.Domain.Entities.User;

namespace eUseControl.BusinessLogic.Core
{
     public class OrderApi
     {
          internal PostResponse CreateOrderAction(OrderDbTable order)
          {
               if (order is null)
               {
                    return new PostResponse { Status = false, StatusMsg = "Data is Invalid" };
               }

               order = AddOrderDetails(order);

               using (var db = new UserContext())

               {
                    db.Orders.Add(order);
                    db.SaveChanges();
               }
               return new PostResponse { Status = true };
          }

          private OrderDbTable AddOrderDetails(OrderDbTable order)
          {
               var bl = new BussinesLogic();
               var subscriptionDuration = bl.GetsSubscriptionDurationBl();
               var subscription = bl.GetSubscriptionBl();
               var subs = subscription.GetSingleSubscription(order.SubscriptionId);
               var subsDetails = subscriptionDuration.GetSingleSubscriptionDuration(order.SubscriptionDurationId);
               var trainer = bl.GetTrainerBl();
               var trainerPrice = 0;
               var trainerName = "";
               if (order.TrainerId != null)
               {
                    var trainerDetails = trainer.GetSingleTrainer(order.TrainerId);
                    if (trainerDetails != null)
                    {
                         trainerPrice = trainerDetails.Price;
                         trainerName = trainerDetails.Name;
                    }
                    else
                    {
                         trainerPrice = 0;
                         trainerName = "Fara Antrenor";

                    }
               }

               order.SubscriptionName = subs.Name;
               order.TrainerName = trainerName;
               order.SubscriptionDurationName = subsDetails.Name;
               order.Price = subs.Price * subsDetails.Months +trainerPrice*subsDetails.Months;
               order.Price -= order.Price*(subsDetails.Discount/100);
               order.ExpirationDate = order.PurchaseDate.AddMonths(subsDetails.Months);
               return order;
          }

          internal List<OrderData> GetOrderByTrainerIdAction(int trainerId)
          {
               List<OrderData> orders = new List<OrderData>();
               using (var db = new UserContext())
               {
                    var result = db.Orders.ToList();

                    foreach (var item in result)
                    {
                         if (item.TrainerId == trainerId)
                         {
                              orders.Add(new OrderData
                              {
                                   Id = item.Id,
                                   UserId = item.UserId,
                                   UserName = item.UserName,
                                   SubscriptionId = item.SubscriptionId,
                                   SubscriptionName = item.SubscriptionName,
                                   TrainerId = item.TrainerId,
                                   TrainerName = item.TrainerName,
                                   SubscriptionDurationId = item.SubscriptionDurationId,
                                   SubscriptionDurationName = item.SubscriptionDurationName,
                                   PurchaseDate = item.PurchaseDate,
                                   ExpirationDate = item.ExpirationDate,
                                   IsValid = item.IsValid,
                                   Price = item.Price
                              }
                              );
                         }
                    }
               }
               return orders;
          }

          internal List<OrderData> GetOrderByUserIdAction(int userId)
          {
               List<OrderData> orders = new List<OrderData>();
               using (var db = new UserContext())
               {
                    var result = db.Orders.ToList();

                    foreach (var item in result)
                    {
                         if (item.UserId == userId)
                         {
                              orders.Add(new OrderData
                              {
                                   Id = item.Id,
                                   UserId = item.UserId,
                                   UserName = item.UserName,
                                   SubscriptionId = item.SubscriptionId,
                                   SubscriptionName = item.SubscriptionName,
                                   TrainerId = item.TrainerId,
                                   TrainerName = item.TrainerName,
                                   SubscriptionDurationId = item.SubscriptionDurationId,
                                   SubscriptionDurationName = item.SubscriptionDurationName,
                                   PurchaseDate = item.PurchaseDate,
                                   ExpirationDate = item.ExpirationDate,
                                   IsValid = item.IsValid,
                                   Price = item.Price
                              }
                              );
                         }
                    }
               }
               return orders;
          }

          internal List<OrderData> GetOrdersAction()
          {
               List<OrderData> orders = new List<OrderData>();
               using (var db = new UserContext())
               {
                    var result = db.Orders.ToList();

                    foreach (var item in result)
                    {
                         orders.Add(new OrderData
                         {
                              Id = item.Id,
                              UserId =item.UserId,
                              UserName = item.UserName,
                              SubscriptionId = item.SubscriptionId,
                              SubscriptionName = item.SubscriptionName,
                              TrainerId = item.TrainerId,
                              TrainerName =item.TrainerName,
                              SubscriptionDurationId = item.SubscriptionDurationId,
                              SubscriptionDurationName =item.SubscriptionDurationName,
                              PurchaseDate =item.PurchaseDate,
                              ExpirationDate = item.ExpirationDate,
                              IsValid = item.IsValid,
                              Price = item.Price
                         }
                         );
                    }
               }
               return orders;
          }
          internal void CheckExpirationAction()
          {
               using (var db = new UserContext())
               {
                    var orders = db.Orders.ToList();
                    foreach (var item in orders)
                    {
                         if (item.ExpirationDate <= DateTime.Now)
                         {
                              item.IsValid = false;
                         }

                         db.Entry(item).State = EntityState.Modified;
                         db.SaveChanges();
                    }
               }
          }
     }
}
