using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.LOC_Country.Models
{
    public class LOC_CountryModel
    {
        public int? CountryID { get; set; }

        [Required(ErrorMessage = "Please Enter Country Name")]
        public string? CountryName { get; set; }
        [Required(ErrorMessage = "Please Enter Country Code")]
        public string? CountryCode { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }

    public class LOC_CountryDropdownModel
    {
        public int CountryID { get; set; }
        public string? CountryName { get; set; }
    }
}
