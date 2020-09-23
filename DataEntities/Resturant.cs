using System.ComponentModel.DataAnnotations;

namespace OdeToFood.Data_Entities
{
    public enum CusineType
    {
        mexican,
        italian,
         egyptian,
         indian
    }
    public class Resturant
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please enter your name ")]
        public string Name { get; set; }
        [Required(ErrorMessage ="The location is required ")]
        public string Location { get; set; }
        public string imagePath { get; set; }
        public string OwnerName { get; set; }
        public CusineType Cusine{get;set;}
    }
}
