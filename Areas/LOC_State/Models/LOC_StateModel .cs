using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.LOC_State.Models
{
    public class LOC_StateModel
    {
        public int? StateID { get; set; }
        [Required(ErrorMessage = "Please Enter State Name")]
        public string? StateName { get; set; }
        [Required(ErrorMessage = "Please Enter State Code")]
        public string? StateCode { get; set; }
        [Required(ErrorMessage = "Please Select Country Name")]
        public int CountryID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
    }
    public class LOC_StateDropdownModel
    {
        public int StateID { get; set; }
        public string StateName { get; set; }
    }
}
