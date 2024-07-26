using System.ComponentModel.DataAnnotations;

namespace DataModal.Models
{
    public class AllEnum
    {
        


        public enum Target
        {
            _self,
            _blank
        }

        public enum Gender
        {
            [Display(Name = "All")]
            All,
            [Display(Name = "Male")]
            Male,
            [Display(Name = "Female")]
            Female
        }

       
        
    }

    public class DropDownlist
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Other { get; set; }
    
    }
}
