﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eUseControl.Domain.Entities.Order
{
     public class OrderDbTable
     {
          [Required]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int Id { get; set; }
          [Required]
          public int UserId { get; set; }
          [Required]
          public string UserName { get; set; }

          [Required]
          public int SubscriptionId { get; set; }
          [Required]
          public string SubscriptionName { get; set; }
          [Required]
          public int? TrainerId { get; set; }
          [Required]
          public string TrainerName { get; set; }

          [Required]
          public int SubscriptionDurationId { get; set; }
          [Required]
          public string SubscriptionDurationName { get; set; }
          public DateTime PurchaseDate { get; set; }

          [Required]
          public DateTime ExpirationDate { get; set; }
          public bool IsValid { get; set; }

          public int Price { get; set; }

     }
}