using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        [Required]
        public int StaffId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int PhoneNumber { get; set; }

        //Navigation property to Staffing class
        public List<Staffing> Staffings { get; set; }
    }
}
