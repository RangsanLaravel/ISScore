using System.ComponentModel.DataAnnotations;

namespace ISScore.Models
{
    public class Search
    {
        [Display(Name = "EMPLOYEE ID"),Required(ErrorMessage = "กรุณาระบุ EMPLOYEE ID")]
        public string EMPLOYEE_ID { get; set; }
    }
}
