using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eUseControl.Web.Models
{
     public class OrderInfo
     {
          public int Id { get; set; }
          public int UserId { get; set; }
          public string UserName { get; set; }

          public int SubscriptionId { get; set; }
          public string SubscriptionName { get; set; }
          public int? TrainerId { get; set; }
          public string TrainerName { get; set; }

          public int SubscriptionDurationId { get; set; }
          public string SubscriptionDurationName { get; set; }
          public DateTime PurchaseDate { get; set; }
          public DateTime ExpirationDate { get; set; }
          public bool IsValid { get; set; }

          public int Price { get; set; }
     }
}