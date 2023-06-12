using System.Collections.Generic;
using System.Web.Mvc;
using eUseControl.BusinessLogic.BL;
using eUseControl.BusinessLogic.Interfaces;
using eUseControl.Domain.Entities.User;
using eUseControl.Domain.Enums;
using eUseControl.Web.Filters;
using eUseControl.Web.Models;
using OrderData = eUseControl.Domain.Entities.Order.OrderData;

namespace eUseControl.Web.Controllers
{
     public class ProfileController : BaseController
     {
          private readonly ITrainer _trainers;
          private readonly IOrder _order;

          public ProfileController()
          {
               BussinesLogic bl = new BussinesLogic();
               _trainers = bl.GetTrainerBl();
               _order = bl.GetOrderBl();
               _order.CheckExpiration();
          }
          public ActionResult Index()
          {
               GetUserData();

               var user = (UserMinimal)System.Web.HttpContext.Current?.Session["__SessionObject"];
               if (user != null && user.Level == URole.Trainer)
               {
                    return RedirectToAction("TrainerProfile", "Profile");
               }
               else if (user != null && user.Level == URole.User)
               {
                    return RedirectToAction("UserProfile", "Profile");
               }
               else if (user != null && user.Level == URole.Admin)
               {
                    return RedirectToAction("AdminProfile", "Profile");
               }
               else
               {
                    return RedirectToAction("Index", "Error");
               }
          }

          public ActionResult TrainerProfile()
          {
               GetUserData();
               var orders = _order.GetOrderByTrainerId(ViewBag.TrainerId);
               var data = GetOrders(orders);
               return View(data);
          }

          public ActionResult UserProfile()
          {
               GetUserData();
               var orders = _order.GetOrderByUserId(ViewBag.UserId);
               var data = GetOrders(orders);
               return View(data);
          }
          [AdminMod]
          public ActionResult AdminProfile()
          {
               GetUserData();
               var orders = _order.GetOrders();
               var data = GetOrders(orders);
               return View(data);
          }

          public List<OrderInfo> GetOrders(List<OrderData> data)
          {
               List<OrderInfo> orderData = new List<OrderInfo>();
               foreach (var item in data)
               {
                    orderData.Add(new OrderInfo
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
                    });
               }

               return orderData;
          }
     }
}