using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AddressBook.Models
{
    public class SEC_UserModel
    {
        public int UserID { get; set; }

        //[Required]
        [Required(ErrorMessage = "Please Enter User Name")]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string? EmailAddress { get; set; }
        [Required]
        [DisplayName("Profile Photo")]
        public IFormFile File { get; set; }
        public string? PhotoPath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
