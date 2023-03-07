using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.MST_Contact.Models
{
    public class MST_ContactModel
    {
        public int? ContactID { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Please Enter Number")]
        public string ContactMobileNumber { get; set; }
        [Required(ErrorMessage = "Please Enter Address")]
        public string ContactAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string ContactEmail { get; set; }
        [Required(ErrorMessage = "Please Enter Country Name")]
        public int CountryID { get; set; }
        [Required(ErrorMessage = "Please Enter State Name")]
        public int StateID { get; set; }
        [Required(ErrorMessage = "Please Enter City Name")]
        public int CityID { get; set; }
        [Required(ErrorMessage = "Please Enter Category")]
        public int ContactCategoryID { get; set; }

        [Required(ErrorMessage = "Please Select Profile Picture")]
        public IFormFile File { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
