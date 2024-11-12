using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staff
    {

        public Staff()
        {
        }

        public Staff(int staffId, string firstName, string lastName)
        {
            StaffId = staffId;
            FirstName = firstName;
            LastName = lastName;
        }
       
        public int StaffId { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        //Navigation property to Staffing class
        public List<Staffing>? Staffings { get; set; }
    }
}
