using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.LOC_City.Models
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }

        [Required(ErrorMessage = "Please Enter City Name")]
        public string CityName { get; set; }

        [Required(ErrorMessage = "Please Enter City Code")]
        public string CityCode { get; set; }

        [Required(ErrorMessage = "Please Select Country Name")]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "Please Select State Name")]
        public int StateID { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class LOC_CityDropdownModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
    }
}
