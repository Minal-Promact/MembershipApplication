using System.ComponentModel.DataAnnotations;

namespace MembershipApplication.Model
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Position { get; set; }
    }
}
