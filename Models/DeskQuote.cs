using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MegaDeskSite.Models
{


    public class DeskQuote
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name ="Name")]
        [Required]
        [StringLength(30, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        public string CustomerName {  get; set; } = string.Empty;

        [Display(Name ="# of Drawers")]
        [Required]
        [Range(0,7)]
        public int NumDrawers {  get; set; }

        [Required]
        [Range(24, 96)]
        public int Width {  get; set; }

        [Required]
        [Range(12, 48)]
        public int Depth {  get; set; }

        public string Material {  get; set; } = string.Empty;

        [Display(Name ="Rush Order")]
        public int RushOrderChoice {  get; set; }



    }
}
