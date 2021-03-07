using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Models.Models
{
    [Table("Subdivison")]
    public class Subdivision
    {
        [Key]
        public int SubdivisonID { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(30)]
        public string Name { get; set; }

        public int CountryID { get; set; }
        public Country Country { get; set; }
    }
}
