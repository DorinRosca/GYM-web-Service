using System.ComponentModel.DataAnnotations;

namespace eUseControl.Web.Models
{
    public class Trainer
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Age { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        [Display(Name = "Phone Number")]
        public int Number { get; set; }

        public string Specialization { get; set; }

        public string Availability { get; set; }

        [Display(Name = "Price Per Month")]
        public int Price { get; set; }
    }
}