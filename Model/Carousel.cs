using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MembershipApplication.Model
{
    public class Carousel
    {
        [Key, Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter Image Url.")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please Enter Slide Caption.")]
        public string SlideCaption { get; set; }
    }
}
