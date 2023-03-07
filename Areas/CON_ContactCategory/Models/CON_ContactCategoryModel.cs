using System.ComponentModel.DataAnnotations;

namespace AddressBook.Areas.CON_ContactCategory.Models
{
    public class CON_ContactCategoryModel
    {
        public int? ContactCategoryID { get; set; }
        [Required(ErrorMessage = "Please Enter Category Name")]
        public string? ContactCategoryName { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
    public class CON_ContactCategoryDropdownModel
    {
        public int? ContactCategoryID { get; set; }
        public string? ContactCategoryName { get; set; }
    }
}
