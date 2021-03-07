using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Countries.Models.Models
{
    [Table("Country")]
    public class Country
    {
        [Key]
        public int CountryID { get; set; }

        [Required(ErrorMessage = "The field {0} is required")]
        [StringLength(30)]
        public string Name { get; set; }

        [StringLength(2)]
        public string Alpha_2 { get; set; }

        [StringLength(3)]
        public string Alpha_3 { get; set; }

        [StringLength(3)]
        public string NumericCode { get; set; }

        public ICollection<Subdivision> Subdivisions { get; set; }
    }
}
