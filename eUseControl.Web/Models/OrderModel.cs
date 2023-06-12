namespace eUseControl.Web.Models
{
     public class OrderModel
     {
          public Order Orders { get; set; }
          public OrderData OrderData { get; set; }


          public OrderModel()
          {
               OrderData = new OrderData();
               Orders = new Order();

          }
     }
}